namespace FileMe.Forms {
    partial class OptionsForm {
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
            this.spnTileWidth = new DevExpress.XtraEditors.SpinEdit();
            this.spnTileHeight = new DevExpress.XtraEditors.SpinEdit();
            this.trkFadeOpacity = new DevExpress.XtraEditors.TrackBarControl();
            this.btnDefaults = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblFadeOpacity = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnLicenceInformation = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cboTheme = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.chkAlwaysOnTop = new DevExpress.XtraEditors.CheckEdit();
            this.lueGlobalHotkeyKey = new DevExpress.XtraEditors.LookUpEdit();
            this.cboGlobalHotkeyModifier = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkGlobalHotkey = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.cboTextSize = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTileWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTileHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFadeOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFadeOpacity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTheme.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAlwaysOnTop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGlobalHotkeyKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGlobalHotkeyModifier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGlobalHotkey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTextSize.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // spnTileWidth
            // 
            this.spnTileWidth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnTileWidth.Location = new System.Drawing.Point(131, 67);
            this.spnTileWidth.Name = "spnTileWidth";
            this.spnTileWidth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnTileWidth.Properties.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spnTileWidth.Properties.Mask.EditMask = "#####";
            this.spnTileWidth.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spnTileWidth.Properties.MinValue = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.spnTileWidth.Size = new System.Drawing.Size(78, 20);
            this.spnTileWidth.TabIndex = 0;
            this.spnTileWidth.ValueChanged += new System.EventHandler(this.spnTileWidth_ValueChanged);
            // 
            // spnTileHeight
            // 
            this.spnTileHeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnTileHeight.Location = new System.Drawing.Point(232, 67);
            this.spnTileHeight.Name = "spnTileHeight";
            this.spnTileHeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnTileHeight.Properties.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spnTileHeight.Properties.Mask.EditMask = "#####";
            this.spnTileHeight.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spnTileHeight.Properties.MinValue = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.spnTileHeight.Size = new System.Drawing.Size(78, 20);
            this.spnTileHeight.TabIndex = 1;
            this.spnTileHeight.ValueChanged += new System.EventHandler(this.spnTileHeight_ValueChanged);
            // 
            // trkFadeOpacity
            // 
            this.trkFadeOpacity.EditValue = 10;
            this.trkFadeOpacity.Location = new System.Drawing.Point(131, 90);
            this.trkFadeOpacity.Name = "trkFadeOpacity";
            this.trkFadeOpacity.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.trkFadeOpacity.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.trkFadeOpacity.Properties.LargeChange = 2;
            this.trkFadeOpacity.Properties.Minimum = 1;
            this.trkFadeOpacity.Properties.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkFadeOpacity.Size = new System.Drawing.Size(144, 45);
            this.trkFadeOpacity.TabIndex = 2;
            this.trkFadeOpacity.Value = 10;
            this.trkFadeOpacity.ValueChanged += new System.EventHandler(this.trkFadeOpacity_ValueChanged);
            // 
            // btnDefaults
            // 
            this.btnDefaults.Location = new System.Drawing.Point(15, 239);
            this.btnDefaults.Name = "btnDefaults";
            this.btnDefaults.Size = new System.Drawing.Size(80, 22);
            this.btnDefaults.TabIndex = 3;
            this.btnDefaults.Text = "&Defaults";
            this.btnDefaults.Click += new System.EventHandler(this.btnDefaults_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(144, 239);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 22);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "&Save";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(230, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 22);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 70);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Tile Dimensions";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 96);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Fade Opacity";
            // 
            // lblFadeOpacity
            // 
            this.lblFadeOpacity.Location = new System.Drawing.Point(281, 96);
            this.lblFadeOpacity.Name = "lblFadeOpacity";
            this.lblFadeOpacity.Size = new System.Drawing.Size(29, 13);
            this.lblFadeOpacity.TabIndex = 8;
            this.lblFadeOpacity.Text = "100%";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(215, 70);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(12, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "by";
            // 
            // btnLicenceInformation
            // 
            this.btnLicenceInformation.Location = new System.Drawing.Point(131, 192);
            this.btnLicenceInformation.Name = "btnLicenceInformation";
            this.btnLicenceInformation.Size = new System.Drawing.Size(179, 23);
            this.btnLicenceInformation.TabIndex = 11;
            this.btnLicenceInformation.Text = "Licence Information";
            this.btnLicenceInformation.Click += new System.EventHandler(this.btnLicenceInformation_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl3.Location = new System.Drawing.Point(0, 184);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(335, 2);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "labelControl3";
            // 
            // labelControl5
            // 
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl5.Location = new System.Drawing.Point(0, 231);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(335, 2);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "labelControl5";
            // 
            // cboTheme
            // 
            this.cboTheme.Location = new System.Drawing.Point(131, 15);
            this.cboTheme.Name = "cboTheme";
            this.cboTheme.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTheme.Properties.Items.AddRange(new object[] {
            "Black",
            "Grey",
            "White",
            "Smooth Grey"});
            this.cboTheme.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboTheme.Size = new System.Drawing.Size(179, 20);
            this.cboTheme.TabIndex = 14;
            this.cboTheme.EditValueChanged += new System.EventHandler(this.cboTheme_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(15, 18);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(32, 13);
            this.labelControl6.TabIndex = 15;
            this.labelControl6.Text = "Theme";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(17, 197);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(35, 13);
            this.labelControl8.TabIndex = 18;
            this.labelControl8.Text = "Version";
            // 
            // lblVersion
            // 
            this.lblVersion.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblVersion.Appearance.Options.UseFont = true;
            this.lblVersion.Location = new System.Drawing.Point(60, 197);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(27, 13);
            this.lblVersion.TabIndex = 19;
            this.lblVersion.Text = "0.8.0";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(15, 122);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(72, 13);
            this.labelControl7.TabIndex = 20;
            this.labelControl7.Text = "Always On Top";
            // 
            // chkAlwaysOnTop
            // 
            this.chkAlwaysOnTop.Location = new System.Drawing.Point(131, 119);
            this.chkAlwaysOnTop.Name = "chkAlwaysOnTop";
            this.chkAlwaysOnTop.Properties.Caption = "";
            this.chkAlwaysOnTop.Size = new System.Drawing.Size(75, 19);
            this.chkAlwaysOnTop.TabIndex = 21;
            // 
            // lueGlobalHotkeyKey
            // 
            this.lueGlobalHotkeyKey.Location = new System.Drawing.Point(223, 145);
            this.lueGlobalHotkeyKey.Name = "lueGlobalHotkeyKey";
            this.lueGlobalHotkeyKey.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueGlobalHotkeyKey.Size = new System.Drawing.Size(87, 20);
            this.lueGlobalHotkeyKey.TabIndex = 23;
            // 
            // cboGlobalHotkeyModifier
            // 
            this.cboGlobalHotkeyModifier.Location = new System.Drawing.Point(152, 145);
            this.cboGlobalHotkeyModifier.Name = "cboGlobalHotkeyModifier";
            this.cboGlobalHotkeyModifier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGlobalHotkeyModifier.Properties.Items.AddRange(new object[] {
            "None",
            "Ctrl",
            "Alt",
            "Shift"});
            this.cboGlobalHotkeyModifier.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboGlobalHotkeyModifier.Size = new System.Drawing.Size(65, 20);
            this.cboGlobalHotkeyModifier.TabIndex = 24;
            // 
            // chkGlobalHotkey
            // 
            this.chkGlobalHotkey.Location = new System.Drawing.Point(131, 145);
            this.chkGlobalHotkey.Name = "chkGlobalHotkey";
            this.chkGlobalHotkey.Properties.Caption = "";
            this.chkGlobalHotkey.Size = new System.Drawing.Size(20, 19);
            this.chkGlobalHotkey.TabIndex = 25;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(15, 148);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(66, 13);
            this.labelControl9.TabIndex = 26;
            this.labelControl9.Text = "Global Hotkey";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(15, 44);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(44, 13);
            this.labelControl10.TabIndex = 28;
            this.labelControl10.Text = "Text Size";
            // 
            // cboTextSize
            // 
            this.cboTextSize.Location = new System.Drawing.Point(131, 41);
            this.cboTextSize.Name = "cboTextSize";
            this.cboTextSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTextSize.Properties.Items.AddRange(new object[] {
            "Small",
            "Medium",
            "Large"});
            this.cboTextSize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboTextSize.Size = new System.Drawing.Size(179, 20);
            this.cboTextSize.TabIndex = 27;
            this.cboTextSize.EditValueChanged += new System.EventHandler(this.cboTextSize_EditValueChanged);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 276);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.chkGlobalHotkey);
            this.Controls.Add(this.cboGlobalHotkeyModifier);
            this.Controls.Add(this.lueGlobalHotkeyKey);
            this.Controls.Add(this.chkAlwaysOnTop);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.cboTheme);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btnLicenceInformation);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.lblFadeOpacity);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnDefaults);
            this.Controls.Add(this.trkFadeOpacity);
            this.Controls.Add(this.spnTileWidth);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.spnTileHeight);
            this.Controls.Add(this.cboTextSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Me - Options";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.spnTileWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTileHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFadeOpacity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFadeOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTheme.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAlwaysOnTop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGlobalHotkeyKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGlobalHotkeyModifier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGlobalHotkey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTextSize.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit spnTileWidth;
        private DevExpress.XtraEditors.SpinEdit spnTileHeight;
        private DevExpress.XtraEditors.TrackBarControl trkFadeOpacity;
        private DevExpress.XtraEditors.SimpleButton btnDefaults;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblFadeOpacity;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnLicenceInformation;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit cboTheme;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CheckEdit chkAlwaysOnTop;
        private DevExpress.XtraEditors.LookUpEdit lueGlobalHotkeyKey;
        private DevExpress.XtraEditors.ComboBoxEdit cboGlobalHotkeyModifier;
        private DevExpress.XtraEditors.CheckEdit chkGlobalHotkey;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.ComboBoxEdit cboTextSize;
    }
}