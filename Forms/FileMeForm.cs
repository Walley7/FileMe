using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using FileMe.Filing;
using FileMe.Outlook;
using FileMe.Utility;
using DevExpress.XtraEditors.ColorPick.Picker;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;
using DevExpress.XtraLayout.Utils;
using System.Runtime.InteropServices;
using FileMe.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DevExpress.LookAndFeel;
using CSACore.Core;
using DevExpress.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraEditors.Controls;
using CSACore.Hotkeys;
using DevExpress.XtraGrid.Views.Tile;
using FileMe.Tutorials;



namespace FileMe.Forms {

    public partial class FileMeForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        // https://github.com/DevExpress-Examples/how-to-implement-the-dragdrop-mechanism-between-tileview-and-gridview-t234664/blob/14.2.7%2B/CS/GridSample/Form1.cs
        //================================================================================
        public const string                     DEFAULT_THEME = "Black";

        public const int                        UNLICENCED_TILE_LIMIT = 5;

        public const int                        ADVERTISING_PERIOD = 300; // Seconds
        
        //--------------------------------------------------------------------------------
        public enum ETextSize {
            SMALL,
            MEDIUM,
            LARGE
        }


        //================================================================================
        private CategoryManager                 mCategoryManager;

        private FilerManager                    mFilerManager;
        private Filer                           mSelectedFiler;

        private bool                            mLoaded = false;

        private bool                            mFirstRun = true;

        private bool                            mLayoutSaveRequired;

        private string                          mTheme = "";
        private ETextSize                       mTextSize = ETextSize.SMALL;

        private double                          mFadeOpacity = 1.0;

        private bool                            mAlwaysOnTop = true;

        private QueueForm                       mQueueForm = null;

        private DateTime                        mLastAdTime = DateTime.Now/*.AddSeconds(-ADVERTISING_PERIOD)*/;
        private AdvertisingForm                 mAdvertisingForm = null;

        private bool                            mClose = false;

        private HotkeyRegistrar                 mGlobalHotkeyRegistrar = null;
        private bool                            mGlobalHotkeyActive = false;
        private HotkeyEvent.Modifier            mGlobalHotkeyModifier = HotkeyEvent.Modifier.Ctrl;
        private Keys                            mGlobalHotkeyKey = Keys.F1;


        //================================================================================
        //--------------------------------------------------------------------------------
        public FileMeForm() {
            // Initialise
            InitializeComponent();

            // Theme
            Theme = DEFAULT_THEME;

            // Categories
            mCategoryManager = new CategoryManager();

            // Filers
            mFilerManager = new FilerManager();
            mFilerManager.FilingTaskProgressEvent += OnFilingTaskProgress;
            mFilerManager.FilingTaskCompletedEvent += OnFilingTaskCompleted;

            // Tiles
            grdTiles.DataSource = mFilerManager.Filers;

            // Settings
            Settings.Initialise(mCategoryManager, mFilerManager, this);
            Settings.LoadFilers();
            Settings.LoadCategories();

            // Refresh
            RefreshDataSources();

            // Filters
            //HideFilters();

            // Categories
            AddCategoryTabs();
            ApplyFiltering();

            // Licence
            ApplyLicence();

            // Advertising
            InitialiseAdvertising();
        }


        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void FileMeForm_Load(object sender, EventArgs e) {
            // Layout
            Settings.LoadLayout();

            // Active category
            ApplyActiveCategoryStyle(tabCategories.SelectedTabPage);
        }
        
        //--------------------------------------------------------------------------------
        private void FileMeForm_Shown(object sender, EventArgs e) {
            // Loaded
            mLoaded = true;

            // Global hotkey
            RegisterGlobalHotkey();

            // First run
            if (mFirstRun) {
                // Welcome message
                if (XtraMessageBox.Show(this, "<b>Welcome to File Me!</b>\n\n" +
                                        "The tutorial can help you get off to a quick start. Would you like to run it now?\n\n" +
                                        "You can also find it later via the ? button at the bottom.",
                                        "File Me", MessageBoxButtons.YesNo, MessageBoxIcon.Information, DefaultBoolean.True) == DialogResult.Yes) {
                    CSA.TutorialSystem.StartTutorial(new FileMeTutorial(this));
                }

                // Mark first run
                mFirstRun = false;
                Settings.SaveLayout();
            }


            Debug.WriteLine($"Category {tabCategories.AppearancePage.Header.Font.Size}");
            Debug.WriteLine($"Tile header {tlvTiles.TileTemplate.ElementAt(0).Appearance.Normal.Font.Size}");
            Debug.WriteLine($"Tile status {tlvTiles.TileTemplate.ElementAt(1).Appearance.Normal.Font.Size}");
        }

        //--------------------------------------------------------------------------------
        private void FileMeForm_FormClosed(object sender, FormClosedEventArgs e) {
            // Global hotkey
            DeregisterGlobalHotkey();

            // Filing tasks
            mFilerManager.StopAllFilingTasks();
        }

