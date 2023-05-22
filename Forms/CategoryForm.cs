using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using FileMe.Filing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FileMe.Forms {

    public partial class CategoryForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        private FileMeForm                      mFileMeForm;

        private FilerManager                    mFilerManager;

        private CategoryManager                 mCategoryManager;
        private Category                        mCategory = null;
        
        private List<string>                    mGroups = new List<string>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public CategoryForm(FileMeForm fileMeForm, FilerManager filerManager, CategoryManager categoryManager, Category category = null) {
            // Initialise
            InitializeComponent();

            // File me form
            mFileMeForm = fileMeForm;
            TopMost = fileMeForm.AlwaysOnTop;

            // Filer manager
            mFilerManager = filerManager;

            // Category manager
            mCategoryManager = categoryManager;
            mCategory = category;

            // Category
            if (category == null)
                colColour.Color = Color.FromArgb(128, 128, 128);
            else {
                txtName.Text = category.Name;
                colColour.Color = category.Colour;
                chkGroupStartsWith.Checked = category.GroupStartsWithEnabled;
                txtGroupStartsWith.Text = category.GroupStartsWith;

                mGroups.Clear();
                foreach (string g in category.Groups) {
                    mGroups.Add(g);
                }
                mGroups.Sort();
            }

            // Bindings
            lstGroups.DataSource = mGroups;
        }


        // CATEGORY ================================================================================
        //--------------------------------------------------------------------------------
        public void Apply(Category category) {
            category.Name = txtName.Text;
            category.Colour = colColour.Color;
            category.GroupStartsWithEnabled = chkGroupStartsWith.Checked;
            category.GroupStartsWith = txtGroupStartsWith.Text;

            category.Groups.Clear();
            category.Groups.AddRange(mGroups);
        }


        // NAVIGATION ================================================================================
        //--------------------------------------------------------------------------------
        private void btnOk_Click(object sender, EventArgs e) {
            // Validation
            DialogResult = DialogResult.None;

            // Mandatory fields
            if (string.IsNullOrWhiteSpace(txtName.Text)) {
                XtraMessageBox.Show("Please enter a name", "File Me");
                txtName.Focus();
                return;
            }

            // Name
            if (mCategory == null && mCategoryManager.HasCategory(txtName.Text, false)) {
                XtraMessageBox.Show(this, $"The name '{txtName.Text}' is already in use", "File Me");
                return;
            }

            // Groups
            if (chkGroupStartsWith.Checked && string.IsNullOrWhiteSpace(txtGroupStartsWith.Text)) {
                XtraMessageBox.Show("Please enter a group starts with pattern", "File Me");
                txtGroupStartsWith.Focus();
                return;
            }

            if (!chkGroupStartsWith.Checked && mGroups.Count == 0) {
                XtraMessageBox.Show("Please add a group or a group starts with pattern", "File Me");
                lstGroups.Focus();
                return;
            }
            
            // Valid
            DialogResult = DialogResult.OK;
        }

        
        // CONTROLS ================================================================================
        //--------------------------------------------------------------------------------
        private void txtName_Leave(object sender, EventArgs e) {
            txtName.Text = txtName.Text.Trim();
        }
        
        //--------------------------------------------------------------------------------
        private void chkGroupStartsWith_CheckedChanged(object sender, EventArgs e) {
            txtGroupStartsWith.Enabled = chkGroupStartsWith.Checked;
        }


        // GROUPS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnAddGroup_Click(object sender, EventArgs e) {
            // Group
            CategoryGroupForm groupForm = new CategoryGroupForm(mFileMeForm, mFilerManager);
            if (groupForm.ShowDialog() != DialogResult.OK)
                return;

            // Add
            if (mGroups.Contains(groupForm.Group))
                return;
            mGroups.Add(groupForm.Group);

            // Sort / update selection
            string selectedGroup = (string)lstGroups.SelectedItem;
            mGroups.Sort();
            lstGroups.SelectedIndex = mGroups.IndexOf(selectedGroup);
        }
        
        //--------------------------------------------------------------------------------
        private void btnRemoveGroup_Click(object sender, EventArgs e) {
            // Checks
            if (lstGroups.SelectedIndex < 0 || mGroups.Count == 0)
                return;

            // Prompt
            if (XtraMessageBox.Show($"Remove the group '{mGroups[lstGroups.SelectedIndex]}'?", "File Me", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Remove
            mGroups.RemoveAt(lstGroups.SelectedIndex);

            // Selection
            if (lstGroups.SelectedIndex >= mGroups.Count)
                lstGroups.SelectedIndex = mGroups.Count - 1;
        }
    }

}
