using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using FileMe.Filing;
using Newtonsoft.Json;
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

    public partial class CategoryExportForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        private CategoryManager                 mCategoryManager;


        //================================================================================
        //--------------------------------------------------------------------------------
        public CategoryExportForm(FileMeForm fileMeForm, CategoryManager categoryManager) {
            // Initialise
            InitializeComponent();

            // Top most
            TopMost = fileMeForm.AlwaysOnTop;

            // Category manager
            mCategoryManager = categoryManager;
        }

        
        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void ExportForm_Shown(object sender, EventArgs e) {
            // Categories
            foreach (Category c in mCategoryManager.Categories) {
                trlCategories.AppendNode(new object[] { c.Name, c.GroupStartsWithEnabled ? c.GroupStartsWith : "", c.GroupsString, c }, null);
            }

            // Expand / check
            trlCategories.ExpandAll();
            CheckAllCategories();
        }

        
        // CATEGORIES ================================================================================
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
        private void btnExport_Click(object sender, EventArgs e) {
            // Checks
            if (CheckedCategoryCount == 0) {
                XtraMessageBox.Show(this, "Nothing selected for export", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Location
            if (dlgExportCategories.ShowDialog() != DialogResult.OK)
                return;

            // Export
            StreamWriter streamWriter = new StreamWriter(dlgExportCategories.FileName);
            JsonTextWriter writer = new JsonTextWriter(streamWriter);

            // Formatting
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;

            // Start
            writer.WriteStartObject();

            // Categories
            writer.WritePropertyName("Categories");
            writer.WriteStartArray();
            foreach (Category c in CheckedCategories) {
                writer.WriteStartObject();
                c.SaveSettings(writer);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            // End
            writer.WriteEndObject();
            
            // Close
            streamWriter.Close();

            // Message
            XtraMessageBox.Show(this, $"Categories exported to '{Path.GetFileName(dlgExportCategories.FileName)}'", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

}