        //--------------------------------------------------------------------------------
        private void FileMeForm_FormClosing(object sender, FormClosingEventArgs e) {
            // Reset cancel state (otherwise advertising form stops us closing)
            e.Cancel = false;

            // Closing
            bool close = mClose;
            mClose = false;

            // Minimise
            if (!close) {
                e.Cancel = true;
                Hide();
                return;
            }

            // Checks
            if (mFilerManager.FilingTasks.Count() == 0)
                return;

            // Prompt
            if (XtraMessageBox.Show(this, "There are filing operations in progress - are you sure you want to close File Me?", "File Me", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;
        }
        
        //--------------------------------------------------------------------------------
        private void FileMeForm_Activated(object sender, EventArgs e) {
            /*if (mQueueForm != null && mQueueForm.Visible) {
                mQueueForm.Hide();
                mQueueForm.Show();
            }*/

            // Tutorial
            CSA.TutorialSystem.NotifyEvent("file_me_focused", this);
        }

        //--------------------------------------------------------------------------------
        private void FileMeForm_Move(object sender, EventArgs e) {
            // Settings
            if (mLoaded && Visible && WindowState != FormWindowState.Minimized)
                Settings.SaveLayout();
        }

        //--------------------------------------------------------------------------------
        private void FileMeForm_Resize(object sender, EventArgs e) {
            // Hide on minimise
            //if (WindowState == FormWindowState.Minimized)
            //    Hide();

            // Refresh tiles
            tlvTiles.RefreshData(); // Causes scroll bars to refresh correctly on resize (otherwise they seem to lag behind until the next resize)

            // Categories
            UpdateCategoriesWidth();
        }
        
        //--------------------------------------------------------------------------------
        private void FileMeForm_ResizeEnd(object sender, EventArgs e) {
            // Settings
            if (Visible && WindowState != FormWindowState.Minimized)
                Settings.SaveLayout();
        }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void OnHotkeyPressed(object sender, HotkeyEvent e) {
            // Show
            if (!Visible || WindowState == FormWindowState.Minimized) {
                Show();
                WindowState = FormWindowState.Normal;
            }

            // Foreground
            SetForegroundWindow(Handle);
        }

        //--------------------------------------------------------------------------------
        [DllImport("User32")] private static extern int SetForegroundWindow(IntPtr hwnd);

        
        // LAYOUT ================================================================================
        //--------------------------------------------------------------------------------
        private void tmrLayout_Tick(object sender, EventArgs e) {
            // Categories
            UpdateCategoriesWidth();

            // Layout
            if (mLayoutSaveRequired) {
                Settings.SaveLayout();
                mLayoutSaveRequired = false;
            }
        }


        // LICENCE ================================================================================
        //--------------------------------------------------------------------------------
        public void ApplyLicence() {
            // Activation header
            lycActivate.Visibility = CSA.Licencer.LicenceActive ? LayoutVisibility.Never : LayoutVisibility.Always;

            // Tiles
            tlvTiles.OptionsDragDrop.AllowDrag = CSA.Licencer.LicenceActive;
            tlvTiles.RefreshData();
        }
        
        //--------------------------------------------------------------------------------
        private void tmrLicenceTimer_Tick(object sender, EventArgs e) {
            // Checks
            if (!CSA.Licencer.LicenceActive)
                return;

            // Poll
            if (CSA.Licencer.PollServerValidation() && !CSA.Licencer.LicenceActive)
                ApplyLicence();
        }

        //--------------------------------------------------------------------------------
        private void lblActivate_Click(object sender, EventArgs e) {
            LicenceForm licenceForm = new LicenceForm(this);
            licenceForm.ShowDialog();
        }


        // MOUSE TRACKING ================================================================================
        //--------------------------------------------------------------------------------
        private void tmrMouseTimer_Tick(object sender, EventArgs e) {
            UpdateFade();
        }


        // TRAY ICON ================================================================================
        //--------------------------------------------------------------------------------
        private void ntiTrayIcon_MouseClick(object sender, MouseEventArgs e) {
            // This approach doesn't work - the menu can end up hidden behind the notifcation tray popup window
            //if (e.Button == MouseButtons.Right)
            //    mnuTray.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            //ntiTrayIcon.ShowBalloonTip(3000, "This is a notification test!", "Notification test.", ToolTipIcon.Info);
        }
        
        //--------------------------------------------------------------------------------
        private void ntiTrayIcon_DoubleClick(object sender, EventArgs e) {
            ShowHide();
        }
        
        //--------------------------------------------------------------------------------
        private void mnuTray_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            switch (e.ClickedItem.Text) {
                case "Show / Hide":
                    ShowHide();
                    break;

                case "Options...":
                    OptionsForm optionsForm = new OptionsForm(this);
                    if (optionsForm.ShowDialog() == DialogResult.OK && Visible && WindowState != FormWindowState.Minimized)
                        Settings.SaveLayout();
                    break;

                case "Licence...":
                    LicenceForm licenceForm = new LicenceForm(this);
                    licenceForm.ShowDialog();
                    break;

                case "Exit":
                    mClose = true;
                    Close();
                    break;
            }
        }

        //--------------------------------------------------------------------------------
        private void btnTray_ShowHide_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowHide();
        }

