namespace FileMe.Forms {
    partial class CategoryForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoryForm));
            this.lycFiler = new DevExpress.XtraLayout.LayoutControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.colColour = new DevExpress.XtraEditors.ColorPickEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.chkGroupStartsWith = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtGroupStartsWith = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lstGroups = new DevExpress.XtraEditors.ListBoxControl();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.btnRemoveGroup = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddGroup = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lycFiler)).BeginInit();
            this.lycFiler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colColour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGroupStartsWith.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupStartsWith.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // lycFiler
            // 
            this.lycFiler.Controls.Add(this.btnRemoveGroup);
            this.lycFiler.Controls.Add(this.btnAddGroup);
            this.lycFiler.Controls.Add(this.lstGroups);
            this.lycFiler.Controls.Add(this.labelControl1);
            this.lycFiler.Controls.Add(this.txtGroupStartsWith);
            this.lycFiler.Controls.Add(this.chkGroupStartsWith);
            this.lycFiler.Controls.Add(this.btnOk);
            this.lycFiler.Controls.Add(this.btnCancel);
            this.lycFiler.Controls.Add(this.txtName);
            this.lycFiler.Controls.Add(this.colColour);
            this.lycFiler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lycFiler.Location = new System.Drawing.Point(0, 0);
            this.lycFiler.Name = "lycFiler";
            this.lycFiler.Root = this.Root;
            this.lycFiler.Size = new System.Drawing.Size(440, 368);
            this.lycFiler.TabIndex = 26;
            this.lycFiler.Text = "layoutControl1";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(259, 331);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 22);
            this.btnOk.StyleController = this.lycFiler;
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&Save";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(345, 331);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 22);
            this.btnCancel.StyleController = this.lycFiler;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(110, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(315, 20);
            this.txtName.StyleController = this.lycFiler;
            this.txtName.TabIndex = 11;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // colColour
            // 
            this.colColour.EditValue = System.Drawing.Color.Empty;
            this.colColour.Location = new System.Drawing.Point(110, 41);
            this.colColour.Name = "colColour";
            this.colColour.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colColour.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colColour.Size = new System.Drawing.Size(49, 20);
            this.colColour.StyleController = this.lycFiler;
            this.colColour.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.layoutControlItem17,
            this.layoutControlItem18,
            this.emptySpaceItem4,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.emptySpaceItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(12, 12, 12, 12);
            this.Root.Size = new System.Drawing.Size(440, 368);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem1.Size = new System.Drawing.Size(416, 26);
            this.layoutControlItem1.Text = "Name";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(90, 13);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.colColour;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(150, 24);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(150, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem3.Size = new System.Drawing.Size(150, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Colour";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(90, 13);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(150, 26);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 26);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.emptySpaceItem2.Size = new System.Drawing.Size(266, 26);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.btnCancel;
            this.layoutControlItem17.Location = new System.Drawing.Point(330, 316);
            this.layoutControlItem17.MaxSize = new System.Drawing.Size(86, 28);
            this.layoutControlItem17.MinSize = new System.Drawing.Size(86, 28);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem17.Size = new System.Drawing.Size(86, 28);
            this.layoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.btnOk;
            this.layoutControlItem18.Location = new System.Drawing.Point(244, 316);
            this.layoutControlItem18.MaxSize = new System.Drawing.Size(86, 28);
            this.layoutControlItem18.MinSize = new System.Drawing.Size(86, 28);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem18.Size = new System.Drawing.Size(86, 28);
            this.layoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 316);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(244, 28);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 100);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(96, 206);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // chkGroupStartsWith
            // 
            this.chkGroupStartsWith.Location = new System.Drawing.Point(110, 66);
            this.chkGroupStartsWith.Name = "chkGroupStartsWith";
            this.chkGroupStartsWith.Properties.Caption = "";
            this.chkGroupStartsWith.Size = new System.Drawing.Size(19, 19);
            this.chkGroupStartsWith.StyleController = this.lycFiler;
            this.chkGroupStartsWith.TabIndex = 12;
            this.chkGroupStartsWith.CheckedChanged += new System.EventHandler(this.chkGroupStartsWith_CheckedChanged);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkGroupStartsWith;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(119, 24);
            this.layoutControlItem2.Text = "Group Starts With";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(91, 13);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // txtGroupStartsWith
            // 
            this.txtGroupStartsWith.Enabled = false;
            this.txtGroupStartsWith.Location = new System.Drawing.Point(133, 66);
            this.txtGroupStartsWith.Name = "txtGroupStartsWith";
            this.txtGroupStartsWith.Size = new System.Drawing.Size(293, 20);
            this.txtGroupStartsWith.StyleController = this.lycFiler;
            this.txtGroupStartsWith.TabIndex = 14;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtGroupStartsWith;
            this.layoutControlItem5.Location = new System.Drawing.Point(119, 52);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(297, 24);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 90);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(92, 20);
            this.labelControl1.StyleController = this.lycFiler;
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "Groups";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.labelControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(96, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(96, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(96, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // lstGroups
            // 
            this.lstGroups.Location = new System.Drawing.Point(110, 90);
            this.lstGroups.Name = "lstGroups";
            this.lstGroups.Size = new System.Drawing.Size(316, 200);
            this.lstGroups.StyleController = this.lycFiler;
            this.lstGroups.TabIndex = 18;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lstGroups;
            this.layoutControlItem7.Location = new System.Drawing.Point(96, 76);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(320, 204);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 306);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(416, 10);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // btnRemoveGroup
            // 
            this.btnRemoveGroup.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveGroup.ImageOptions.Image")));
            this.btnRemoveGroup.Location = new System.Drawing.Point(269, 294);
            this.btnRemoveGroup.Name = "btnRemoveGroup";
            this.btnRemoveGroup.Size = new System.Drawing.Size(157, 22);
            this.btnRemoveGroup.StyleController = this.lycFiler;
            this.btnRemoveGroup.TabIndex = 20;
            this.btnRemoveGroup.Text = "Remove";
            this.btnRemoveGroup.Click += new System.EventHandler(this.btnRemoveGroup_Click);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddGroup.ImageOptions.Image")));
            this.btnAddGroup.Location = new System.Drawing.Point(110, 294);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(155, 22);
            this.btnAddGroup.StyleController = this.lycFiler;
            this.btnAddGroup.TabIndex = 19;
            this.btnAddGroup.Text = "Add";
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnAddGroup;
            this.layoutControlItem6.Location = new System.Drawing.Point(96, 280);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(159, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnRemoveGroup;
            this.layoutControlItem8.Location = new System.Drawing.Point(255, 280);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(161, 26);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // CategoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 368);
            this.Controls.Add(this.lycFiler);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CategoryForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Me - Category";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.lycFiler)).EndInit();
            this.lycFiler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colColour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGroupStartsWith.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupStartsWith.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lycFiler;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.ColorPickEdit colColour;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.CheckEdit chkGroupStartsWith;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txtGroupStartsWith;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.ListBoxControl lstGroups;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.SimpleButton btnAddGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnRemoveGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
    }
}