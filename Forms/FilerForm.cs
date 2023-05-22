using CSACore.Core;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using FileMe.Filing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FileMe.Forms {

    public partial class FilerForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        public const string                     PREVIEW_FILENAME = @"MyFile.ext";
        
        public const int                        HEIGHT_WITHOUT_ADVANCED_OPTIONS = 495;
        public const int                        HEIGHT_WITH_ADVANCED_OPTIONS = 667;


        //================================================================================
        private FileMeForm                      mFileMeForm;

        private FilerManager                    mFilerManager;
        private Filer                           mFiler = null;
        private bool                            mNewFiler;

        private string                          mPreviousName = "";

        private bool                            mShowAdvancedOptions;

        private bool                            mPendingFilerPathSelectedNotification = false;


        //================================================================================
        //--------------------------------------------------------------------------------
        public FilerForm(FileMeForm fileMeForm, FilerManager filerManager, Filer filer = null, bool copying = false) {
            // Initialise
            InitializeComponent();

            // File me form
            mFileMeForm = fileMeForm;
            TopMost = fileMeForm.AlwaysOnTop;

            // Filer manager / filer
            mFilerManager = filerManager;
            if (!copying)
                mFiler = filer;

            // Groups
            cboGroup.Properties.Items.AddRange(filerManager.FilerGroups);

            // Date / time formats
            InitialiseDateFormats();
            InitialiseTimeFormats();

            // Filer
            if (filer == null) {
                mNewFiler = true;
                colColour.Color = Color.FromArgb(128, 128, 128);
                //memFilenamePattern.Text = "{FilenameWithoutExtension}-{Date}-{Time}.{Extension}";
            }
            else
                LoadFiler(filer, copying);

            // Advanced options
            UpdateAdvancedOptions();
            UpdateFilenamePattern();
        }


        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void FilerForm_Shown(object sender, EventArgs e) {
            // Tutorial
            if (mNewFiler)
                CSA.TutorialSystem.NotifyEvent("add_filer", this);
        }


        // FILER ================================================================================
        //--------------------------------------------------------------------------------
        public void Apply(Filer filer) {
            SaveFiler(filer);
        }
        
        //--------------------------------------------------------------------------------
        private void LoadFiler(Filer filer, bool copying = false) {
            // Filer
            cboGroup.Text = filer.Group;
            if (!copying)
                txtName.Text = filer.Name;
            colColour.Color = filer.Colour;

            // Destination
            memTargetPath.Text = filer.TargetPath;

            // Filename
            chkRename.Checked = filer.Rename;
            txtRename.Text = filer.RenameText;
            cboRenameMode.EditValue = filer.RenameModeString;
            chkAppendDate.Checked = filer.AppendDate;
            cboDateFormat.Text = filer.DateFormat;
            chkAppendTime.Checked = filer.AppendTime;
            cboTimeFormat.Text = filer.TimeFormat;

            // Advanced options
            //ShowAdvancedOptions = filer.AdvancedOptions
            chkFilenameAuditing.Checked = filer.AuditFilename;
            chkFileAttachments.Checked = filer.FileAttachments;
            chkUseEmailDate.Checked = filer.UseEmailDate;
            chkInputDate.Checked = filer.InputDate;
            chkIgnoreEmbeddedAttachments.Checked = filer.IgnoreEmbeddedAttachments;
            chkCustomFilenamePattern.Checked = filer.CustomFilenamePattern;
            if (chkCustomFilenamePattern.Checked)
                memFilenamePattern.Text = filer.FilenamePattern;
            ShowAdvancedOptions = chkCustomFilenamePattern.Checked;
        }

        //--------------------------------------------------------------------------------
        private void LoadGroupFiler(Filer filer) {
            // Filer
            colColour.Color = filer.Colour;

            // Destination
            memTargetPath.Text = filer.TargetPath;

            // Filename
            chkRename.Checked = filer.Rename;
            txtRename.Text = filer.RenameText;
            cboRenameMode.EditValue = filer.RenameModeString;
            chkAppendDate.Checked = filer.AppendDate;
            cboDateFormat.Text = filer.DateFormat;
            chkAppendTime.Checked = filer.AppendTime;
            cboTimeFormat.Text = filer.TimeFormat;

            // Advanced options
            chkFilenameAuditing.Checked = filer.AuditFilename;
            chkFileAttachments.Checked = filer.FileAttachments;
            chkUseEmailDate.Checked = filer.UseEmailDate;
            chkInputDate.Checked = filer.InputDate;
            chkIgnoreEmbeddedAttachments.Checked = filer.IgnoreEmbeddedAttachments;
            chkCustomFilenamePattern.Checked = filer.CustomFilenamePattern;
            if (chkCustomFilenamePattern.Checked)
                memFilenamePattern.Text = filer.FilenamePattern;
        }
        
        //--------------------------------------------------------------------------------
        private void SaveFiler(Filer filer) {
            // Filer
            filer.Name = txtName.Text;
            filer.Group = cboGroup.Text;
            filer.Colour = colColour.Color;

            // Destination
            filer.TargetPath = memTargetPath.Text;

            // Filename
            filer.Rename = chkRename.Checked;
            filer.RenameText = txtRename.Text;
            filer.RenameModeString = cboRenameMode.Text;
            filer.AppendDate = chkAppendDate.Checked;
            filer.DateFormat = cboDateFormat.Text;
            filer.AppendTime = chkAppendTime.Checked;
            filer.TimeFormat = cboTimeFormat.Text;

            // Advanced Options
            //filer.AdvancedOptions = ShowAdvancedOptions;
            filer.AuditFilename = chkFilenameAuditing.Checked;
            filer.FileAttachments = chkFileAttachments.Checked;
            filer.UseEmailDate = chkUseEmailDate.Checked;
            filer.InputDate = chkInputDate.Checked;
            filer.IgnoreEmbeddedAttachments = chkIgnoreEmbeddedAttachments.Checked;
            filer.CustomFilenamePattern = chkCustomFilenamePattern.Checked;
            filer.FilenamePattern = FilenamePattern;
            //filer.FilenamePattern = FilenamePattern.Replace("{Filename}", "{FilenameWithoutExtension}") + ".{Extension}";
        }


        // NAVIGATION ================================================================================
        //--------------------------------------------------------------------------------
        private void btnOk_Click(object sender, EventArgs e) {
            // Validation
            DialogResult = DialogResult.None;

            // Mandatory fields
            if (string.IsNullOrWhiteSpace(txtName.Text)) {
                XtraMessageBox.Show(this, "Please enter a name", "File Me");
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cboGroup.Text)) {
                XtraMessageBox.Show(this, "Please enter or select a group", "File Me");
                cboGroup.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(memTargetPath.Text)) {
                XtraMessageBox.Show(this, "Please select a target path", "File Me");
                memTargetPath.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(memFilenamePattern.Text)) {
                XtraMessageBox.Show(this, "Please enter a filename pattern", "File Me");
                memFilenamePattern.Focus();
                return;
            }

            // Name
            if (mFiler == null && mFilerManager.HasFiler(txtName.Text, false)) {
                XtraMessageBox.Show(this, "The name '" + txtName.Text + "' is already in use", "File Me");
                return;
            }

            // Date format
            if (!Filer.TestFilenamePattern(cboDateFormat.Text, out string dateFormatError)) {
                XtraMessageBox.Show(this, dateFormatError, "File Me");
                cboDateFormat.Focus();
                return;
            }

            // Time format
            if (!Filer.TestFilenamePattern(cboTimeFormat.Text, out string timeFormatError)) {
                XtraMessageBox.Show(this, timeFormatError, "File Me");
                cboTimeFormat.Focus();
                return;
            }

            // Filename pattern
            if (!Filer.TestFilenamePattern(memFilenamePattern.Text, out string filenamePatternError)) {
                XtraMessageBox.Show(this, filenamePatternError, "File Me");
                return;
            }
            
            // Valid
            DialogResult = DialogResult.OK;
        }
        
        //--------------------------------------------------------------------------------
        private void btnCancel_Click(object sender, EventArgs e) {
            CSA.TutorialSystem.NotifyEvent("filer_cancelled", this);
        }


        // FILER ================================================================================
        //--------------------------------------------------------------------------------
        private void txtName_Enter(object sender, EventArgs e) {
            mPreviousName = txtName.Text;
        }

        //--------------------------------------------------------------------------------
        private void txtName_Leave(object sender, EventArgs e) {
            //txtName.Text = txtName.Text.Trim();
            //if (string.IsNullOrWhiteSpace(txtRename.Text) || txtRename.Text.Equals(mPreviousName))
            //    txtRename.Text = txtName.Text;
        }
        
        //--------------------------------------------------------------------------------
        private void txtName_TextChanged(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txtRename.Text) || txtRename.Text.Equals(mPreviousName))
                txtRename.Text = txtName.Text;
            mPreviousName = txtName.Text;
        }
        
        //--------------------------------------------------------------------------------
        private void cboGroup_Leave(object sender, EventArgs e) {
            cboGroup.Text = cboGroup.Text.Trim();
        }
        
        //--------------------------------------------------------------------------------
        private void cboGroup_TextChanged(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(cboGroup.Text))
                CSA.TutorialSystem.NotifyEvent("filer_name_group_entered", this);
        }
        
        //--------------------------------------------------------------------------------
        private void cboGroup_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e) {
            if (e.CloseMode == PopupCloseMode.Normal && !string.IsNullOrWhiteSpace(cboGroup.Text) && mFiler == null) {
                // Copy group defaults
                if (mNewFiler) {
                    IEnumerable<Filer> filers = from Filer f in mFilerManager.Filers where f.Group.Equals(cboGroup.Text) select f;
                    if (filers.Count() > 0) {
                        Filer groupFiler = filers.OrderBy(f => f.Name).First();
                        LoadGroupFiler(groupFiler);
                    }
                }

                // Tutorial
                if (!string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(cboGroup.Text))
                    CSA.TutorialSystem.NotifyEvent("filer_name_group_entered", this);
            }
        }


        // TARGET PATH ================================================================================
        //--------------------------------------------------------------------------------
        private void btnBrowseTargetPath_Click(object sender, EventArgs e) {
            // Disable top most
            bool wasTopMost = TopMost;
            TopMost = false;

            // Title
            dlgBrowseFolders.Title = $"{(!string.IsNullOrWhiteSpace(txtName.Text) ? txtName.Text + " - " : "")}{(!string.IsNullOrWhiteSpace(cboGroup.Text) ? cboGroup.Text + " - ": "")}Select Target Path";

            // Selected path
            if (!string.IsNullOrWhiteSpace(memTargetPath.Text))
                dlgBrowseFolders.SelectedPath = memTargetPath.Text;

            // Browse
            //bool pathSelected = false;
            if (dlgBrowseFolders.ShowDialog() == DialogResult.OK) {
                memTargetPath.Text = dlgBrowseFolders.SelectedPath/*.Replace(@"\", "/")*/;
                //pathSelected = true;
            }

            // Restore top most
            mFileMeForm.TopMost = wasTopMost; // It seems that setting this form's TopMost to false cascades up to the file me form too
            TopMost = wasTopMost;

            // Tutorial
            //if (pathSelected) {
            //    pendingFilerPathSelectedNotification = true;
            //    tmrTimer.Enabled = true;
            //}
        }
        
        //--------------------------------------------------------------------------------
        private void memTargetPath_DragOver(object sender, DragEventArgs e) {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, false) ? DragDropEffects.Link : DragDropEffects.None;
        }
        
        //--------------------------------------------------------------------------------
        private void memTargetPath_DragDrop(object sender, DragEventArgs e) {
            // Files
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string f in filenames) {
                if (File.GetAttributes(f).HasFlag(FileAttributes.Directory)) {
                    memTargetPath.Text = f;
                    break;
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        // This tutorial event has to happen after a delay, as the restoration of top most
        // causes the tooltip to be instantly hidden.
        private void tmrTimer_Tick(object sender, EventArgs e) {
            if (mPendingFilerPathSelectedNotification) {
                mPendingFilerPathSelectedNotification = false;
                CSA.TutorialSystem.NotifyEvent("filer_path_selected", this);
                tmrTimer.Enabled = false;
            }
        }
        

        // FILENAME ================================================================================
        //--------------------------------------------------------------------------------
        private void chkRename_CheckedChanged(object sender, EventArgs e) {
            // Filename
            txtRename.Enabled = chkRename.Checked && !chkCustomFilenamePattern.Checked;
            cboRenameMode.Enabled = chkRename.Checked && !chkCustomFilenamePattern.Checked;
            UpdateFilenamePattern();

            // Tutorial
            CSA.TutorialSystem.NotifyEvent("filer_filename_unchecked", this);
        }
        
        //--------------------------------------------------------------------------------
        private void txtRename_Leave(object sender, EventArgs e) {
            txtRename.Text = txtRename.Text.Trim();
        }

        //--------------------------------------------------------------------------------
        private void txtRename_EditValueChanged(object sender, EventArgs e) { UpdateFilenamePattern(); }
        private void cboRenameMode_EditValueChanged(object sender, EventArgs e) { UpdateFilenamePattern(); }

        //--------------------------------------------------------------------------------
        private void chkAppendDate_CheckedChanged(object sender, EventArgs e) {
            // Filename
            UpdateFilenamePattern();

            // Tutorial
            if (chkAppendDate.Checked)
                CSA.TutorialSystem.NotifyEvent("filer_date_checked", this);
        }

        //--------------------------------------------------------------------------------
        private void cboDateFormat_EditValueChanged(object sender, EventArgs e) {
            if (cboDateFormat.EditValue is DateTimeFormatValue)
                cboDateFormat.EditValue = ((DateTimeFormatValue)cboDateFormat.EditValue).format;
            else
                UpdatePreview();
        }

        //--------------------------------------------------------------------------------
        private void chkAppendTime_CheckedChanged(object sender, EventArgs e) { UpdateFilenamePattern(); }

        //--------------------------------------------------------------------------------
        private void cboTimeFormat_EditValueChanged(object sender, EventArgs e) {
            if (cboTimeFormat.EditValue is DateTimeFormatValue)
                cboTimeFormat.EditValue = ((DateTimeFormatValue)cboTimeFormat.EditValue).format;
            else
                UpdatePreview();
        }

        
        // OPTIONS ================================================================================
        //--------------------------------------------------------------------------------
        private void chkFileAttachments_CheckedChanged(object sender, EventArgs e) { UpdateFilenamePattern(); }

        //--------------------------------------------------------------------------------
        private void chkInputDate_CheckedChanged(object sender, EventArgs e) {
            if (chkInputDate.Checked)
                chkUseEmailDate.Checked = false;
            UpdateFilenamePattern();
        }

        //--------------------------------------------------------------------------------
        private void chkUseEmailDate_CheckedChanged(object sender, EventArgs e) {
            if (chkUseEmailDate.Checked)
                chkInputDate.Checked = false;
            UpdateFilenamePattern();
        }

        
        // ADVANCED OPTIONS ================================================================================
        //--------------------------------------------------------------------------------
        public bool ShowAdvancedOptions {
            set {
                mShowAdvancedOptions = value;
                btnToggleAdvancedOptions.Text = mShowAdvancedOptions ? "Hide" : "Show";
                UpdateAdvancedOptions();
            }
            get { return mShowAdvancedOptions; }
        }

        //--------------------------------------------------------------------------------
        private void btnToggleAdvancedOptions_Click(object sender, EventArgs e) { ShowAdvancedOptions = !ShowAdvancedOptions; }
        private void chkCustomFilenamePattern_CheckedChanged(object sender, EventArgs e) { UpdateFilenamePattern(); }

        //--------------------------------------------------------------------------------
        private void memFilenamePattern_Enter(object sender, EventArgs e) {
            lciAdvancedOptions15.Visibility = LayoutVisibility.Always;
        }

        //--------------------------------------------------------------------------------
        private void memFilenamePattern_Leave(object sender, EventArgs e) {
            memFilenamePattern.Text = memFilenamePattern.Text.Trim();
            lciAdvancedOptions15.Visibility = LayoutVisibility.Never;
        }

        //--------------------------------------------------------------------------------
        private void memFilenamePattern_TextChanged(object sender, EventArgs e) {
            UpdatePreview();
        }


        // DATE / TIME ================================================================================
        //--------------------------------------------------------------------------------
        private void InitialiseDateFormats() {
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("dd-MM-yyyy", "27-03-2020"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("dd-MM-yy", "27-03-20"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("dd-MM", "27-03"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("dd", "27"));
            
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("MM-yyyy", "03-2020"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("MM-yy", "03-20"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("MM", "03"));
            
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("yyyy", "2020"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("yy", "20"));

            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("ddd dd MMM yyyy", "Fri 27 Mar 2020"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("ddd dd MMM", "Fri 27 Mar"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("ddd", "Fri"));

            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("dddd dd MMMM yyyy", "Friday 27 March 2020"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("dddd dd MMMM", "Friday 27 March"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("dddd", "Friday"));
            
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("dd MMM yyyy", "27 Mar 2020"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("dd MMM", "27 Mar"));

            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("dd MMMM yyyy", "27 March 2020"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("dd MMMM", "27 March"));

            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("MMM yyyy", "Mar 2020"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("MMM", "Mar"));

            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("MMMM yyyy", "March 2020"));
            cboDateFormat.Properties.Items.Add(new DateTimeFormatValue("MMMM", "March"));
        }
        
        //--------------------------------------------------------------------------------
        private void InitialiseTimeFormats() {
            cboTimeFormat.Properties.Items.Add(new DateTimeFormatValue("HH-mm-ss", "17-30-05"));
            cboTimeFormat.Properties.Items.Add(new DateTimeFormatValue("HH-mm", "17-30"));
            cboTimeFormat.Properties.Items.Add(new DateTimeFormatValue("HH", "17"));
            
            cboTimeFormat.Properties.Items.Add(new DateTimeFormatValue("h-mm-ss tt", "5-30-05 PM"));
            cboTimeFormat.Properties.Items.Add(new DateTimeFormatValue("h-mm tt", "5-30 PM"));
            cboTimeFormat.Properties.Items.Add(new DateTimeFormatValue("h tt", "5 PM"));
            
            cboTimeFormat.Properties.Items.Add(new DateTimeFormatValue("hh-mm-ss tt", "05-30-05 PM"));            
            cboTimeFormat.Properties.Items.Add(new DateTimeFormatValue("hh-mm tt", "05-30 PM"));
            cboTimeFormat.Properties.Items.Add(new DateTimeFormatValue("hh tt", "05 PM"));
        }


        // FILENAME PATTERN ================================================================================
        //--------------------------------------------------------------------------------
        public string FilenamePattern { get { return memFilenamePattern.Text; } }
        
        //--------------------------------------------------------------------------------
        private void UpdateFilenamePattern() {
            // Filename pattern
            if (!chkCustomFilenamePattern.Checked) {
                // Filename
                string filenamePattern = "";
                if (chkRename.Checked) {
                    switch (cboRenameMode.Text) {
                        case "Replace":         filenamePattern = txtRename.Text; break;
                        case "Add to front":    filenamePattern = (!string.IsNullOrWhiteSpace(txtRename.Text) ? txtRename.Text + " " : "") + "{FilenameWithoutExtension}"; break;
                        case "Add to end":      filenamePattern = "{FilenameWithoutExtension}" + (!string.IsNullOrWhiteSpace(txtRename.Text) ? " " + txtRename.Text : ""); break;
                    }
                }
                else
                    filenamePattern = "{FilenameWithoutExtension}";

                // Date / time
                if (chkAppendDate.Checked) {
                    filenamePattern += (!string.IsNullOrWhiteSpace(filenamePattern) ? " " : "");
                    if (chkUseEmailDate.Checked)
                        filenamePattern += "{EmailDate}";
                    else if (chkInputDate.Checked)
                        filenamePattern += "{InputDate}";
                    else
                        filenamePattern += "{Date}";
                }
                if (chkAppendTime.Checked)
                    filenamePattern += (!string.IsNullOrWhiteSpace(filenamePattern) ? " " : "") + "{Time}";

                // Extension
                filenamePattern += "{Extension}";
                memFilenamePattern.Text = filenamePattern.Trim();
            }

            // Controls
            memFilenamePattern.Enabled = chkCustomFilenamePattern.Checked;
            chkRename.Enabled = !chkCustomFilenamePattern.Checked;
            txtRename.Enabled = chkRename.Checked && !chkCustomFilenamePattern.Checked;
            cboRenameMode.Enabled = chkRename.Checked && !chkCustomFilenamePattern.Checked;
            chkAppendDate.Enabled = !chkCustomFilenamePattern.Checked;
            chkAppendTime.Enabled = !chkCustomFilenamePattern.Checked;
            chkInputDate.Enabled = !chkCustomFilenamePattern.Checked && chkAppendDate.Checked;
            chkUseEmailDate.Enabled = !chkCustomFilenamePattern.Checked && chkAppendDate.Checked;
            chkIgnoreEmbeddedAttachments.Enabled = chkFileAttachments.Checked;
        }

        //--------------------------------------------------------------------------------
        private void InsertAtFilenamePatternCaret(string text) {
            // Selection start
            int selectionStart = memFilenamePattern.SelectionStart;

            // Left brace
            int leftIndex = -1;
            for (int i = selectionStart - 1; i >= 0; --i) {
                if (memFilenamePattern.Text[i] == '{') {
                    leftIndex = i;
                    break;
                }
                else if (memFilenamePattern.Text[i] == '}')
                    break;
            }

            // Right brace
            int rightIndex = -1;
            for (int i = selectionStart; i < memFilenamePattern.Text.Length; ++i) {
                if (memFilenamePattern.Text[i] == '}') {
                    rightIndex = i + 1;
                    break;
                }
                else if (memFilenamePattern.Text[i] == '{')
                    break;
            }

            // Insertion point
            int insertionPoint = selectionStart;
            if (leftIndex != -1)
                insertionPoint = leftIndex;
            if (rightIndex != -1 && (leftIndex == -1 || (rightIndex - selectionStart <= selectionStart - leftIndex)))
                insertionPoint = rightIndex;

            // Insert
            memFilenamePattern.Text = memFilenamePattern.Text.Substring(0, insertionPoint) + text + memFilenamePattern.Text.Substring(insertionPoint, memFilenamePattern.Text.Length - insertionPoint);
            memFilenamePattern.SelectionStart = insertionPoint + text.Length;
        }

        //--------------------------------------------------------------------------------
        private void AddToFilenamePattern(string text) {
            int selectionStart = memFilenamePattern.SelectionStart;
            memFilenamePattern.Text = memFilenamePattern.Text + text;
            memFilenamePattern.SelectionStart = selectionStart;
        }

        //--------------------------------------------------------------------------------
        private void btnAddElement_Click(object sender, EventArgs e) {
            mnuAddElement.ShowPopup(new Point(Cursor.Position.X - 14, Cursor.Position.Y - 12));
        }
        
        //--------------------------------------------------------------------------------
        private void btnAddElement_Filename_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{Filename}"); }
        private void btnAddElement_FilenameWithoutExtension_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{FilenameWithoutExtension}"); }
        private void btnAddElement_Extension_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{Extension}"); }
        private void btnAddElement_ExtensionWithoutDot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{ExtensionWithoutDot}"); }
        private void btnAddElement_Date_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{Date}"); }
        private void btnAddElement_Year_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{Year}"); }
        private void btnAddElement_Month_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{Month}"); }
        private void btnAddElement_Day_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{Day}"); }
        private void btnAddElement_Time_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{Time}"); }
        private void btnAddElement_Hour_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{Hour}"); }
        private void btnAddElement_Minute_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{Minute}"); }
        private void btnAddElement_Second_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{Second}"); }
        private void btnAddElement_Millisecond_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{Millisecond}"); }
        private void btnAddElement_InputText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{InputText}"); }
        private void btnAddElement_InputDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{InputDate}"); }
        private void btnAddElement_EmailDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddToFilenamePattern("{EmailDate}"); }


        // PREVIEW ================================================================================
        //--------------------------------------------------------------------------------
        private void UpdatePreview() {
            FilePathGenerator generator = new FilePathGenerator("", memFilenamePattern.Text);
            generator.DateFormat = cboDateFormat.Text;
            generator.TimeFormat = cboTimeFormat.Text;
            generator.InputText = "[Input Text]";
            memPreview.Text = generator.GenerateFilename(PREVIEW_FILENAME);
        }


        // ADVANCED OPTIONS ================================================================================
        //--------------------------------------------------------------------------------
        private void UpdateAdvancedOptions() {
            // Custom filename
            //if (!ShowAdvancedOptions)
            //    chkCustomFilenamePattern.Checked = false;

            // Form size (done in staggered order to reduce layout flickering)
            if (ShowAdvancedOptions)
                Height = HEIGHT_WITH_ADVANCED_OPTIONS;

            // Custom filename pattern warning
            lciCustomFilenamePatternWarning.Visibility = (!ShowAdvancedOptions && chkCustomFilenamePattern.Checked ? LayoutVisibility.Always : LayoutVisibility.Never);

            // Advanced options visibility
            LayoutVisibility visibility = ShowAdvancedOptions ? LayoutVisibility.Always : LayoutVisibility.Never;
            lciAdvancedOptions1.Visibility = visibility;
            lciAdvancedOptions2.Visibility = visibility;
            lciAdvancedOptions3.Visibility = visibility;
            lciAdvancedOptions4.Visibility = visibility;
            lciAdvancedOptions5.Visibility = visibility;
            lciAdvancedOptions6.Visibility = visibility;
            lciAdvancedOptions7.Visibility = visibility;
            lciAdvancedOptions8.Visibility = visibility;
            lciAdvancedOptions9.Visibility = visibility;
            lciAdvancedOptions10.Visibility = visibility;
            lciAdvancedOptions11.Visibility = visibility;
            lciAdvancedOptions12.Visibility = visibility;
            //lciAdvancedOptions13.Visibility = visibility;
            lciAdvancedOptions14.Visibility = visibility;
            lciAdvancedOptions16.Visibility = visibility;
            lciAdvancedOptions17.Visibility = visibility;

            // Form size (done in staggered order to reduce layout flickering)
            if (!ShowAdvancedOptions)
                Height = HEIGHT_WITHOUT_ADVANCED_OPTIONS;
        }

        
        //================================================================================
        //********************************************************************************
        public class DateTimeFormatValue {
            public string format;
            public string displayFormat;

            public DateTimeFormatValue(string format, string preview) {
                this.format = format;
                this.displayFormat = format + "  (" + preview + ")";
            }

            public override string ToString() { return displayFormat; }
        }
    }

}