        //--------------------------------------------------------------------------------
        private void btnTray_Options_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            OptionsForm optionsForm = new OptionsForm(this);
            if (optionsForm.ShowDialog() == DialogResult.OK && Visible && WindowState != FormWindowState.Minimized)
                Settings.SaveLayout();
        }

        //--------------------------------------------------------------------------------
        private void btnTray_Licence_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            LicenceForm licenceForm = new LicenceForm(this);
            licenceForm.ShowDialog();
        }

        //--------------------------------------------------------------------------------
        private void btnTray_Exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            mClose = true;
            Close();
        }

        //--------------------------------------------------------------------------------
        private void ShowHide() {
            if (!Visible || WindowState == FormWindowState.Minimized) {
                Show();
                WindowState = FormWindowState.Normal;
                SetForegroundWindow(Handle);
            }
            else
                Hide();
        }


        // DATA SOURCES ================================================================================
        //--------------------------------------------------------------------------------
        private void RefreshDataSources(bool refreshTiles = false) {
            // Filers
            if (refreshTiles)
                grdTiles.RefreshDataSource(); // No longer needed thanks to using Bindable with filers

            // Filters
            cboGroupFilter.Properties.Items.Clear();
            cboGroupFilter.Properties.Items.AddRange(mFilerManager.FilerGroups);
            if (!mFilerManager.FilerGroups.Contains(cboGroupFilter.Text))
                cboGroupFilter.Text = "";

            // Getting started
            //lciGettingStarted.Visibility = mFilerManager.Filers.Count == 0 ? LayoutVisibility.Always : LayoutVisibility.Never;
            lciGettingStarted.Visibility = LayoutVisibility.Never;

            // Queue form - hacky way of getting data to refresh in queue form
            if (mQueueForm != null && mQueueForm.Visible) {
                mQueueForm.Focus();
                Focus();
            }
        }


        // TILES GRID ================================================================================
        //--------------------------------------------------------------------------------
        private void tlvTiles_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e) {
            // Filer
            Filer filer = (Filer)tlvTiles.GetRow(e.RowHandle);
            if (filer == null)
                return;

            // Background
            e.Item.AppearanceItem.Normal.BackColor = UColor.Darken(filer.Colour, 10);
            e.Item.AppearanceItem.Normal.BackColor2 = filer.Colour;
            e.Item.AppearanceItem.Normal.BorderColor = UColor.Brighten(filer.Colour, 20);
            
            // Text
            e.Item.AppearanceItem.Normal.ForeColor = (UColor.WeightedBrightness(filer.Colour) > 0.5 ? Color.Black : Color.White);

            // Activation
            if (!CSA.Licencer.LicenceActive && mFilerManager.Filers.IndexOf(filer) >= UNLICENCED_TILE_LIMIT) { // e.RowHandle >= UNLICENCED_TILE_LIMIT
                // Icon
                e.Item.GetElementByName("grdTiles_Lock").ImageVisible = true;

                // Background
                e.Item.AppearanceItem.Normal.BackColor = Color.FromArgb((int)(e.Item.AppearanceItem.Normal.BackColor.R * 0.25f), (int)(e.Item.AppearanceItem.Normal.BackColor.G * 0.25f), (int)(e.Item.AppearanceItem.Normal.BackColor.B * 0.25f));
                e.Item.AppearanceItem.Normal.BackColor2 = Color.FromArgb((int)(e.Item.AppearanceItem.Normal.BackColor2.R * 0.25f), (int)(e.Item.AppearanceItem.Normal.BackColor2.G * 0.25f), (int)(e.Item.AppearanceItem.Normal.BackColor2.B * 0.25f));
                e.Item.AppearanceItem.Normal.BorderColor = Color.FromArgb((int)(e.Item.AppearanceItem.Normal.BorderColor.R * 0.25f), (int)(e.Item.AppearanceItem.Normal.BorderColor.G * 0.25f), (int)(e.Item.AppearanceItem.Normal.BorderColor.B * 0.25f));
            
                // Text
                e.Item.AppearanceItem.Normal.ForeColor = Color.Black;
            }
        }

        //--------------------------------------------------------------------------------
        private void grdTiles_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                // Selected item
                TileViewHitInfo hitInfo = tlvTiles.CalcHitInfo(new Point(e.X, e.Y));
                mSelectedFiler = hitInfo.InItem ? (Filer)tlvTiles.GetRow(hitInfo.RowHandle) : null;
                //mSelectedTileGroup = hitInfo.InGroup ? hitInfo.GroupInfo.Group : null;
                //mSelectedTileItem = null;
                //if (hitInfo.InItem) {
                //    mSelectedTileGroup = hitInfo.ItemInfo.Item.Group;
                //    mSelectedTileItem = hitInfo.ItemInfo.Item;
                //}

                // Menu
                btnTiles_Remove.Enabled = hitInfo.InItem;
                btnTiles_Edit.Enabled = hitInfo.InItem;
                mnuTiles.ShowPopup(new Point(Cursor.Position.X - 14, Cursor.Position.Y - 12));
            }
        }

        //--------------------------------------------------------------------------------
        private void tlvTiles_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e) {
            //EditFiler((Filer)tlvTiles.GetRow(e.Item.RowHandle));
            BrowseToPath((Filer)tlvTiles.GetRow(e.Item.RowHandle));
        }

        //--------------------------------------------------------------------------------
        private void tlvTiles_ItemDrop(object sender, DevExpress.XtraGrid.Views.Tile.ItemDropEventArgs e) {
            // Save
            Settings.SaveFilers();
        }

        //--------------------------------------------------------------------------------
        private void grdTiles_DragOver(object sender, DragEventArgs e) {
            // Reset
            e.Effect = DragDropEffects.None;

            // Effect
            TileViewHitInfo hitInfo = tlvTiles.CalcHitInfo(grdTiles.PointToClient(new Point(e.X, e.Y)));
            if (hitInfo.InItem)
                e.Effect = DragDropEffects.Link;
        }

        //--------------------------------------------------------------------------------
        private void grdTiles_DragDrop(object sender, DragEventArgs e) {
            // Hit info
            TileViewHitInfo hitInfo = tlvTiles.CalcHitInfo(grdTiles.PointToClient(new Point(e.X, e.Y)));
            if (hitInfo.InItem) {
                // Licencing
                if (!CSA.Licencer.LicenceActive && hitInfo.RowHandle >= UNLICENCED_TILE_LIMIT)
                    return;

                // Advertising
                ShowAdvertising();

                // File
                Filer filer = (Filer)tlvTiles.GetRow(hitInfo.RowHandle);
                try { filer.File(e.Data); }
                catch (Exception ex) { XtraMessageBox.Show(this, "Failed to file: " + ex.Message, "File Me", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                // Warnings
                if (filer.HasWarnings) {
                    foreach (string w in filer.Warnings) {
                        XtraMessageBox.Show(this, w, "File Me", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                // Tutorial
                CSA.TutorialSystem.NotifyEvent("filing_started", this);
            }
        }

        //--------------------------------------------------------------------------------
        public Size TileSize {
            set { tlvTiles.OptionsTiles.ItemSize = value; }
            get { return tlvTiles.OptionsTiles.ItemSize; }
        }


        // TILE CONTEXT BUTTONS ================================================================================
        //--------------------------------------------------------------------------------
        private void tlvTiles_ContextButtonClick(object sender, ContextItemClickEventArgs e) {
            // Filer
            Filer filer = (Filer)tlvTiles.GetRow(((TileViewItem)e.DataItem).RowHandle);

            // Action
            switch (e.Item.Name) {
                case "tlvTiles_Edit":   EditFiler(filer); break;
                case "tlvTiles_Remove": RemoveFiler(filer); break;
            }
        }


        // TILE ANIMATIONS ================================================================================
        //--------------------------------------------------------------------------------
        private void tmrTileAnimations_Tick(object sender, EventArgs e) {
            mFilerManager.UpdateFilers(1.0f / tmrTileAnimations.Interval);
        }


        // TILE MENU EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnTiles_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddFiler(); }
        private void btnTiles_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { EditFiler(mSelectedFiler); }
        private void btnTiles_Remove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { RemoveFiler(mSelectedFiler); }
        private void btnTiles_Copy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddFiler(mSelectedFiler); }
        private void btnTiles_BrowseToPath_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { BrowseToPath(mSelectedFiler); }
        private void btnTiles_Import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { ImportFilers(); }
        private void btnTiles_Export_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { ExportFilers(); }


        // FILERS ================================================================================
        //--------------------------------------------------------------------------------
        private void AddFiler(Filer filerToCopy = null) {
            // Filer form
            FilerForm filerForm = new FilerForm(this, mFilerManager, filerToCopy, filerToCopy != null);
            if (filerForm.ShowDialog() != DialogResult.OK)
                return;

            // Filer
            Filer filer = mFilerManager.CreateFiler();
            filerForm.Apply(filer);

            // Save
            Settings.SaveFilers();

            // Refresh
            RefreshDataSources(true);

            // Tutorial
            CSA.TutorialSystem.NotifyEvent("filer_added", this);
        }

        //--------------------------------------------------------------------------------
        private void RemoveFiler(Filer filer) {
            // Prompt
            if (XtraMessageBox.Show(this, $"Remove the filer '{filer.Name}'?", "File Me", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Destroy
            mFilerManager.DestroyFiler(filer);

            // Save
            Settings.SaveFilers();

            // Refresh
            RefreshDataSources();
        }

        //--------------------------------------------------------------------------------
        private void EditFiler(Filer filer) {
            // Filer form
            FilerForm filerForm = new FilerForm(this, mFilerManager, filer);
            if (filerForm.ShowDialog() != DialogResult.OK)
                return;

            // Apply
            filerForm.Apply(filer);

            // Save
            Settings.SaveFilers();

            // Refresh
            RefreshDataSources(true);
        }
        
        //--------------------------------------------------------------------------------
        private void BrowseToPath(Filer filer) {
            // Browse
            try { filer.BrowseToPath(); }
            catch (Exception ex) {
                XtraMessageBox.Show(this, "Failed to browse to path: " + (ex.Message.Equals("The system cannot find the file specified") ? "Folder does not exist yet" : ex.Message),
                                    "File Me", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Tutorial
            CSA.TutorialSystem.NotifyEvent("browse_to_path", this);
        }

        //--------------------------------------------------------------------------------
        private void OnFilingTaskProgress(FilingTask task) {
            int taskCount = mFilerManager.FilingTasks.Count;
            btnQueue.Text = "Queue" + (taskCount > 0 ? " (" + taskCount + ")" : "");
        }

        //--------------------------------------------------------------------------------
        private void OnFilingTaskCompleted(FilingTask task, Exception exception) {
            // Queue
            int taskCount = mFilerManager.FilingTasks.Count;
            btnQueue.Text = "Queue" + (taskCount > 0 ? " (" + taskCount + ")" : "");

            // Exception
            if (exception != null && !(exception is DirectoryNotFoundException))
                XtraMessageBox.Show(this, "Filing failed: " + exception.Message, "File Me", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        // FILER IMPORTING / EXPORTING ================================================================================
        //--------------------------------------------------------------------------------
        private void ImportFilers() {
            // Import
            FilerImportForm importForm = new FilerImportForm(this, mFilerManager);
            importForm.ShowDialog();

            // Save
            Settings.SaveFilers();
        }
        
        //--------------------------------------------------------------------------------
        private void ExportFilers() {
            // Checks
            if (mFilerManager.Filers.Count == 0) {
                XtraMessageBox.Show(this, "Nothing to export", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Export
            FilerExportForm exportForm = new FilerExportForm(this, mFilerManager);
            exportForm.ShowDialog();
        }


        // FILTERING ================================================================================
        //--------------------------------------------------------------------------------
        private void ApplyFiltering() {
            // Filter string
            string filterString = "1=1";

            // Search / group / filter
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                filterString += $" and ([Name] like '%{txtSearch.Text.Replace("'", "''")}%' or [Group] like '%{txtSearch.Text.Replace("'", "''")}%')";
            if (!string.IsNullOrWhiteSpace(cboGroupFilter.Text))
                filterString += " and [Group] = '" + cboGroupFilter.Text.Replace("'", "''") + "'";
            if (!string.IsNullOrWhiteSpace(txtFilter.Text))
                filterString += " and ([Name] like '%" + txtFilter.Text.Replace("'", "''") + "%' or [Group] like '%" + txtFilter.Text.Replace("'", "''") + "%')";

            // Category
            if (tabCategories.SelectedTabPage != tabCategories_All && tabCategories.SelectedTabPage != null) {
                Category category = mCategoryManager.Category(tabCategories.SelectedTabPage);

                if (category.GroupStartsWithEnabled && !string.IsNullOrWhiteSpace(category.GroupStartsWith))
                    filterString += " and [Group] like '" + category.GroupStartsWith.Replace("'", "''") + "%'";

                if (category.Groups.Count > 0) {
                    filterString += " and (";
                    for (int i = 0; i < category.Groups.Count; ++i) {
                        filterString += (i > 0 ? " or " : "") + "[Group] = '" + category.Groups[i].Replace("'", "''") + "'";
                    }
                    filterString += ")";
                }
            }

            // Apply
            tlvTiles.ActiveFilterString = filterString;
        }
   
        
        // FILTERING - CONTROLS ================================================================================
        //--------------------------------------------------------------------------------
        private void txtSearch_TextChanged(object sender, EventArgs e) { ApplyFiltering(); }
        private void cboGroupFilter_TextChanged(object sender, EventArgs e) { ApplyFiltering(); }
        private void txtFilter_TextChanged(object sender, EventArgs e) { ApplyFiltering(); }

        //--------------------------------------------------------------------------------
        private void btnClearGroupFilter_Click(object sender, EventArgs e) {
            cboGroupFilter.Text = "";
        }

        //--------------------------------------------------------------------------------
        private void btnClearFilter_Click(object sender, EventArgs e) {
            txtFilter.Text = "";
        }

        //--------------------------------------------------------------------------------
        private void btnShowFilters_Click(object sender, EventArgs e) { ShowFilters(); }
        private void btnHideFilters_Click(object sender, EventArgs e) { HideFilters(); }

        //--------------------------------------------------------------------------------
        private void ShowFilters() {
            lciFilters.Visibility = LayoutVisibility.Always;
            lciHideFilters.Visibility = LayoutVisibility.Never;
        }

        //--------------------------------------------------------------------------------
        private void HideFilters() {
            lciFilters.Visibility = LayoutVisibility.Never;
            lciHideFilters.Visibility = LayoutVisibility.Always;
            grdTiles.Focus();
        }


        // CATEGORY CONTROLS ================================================================================        
        //--------------------------------------------------------------------------------
        private void tabCategories_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                mnuCategories_Remove.Enabled = (tabCategories.SelectedTabPage != tabCategories_All);
                mnuCategories_Edit.Enabled = (tabCategories.SelectedTabPage != tabCategories_All);
                mnuCategories.ShowPopup(new Point(Cursor.Position.X - 14, Cursor.Position.Y - 12));
            }
        }
        
        //--------------------------------------------------------------------------------
        private void tabCategories_CustomHeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventArgs e) {
            if (e.Button.Kind == ButtonPredefines.Plus)
                AddCategory();
        }

        //--------------------------------------------------------------------------------
        private void btnAddCategory_Click(object sender, EventArgs e) { AddCategory(); }
        
        //--------------------------------------------------------------------------------
        private void tabCategories_Selected(object sender, TabPageEventArgs e) {
            ApplyInactiveCategoryStyle(tabCategories.SelectedTabPage);
            ApplyActiveCategoryStyle(e.Page);
        }
        
        //--------------------------------------------------------------------------------
        private void tabCategories_SelectedPageChanged(object sender, TabPageChangedEventArgs e) {
            ApplyFiltering();
        }

        //--------------------------------------------------------------------------------
        private void splCategories_PositionChanged(object sender, EventArgs e) {
            UpdateCategoriesWidth();
        }
        
        //--------------------------------------------------------------------------------
        private void UpdateCategoriesWidth() {
            int previousWidth = tabCategories.TabPageWidth;
            tabCategories.TabPageWidth = lciCategories.Width - 16 - 1;
            if (tabCategories.TabPageWidth != previousWidth)
                mLayoutSaveRequired = true;
        }
        
        //--------------------------------------------------------------------------------
        private void SetCategoriesWidth(int width, bool applyRecursively = false) {
            // Width
            width = Math.Max(width, lciCategoriesBase.MinSize.Width);
            if (lciCategoriesBase.MaxSize.Width > 0 && width < lciCategoriesBase.MaxSize.Width)
                width = lciCategoriesBase.MaxSize.Width;

            // Apply
            if (!applyRecursively)
                lciCategoriesBase.Width = width;
            else {
                while (lciCategoriesBase.Width != width) {
                    lciCategoriesBase.Width = width;
                    lycLayout.Invalidate();
                }
            }
        }

        
        // CATEGORY MENU EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void mnuCategories_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { AddCategory(); }

        //--------------------------------------------------------------------------------
        private void mnuCategories_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (tabCategories.SelectedTabPage != tabCategories_All)
                EditCategory(mCategoryManager.Category(tabCategories.SelectedTabPage));
        }
        
        //--------------------------------------------------------------------------------
        private void mnuCategories_Remove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (tabCategories.SelectedTabPage != tabCategories_All)
                RemoveCategory(mCategoryManager.Category(tabCategories.SelectedTabPage));
        }
        
        //--------------------------------------------------------------------------------
        private void mnuCategories_Import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { ImportCategories(); }
        private void mnuCategories_Export_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { ExportCategories(); }

        
        // CATEGORIES ================================================================================
        //--------------------------------------------------------------------------------
        private void AddCategory() {
            // Category form
            CategoryForm categoryForm = new CategoryForm(this, mFilerManager, mCategoryManager, null);
            if (categoryForm.ShowDialog() != DialogResult.OK)
                return;

            // Category
            Category category = mCategoryManager.CreateCategory();
            categoryForm.Apply(category);
            mCategoryManager.SortCategories();

            // Tabs
            AddCategoryTabs();

            // Save
            Settings.SaveCategories();
        }
            
        //--------------------------------------------------------------------------------
        private void RemoveCategory(Category category) {
            // Prompt
            if (XtraMessageBox.Show(this, $"Remove the category '{category.Name}'?", "File Me", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Destroy
            mCategoryManager.DestroyCategory(category);

            // Tabs
            RemoveCategoryTab(category);

            // Save
            Settings.SaveCategories();
        }

        //--------------------------------------------------------------------------------
        private void EditCategory(Category category) {
            // Category form
            CategoryForm categoryForm = new CategoryForm(this, mFilerManager, mCategoryManager, category);
            if (categoryForm.ShowDialog() != DialogResult.OK)
                return;

            // Category
            categoryForm.Apply(category);
            mCategoryManager.SortCategories();

            // Tabs
            UpdateCategoryTab(category);

            // Save
            Settings.SaveCategories();
        }


        // CATEGORY IMPORTING / EXPORTING ================================================================================
        //--------------------------------------------------------------------------------
        private void ImportCategories() {
            // Import
            CategoryImportForm importForm = new CategoryImportForm(this, mCategoryManager);
            importForm.ShowDialog();

            // Tabs
            AddCategoryTabs();

            // Save
            Settings.SaveCategories();
        }
        
        //--------------------------------------------------------------------------------
        private void ExportCategories() {
            // Checks
            if (mCategoryManager.Categories.Count == 0) {
                XtraMessageBox.Show(this, "Nothing to export", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Export
            CategoryExportForm exportForm = new CategoryExportForm(this, mCategoryManager);
            exportForm.ShowDialog();
        }

        
        // CATEGORY TABS ================================================================================
        //--------------------------------------------------------------------------------
        private void AddCategoryTabs() {
            // Add
            for (int i = 0; i < mCategoryManager.Categories.Count; ++i) {
                // Category
                Category category = mCategoryManager.Categories[i];

                // Add
                if (category.TabPage == null) {
                    XtraTabPage tabPage = new XtraTabPage();
                    tabCategories.TabPages.Insert(i + 1, tabPage);
                    category.TabPage = tabPage;

                    // Appearance
                    category.TabPage.Text = category.Name;
                    category.TabPage.Appearance.Header.BackColor = category.Colour;
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        private void RemoveCategoryTabs() {
            // Remove
            int i = 1; // Skip the all tab
            while (true) {
                if (i >= tabCategories.TabPages.Count)
                    break;
                if (!mCategoryManager.HasCategory(tabCategories.TabPages[i]))
                    tabCategories.TabPages.RemoveAt(i);
                else
                    ++i;
            }
        }

        //--------------------------------------------------------------------------------
        private void RemoveCategoryTab(Category category) {
            if (category.TabPage != null)
                tabCategories.TabPages.Remove(category.TabPage);
            ApplyActiveCategoryStyle(tabCategories.SelectedTabPage);
        }

        //--------------------------------------------------------------------------------
        private void UpdateCategoryTab(Category category) {
            if (category.TabPage != null) {
                // Selection
                bool wasSelected = tabCategories.SelectedTabPage == category.TabPage;
                if (wasSelected)
                    ApplyInactiveCategoryStyle(category.TabPage);

                // Reposition
                tabCategories.TabPages.Remove(category.TabPage);
                if (wasSelected)
                    ApplyActiveCategoryStyle(tabCategories.SelectedTabPage);
                tabCategories.TabPages.Insert(mCategoryManager.Categories.IndexOf(category) + 1, category.TabPage);

                // Appearance
                category.TabPage.Text = category.Name;
                category.TabPage.Appearance.Header.BackColor = category.Colour;

                // Selection
                if (wasSelected)
                    tabCategories.SelectedTabPage = category.TabPage;
            }
        }

        //--------------------------------------------------------------------------------
        private void ApplyActiveCategoryStyle(XtraTabPage tabPage) {
            tabPage.Text = "█ " + tabPage.Text;
            tabPage.Appearance.Header.Font = new Font(tabPage.Appearance.Header.Font, FontStyle.Bold);
        }

        //--------------------------------------------------------------------------------
        private void ApplyInactiveCategoryStyle(XtraTabPage tabPage) {
            tabPage.Text = tabPage.Text.Substring(2, tabPage.Text.Length - 2);
            tabPage.Appearance.Header.Font = new Font(tabPage.Appearance.Header.Font, FontStyle.Regular);
        }

        
        // BOTTOM BUTTONS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e) { AddFiler(); }
        
        //--------------------------------------------------------------------------------
        private void btnQueue_Click(object sender, EventArgs e) {
            // Existing form
            if (mQueueForm != null && mQueueForm.Visible) {
                mQueueForm.Focus();
                return;
            }

            // Queue form
            mQueueForm = new QueueForm(this, mFilerManager);
            mQueueForm.Show(this);
            //mQueueForm.Show();
        }
        
        //--------------------------------------------------------------------------------
        private void btnHelp_Click(object sender, EventArgs e) { ShowHelp(); }
        
        //--------------------------------------------------------------------------------
        private void btnOptions_Click(object sender, EventArgs e) {
            OptionsForm optionsForm = new OptionsForm(this);
            if (optionsForm.ShowDialog() == DialogResult.OK)
                Settings.SaveLayout();
        }


        // HELP ================================================================================
        //--------------------------------------------------------------------------------
        public void ShowHelp(bool fromTutorialEnd = false) {
            HelpForm helpForm = new HelpForm(fromTutorialEnd);
            helpForm.ShowDialog(this);
        }


        // ADVERTISING ================================================================================
        //--------------------------------------------------------------------------------
        private void InitialiseAdvertising() {
            // Checks
            if (CSA.Licencer.LicenceActive)
                return;

            // Initialise
            mAdvertisingForm = new AdvertisingForm();
            mAdvertisingForm.Show(this);
            mAdvertisingForm.Hide();
        }

        //--------------------------------------------------------------------------------
        public void ShowAdvertising() {
            // Checks
            if (CSA.Licencer.LicenceActive)
                return;

            // Timing
            DateTime now = DateTime.Now;
            if ((now - mLastAdTime).TotalSeconds < ADVERTISING_PERIOD)
                return;
            mLastAdTime = now;

            // Existing form
            if (mAdvertisingForm != null) {
                if (mAdvertisingForm.Visible)
                    mAdvertisingForm.Focus();
                else
                    mAdvertisingForm.Show(this);
                return;
            }
            
            // New form
            InitialiseAdvertising();
            mAdvertisingForm.Show(this);
        }


        // APPEARANCE ================================================================================
        //--------------------------------------------------------------------------------
        public string Theme {
            set {
                mTheme = value;
                UserLookAndFeel.Default.SetSkinStyle(DevExpressThemeName(mTheme));
            }
            get { return mTheme; }
        }
        
        //--------------------------------------------------------------------------------
        private string DevExpressThemeName(string theme) {
            switch (theme) {
                case "Black":       return "Office 2016 Black";
                case "Grey":        return "Office 2016 Dark";
                case "White":       return "Office 2016 Colorful";
                case "Smooth Grey": return "DevExpress Dark Style";
                default:            return theme;
            }
        }

        //--------------------------------------------------------------------------------
        public ETextSize TextSize {
            set {
                // Text size
                mTextSize = value;
                float increaseFactor = 0.0f;
                switch (mTextSize) {
                    case ETextSize.SMALL:   increaseFactor = 0.0f; break;
                    case ETextSize.MEDIUM:  increaseFactor = 1.0f; break;
                    case ETextSize.LARGE:   increaseFactor = 2.0f; break;
                }

                // Categories
                Font font = tabCategories.AppearancePage.Header.Font;
                tabCategories.AppearancePage.Header.Font = new Font(font.FontFamily, 7.0f + 1.75f * increaseFactor, font.Style);

                font = tabCategories_All.Appearance.Header.Font;
                tabCategories_All.Appearance.Header.Font = new Font(font.FontFamily, 7.0f + 1.75f * increaseFactor, font.Style);

                foreach (XtraTabPage p in tabCategories.TabPages) {
                    font = p.Appearance.Header.Font;
                    p.Appearance.Header.Font = new Font(font.FontFamily, 7.0f + 1.75f * increaseFactor, font.Style);
                }

                // Tiles
                font = tlvTiles.Appearance.GroupText.Font;
                tlvTiles.Appearance.GroupText.Font = new Font(font.FontFamily, 11.0f + 1.0f * increaseFactor, font.Style);

                font = tlvTiles.TileTemplate.ElementAt(0).Appearance.Normal.Font;
                tlvTiles.TileTemplate.ElementAt(0).Appearance.Normal.Font = new Font(font.FontFamily, 8.25f + 2.0f * increaseFactor, font.Style);

                font = tlvTiles.TileTemplate.ElementAt(1).Appearance.Normal.Font;
                tlvTiles.TileTemplate.ElementAt(1).Appearance.Normal.Font = new Font(font.FontFamily, 6.5f + 1.5f * increaseFactor, font.Style);
            }
            get { return mTextSize; }
        }

        //--------------------------------------------------------------------------------
        public string TextSizeString {
            set {
                switch (value) {
                    case "Small":   TextSize = FileMeForm.ETextSize.SMALL; break;
                    case "Medium":  TextSize = FileMeForm.ETextSize.MEDIUM; break;
                    case "Large":   TextSize = FileMeForm.ETextSize.LARGE; break;
                    default:        TextSize = FileMeForm.ETextSize.SMALL; break;
                }
            }
            get {
                switch (TextSize) {
                    case FileMeForm.ETextSize.SMALL:    return "Small";
                    case FileMeForm.ETextSize.MEDIUM:   return "Medium";
                    case FileMeForm.ETextSize.LARGE:    return "Large";
                    default:                            return "Small";
                }
            }
        }


        // FADING ================================================================================
        //--------------------------------------------------------------------------------
        private void UpdateFade() {
            // Top left corner
            Point topLeftPosition = PointToClient(Location);

            // Cursor
            Point cursorPosition = PointToClient(MousePosition);
            if (cursorPosition.X >= topLeftPosition.X && cursorPosition.X < topLeftPosition.X + Width &&
                cursorPosition.Y >= topLeftPosition.Y && cursorPosition.Y < topLeftPosition.Y + Height)
            {
                Opacity = 1.0;
            }                
            else
                Opacity = mFadeOpacity;
        }

        //--------------------------------------------------------------------------------
        public double FadeOpacity {
            set {
                mFadeOpacity = value;
                UpdateFade();
            }
            get { return mFadeOpacity; }
        }


        // ALWAYS ON TOP ================================================================================
        //--------------------------------------------------------------------------------
        public bool AlwaysOnTop {
            set {
                mAlwaysOnTop = value;
                TopMost = mAlwaysOnTop;
            }
            get { return mAlwaysOnTop; }
        }


        // GLOBAL HOTKEY ================================================================================
        //--------------------------------------------------------------------------------
        public void RegisterGlobalHotkey() {
            // Deregister
            DeregisterGlobalHotkey();

            // Register
            if (mGlobalHotkeyActive) {
                mGlobalHotkeyRegistrar = new HotkeyRegistrar();
                mGlobalHotkeyRegistrar.KeyPressedEvent += new EventHandler<HotkeyEvent>(OnHotkeyPressed);
                mGlobalHotkeyRegistrar.RegisterHotkey(mGlobalHotkeyModifier, mGlobalHotkeyKey);
            }
        }

        //--------------------------------------------------------------------------------
        public void DeregisterGlobalHotkey() {
            if (mGlobalHotkeyRegistrar != null) {
                mGlobalHotkeyRegistrar.Dispose();
                mGlobalHotkeyRegistrar = null;
            }
        }
        
        //--------------------------------------------------------------------------------
        public void SetGlobalHotkey(bool active, HotkeyEvent.Modifier modifier = HotkeyEvent.Modifier.None, Keys key = Keys.None) {
            mGlobalHotkeyActive = active;
            mGlobalHotkeyModifier = modifier;
            mGlobalHotkeyKey = key;
        }

        //--------------------------------------------------------------------------------
        public bool GlobalHotkeyActive { get { return mGlobalHotkeyActive; } }
        public HotkeyEvent.Modifier GlobalHotkeyModifier { get { return mGlobalHotkeyModifier; } }
        public Keys GlobalHotkeyKey { get { return mGlobalHotkeyKey; } }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        public void SaveSettings(JsonTextWriter writer) {
            // First run
            writer.WritePropertyName("FirstRun"); writer.WriteValue(mFirstRun);

            // Location
            writer.WritePropertyName("X"); writer.WriteValue(Location.X);
            writer.WritePropertyName("Y"); writer.WriteValue(Location.Y);

            // Size
            writer.WritePropertyName("Width"); writer.WriteValue(Size.Width);
            writer.WritePropertyName("Height"); writer.WriteValue(Size.Height);

            // Categories
            writer.WritePropertyName("CategoriesWidth"); writer.WriteValue(lciCategoriesBase.Width);

            // Tiles
            writer.WritePropertyName("TileWidth"); writer.WriteValue(TileSize.Width);
            writer.WritePropertyName("TileHeight"); writer.WriteValue(TileSize.Height);

            // Theme
            writer.WritePropertyName("Theme"); writer.WriteValue(Theme);
            writer.WritePropertyName("TextSize"); writer.WriteValue(TextSize);

            // Fade opacity
            writer.WritePropertyName("FadeOpacity"); writer.WriteValue(FadeOpacity);

            // Always on top
            writer.WritePropertyName("AlwaysOnTop"); writer.WriteValue(AlwaysOnTop);

            // Global hotkey
            writer.WritePropertyName("GlobalHotkeyActive"); writer.WriteValue(GlobalHotkeyActive);
            writer.WritePropertyName("GlobalHotkeyModifier"); writer.WriteValue((int)GlobalHotkeyModifier);
            writer.WritePropertyName("GlobalHotkeyKey"); writer.WriteValue((int)GlobalHotkeyKey);
        }
        
        //--------------------------------------------------------------------------------
        public void LoadSettings(JToken token) {
            // First run
            mFirstRun = (bool)(token.SelectToken("FirstRun") ?? true);

            // Location
            int x = (int)token.SelectToken("X");
            int y = (int)token.SelectToken("Y");
            Location = new Point(x, y);

            // Size
            int width = (int)token.SelectToken("Width");
            int height = (int)token.SelectToken("Height");
            Size = new Size(width, height);

            // Categories
            SetCategoriesWidth((int)(token.SelectToken("CategoriesWidth") ?? 101), true);

            // Tiles
            int tileWidth = (int)token.SelectToken("TileWidth");
            int tileHeight = (int)token.SelectToken("TileHeight");
            TileSize = new Size(tileWidth, tileHeight);

            // Appearance
            Theme = (string)token.SelectToken("Theme");
            TextSize = (ETextSize)(int)(token.SelectToken("TextSize") ?? 0);

            // Fade opacity
            FadeOpacity = (double)token.SelectToken("FadeOpacity");

            // Always on top
            AlwaysOnTop = (bool)(token.SelectToken("AlwaysOnTop") ?? false);

            // Global hotkey
            bool globalHotkeyActive = (bool)(token.SelectToken("GlobalHotkeyActive") ?? true);
            HotkeyEvent.Modifier globalHotkeyModifier = (HotkeyEvent.Modifier)((int?)token.SelectToken("GlobalHotkeyModifier") ?? (int?)HotkeyEvent.Modifier.Ctrl);
            Keys globalHotkeyKey = (Keys)((int?)token.SelectToken("GlobalHotkeyKey") ?? (int?)Keys.F1);
            SetGlobalHotkey(globalHotkeyActive, globalHotkeyModifier, globalHotkeyKey);
        }
    }

}
