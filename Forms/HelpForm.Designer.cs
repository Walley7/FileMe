namespace FileMe.Forms {
    partial class HelpForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.navMain = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.nvpTutorial = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.btnRunTutorial = new DevExpress.XtraEditors.SimpleButton();
            this.btnEndTutorial = new DevExpress.XtraEditors.SimpleButton();
            this.nvpTutorialCompleted = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.nvpHelp = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblHelp = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lblTutorial = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.navMain)).BeginInit();
            this.navMain.SuspendLayout();
            this.nvpTutorial.SuspendLayout();
            this.nvpTutorialCompleted.SuspendLayout();
            this.nvpHelp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // navMain
            // 
            this.navMain.Controls.Add(this.nvpTutorial);
            this.navMain.Controls.Add(this.nvpTutorialCompleted);
            this.navMain.Controls.Add(this.nvpHelp);
            this.navMain.Location = new System.Drawing.Point(112, 12);
            this.navMain.Name = "navMain";
            this.navMain.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.nvpTutorial,
            this.nvpTutorialCompleted,
            this.nvpHelp});
            this.navMain.SelectedPage = this.nvpTutorial;
            this.navMain.Size = new System.Drawing.Size(716, 568);
            this.navMain.TabIndex = 0;
            this.navMain.Text = "navigationFrame1";
            this.navMain.TransitionAnimationProperties.FrameCount = 200;
            this.navMain.TransitionType = DevExpress.Utils.Animation.Transitions.Fade;
            // 
            // nvpTutorial
            // 
            this.nvpTutorial.Controls.Add(this.btnRunTutorial);
            this.nvpTutorial.Controls.Add(this.btnEndTutorial);
            this.nvpTutorial.Name = "nvpTutorial";
            this.nvpTutorial.Size = new System.Drawing.Size(716, 568);
            // 
            // btnRunTutorial
            // 
            this.btnRunTutorial.Appearance.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnRunTutorial.Appearance.Options.UseFont = true;
            this.btnRunTutorial.Location = new System.Drawing.Point(198, 3);
            this.btnRunTutorial.Name = "btnRunTutorial";
            this.btnRunTutorial.Size = new System.Drawing.Size(322, 89);
            this.btnRunTutorial.TabIndex = 1;
            this.btnRunTutorial.Text = "Run Tutorial";
            this.btnRunTutorial.Visible = false;
            this.btnRunTutorial.Click += new System.EventHandler(this.btnRunTutorial_Click);
            // 
            // btnEndTutorial
            // 
            this.btnEndTutorial.Appearance.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnEndTutorial.Appearance.Options.UseFont = true;
            this.btnEndTutorial.Location = new System.Drawing.Point(198, 3);
            this.btnEndTutorial.Name = "btnEndTutorial";
            this.btnEndTutorial.Size = new System.Drawing.Size(322, 89);
            this.btnEndTutorial.TabIndex = 2;
            this.btnEndTutorial.Text = "End Tutorial";
            this.btnEndTutorial.Visible = false;
            this.btnEndTutorial.Click += new System.EventHandler(this.btnEndTutorial_Click);
            // 
            // nvpTutorialCompleted
            // 
            this.nvpTutorialCompleted.Controls.Add(this.labelControl1);
            this.nvpTutorialCompleted.Name = "nvpTutorialCompleted";
            this.nvpTutorialCompleted.Size = new System.Drawing.Size(716, 568);
            // 
            // labelControl1
            // 
            this.labelControl1.AllowHtmlString = true;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(10);
            this.labelControl1.Size = new System.Drawing.Size(716, 568);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = resources.GetString("labelControl1.Text");
            // 
            // nvpHelp
            // 
            this.nvpHelp.Controls.Add(this.labelControl2);
            this.nvpHelp.Name = "nvpHelp";
            this.nvpHelp.Size = new System.Drawing.Size(716, 568);
            // 
            // labelControl2
            // 
            this.labelControl2.AllowHtmlString = true;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.labelControl2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl2.Location = new System.Drawing.Point(0, 0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Padding = new System.Windows.Forms.Padding(10);
            this.labelControl2.Size = new System.Drawing.Size(716, 568);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = resources.GetString("labelControl2.Text");
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblHelp);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.lblTutorial);
            this.layoutControl1.Controls.Add(this.navMain);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(840, 622);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblHelp
            // 
            this.lblHelp.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblHelp.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
            this.lblHelp.Appearance.Options.UseFont = true;
            this.lblHelp.Appearance.Options.UseForeColor = true;
            this.lblHelp.Location = new System.Drawing.Point(12, 38);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(44, 22);
            this.lblHelp.StyleController = this.layoutControl1;
            this.lblHelp.TabIndex = 7;
            this.lblHelp.Text = "Help";
            this.lblHelp.Click += new System.EventHandler(this.lblHelp_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(737, 584);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 26);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            // 
            // lblTutorial
            // 
            this.lblTutorial.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblTutorial.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
            this.lblTutorial.Appearance.Options.UseFont = true;
            this.lblTutorial.Appearance.Options.UseForeColor = true;
            this.lblTutorial.Location = new System.Drawing.Point(12, 12);
            this.lblTutorial.Name = "lblTutorial";
            this.lblTutorial.Size = new System.Drawing.Size(96, 22);
            this.lblTutorial.StyleController = this.layoutControl1;
            this.lblTutorial.TabIndex = 6;
            this.lblTutorial.Text = "Tutorial";
            this.lblTutorial.Click += new System.EventHandler(this.lblTutorial_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(840, 622);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.navMain;
            this.layoutControlItem1.Location = new System.Drawing.Point(100, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(720, 572);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblTutorial;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 52);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(100, 520);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 572);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(725, 30);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnClose;
            this.layoutControlItem5.Location = new System.Drawing.Point(725, 572);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 30);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(80, 30);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(95, 30);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lblHelp;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(48, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(48, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 622);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help";
            ((System.ComponentModel.ISupportInitialize)(this.navMain)).EndInit();
            this.navMain.ResumeLayout(false);
            this.nvpTutorial.ResumeLayout(false);
            this.nvpTutorialCompleted.ResumeLayout(false);
            this.nvpHelp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.NavigationFrame navMain;
        private DevExpress.XtraBars.Navigation.NavigationPage nvpTutorial;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl lblTutorial;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnRunTutorial;
        private DevExpress.XtraBars.Navigation.NavigationPage nvpTutorialCompleted;
        private DevExpress.XtraEditors.SimpleButton btnEndTutorial;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.Navigation.NavigationPage nvpHelp;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblHelp;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}