using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using FileMe.Filing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FileMe.Forms {

    public partial class CategoryImportForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        private CategoryManager                 mCategoryManager;

        private string                          mFilename = null;

        private List<Category>                  mCategories = new List<Category>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public CategoryImportForm(FileMeForm fileMeForm, CategoryManager categoryManager) {
            // Initialise
            InitializeComponent();

            // Top most
            TopMost = fileMeForm.AlwaysOnTop;

            // Category manager
            mCategoryManager = categoryManager;

            // Filename
            if (dlgImportCategories.ShowDialog(this) == DialogResult.OK) {
                mFilename = dlgImportCategories.FileName;
                if (!LoadCategories())
                    mFilename = null;
            }
        }
        

        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void ImportForm_Shown(object sender, EventArgs e) {
            // Checks
            if (mFilename == null)
                Close();

            // Load
            ShowCategories();

            // Expand / check
            trlCategories.ExpandAll();
            CheckAllCategories();
        }
        

        // FILERS ================================================================================
        //--------------------------------------------------------------------------------
        private bool LoadCategories() {
            try {
                // Load
                StreamReader streamReader = new StreamReader(mFilename);
                string json = streamReader.ReadToEnd();
                streamReader.Close();

                // Parse
                JObject jsonObject = JObject.Parse(json);

                // Categories
                JArray categories = (JArray)jsonObject.SelectToken("Categories");
                if (categories != null) {
                    foreach (JToken c in categories) {
                        Category category = mCategoryManager.CreateCategory(true);
                        category.LoadSettings(c);
                        mCategories.Add(category);
                    }
                }
            }
            catch (JsonReaderException e) {
                XtraMessageBox.Show(this, $"File not valid for import: {e.Message}", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            catch (Exception) {
                XtraMessageBox.Show(this, $"File not valid for import: missing data", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Return
            return true;
        }

        //--------------------------------------------------------------------------------
        private void ShowCategories() {
            foreach (Category c in mCategories) {
                trlCategories.AppendNode(new object[] { c.Name, c.GroupStartsWithEnabled ? c.GroupStartsWith : "", c.GroupsString, c }, null);
            }
        }

        //--------------------------------------------------------------------------------
        private void CheckAllCategories() {
            foreach (TreeListNode c in trlCategories.Nodes) {
                c.CheckState = CheckState.Checked;
            }
        }
        
        //--------------------------------------------------------------------------------
        private void UncheckAllCategories() {
            foreach (TreeListNode c in trlCategories.Nodes) {
                c.CheckState = CheckState.Unchecked;
            }
        }
        
        //--------------------------------------------------------------------------------
        private List<Category> CheckedCategories {
            get {
                List<Category> categories = new List<Category>();
                foreach (TreeListNode c in trlCategories.Nodes) {
                    if (c.Checked)
                        categories.Add((Category)c.GetValue(trlCategories_CategoryColumn));
                }
                return categories;
            }
        }
        
        //--------------------------------------------------------------------------------
        private int CheckedCategoryCount { get { return CheckedCategories.Count; } }


        // BUTTONS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnCheckAll_Click(object sender, EventArgs e) { CheckAllCategories(); }
        private void btnUncheckAll_Click(object sender, EventArgs e) { UncheckAllCategories(); }
        
        //--------------------------------------------------------------------------------
        private void btnImport_Click(object sender, EventArgs e) {
            // Checks
            if (CheckedCategoryCount == 0) {
                XtraMessageBox.Show(this, "Nothing selected for import", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Categories
            foreach (Category c in CheckedCategories) {
                mCategoryManager.AddCategory(c);
            }

            // Sort
            mCategoryManager.SortCategories();

            // Message
            XtraMessageBox.Show(this, "Categories imported", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }

}
