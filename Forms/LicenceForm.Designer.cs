namespace FileMe.Forms {
    partial class LicenceForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenceForm));
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtStatus = new DevExpress.XtraEditors.TextEdit();
            this.txtType = new DevExpress.XtraEditors.TextEdit();
            this.txtExpires = new DevExpress.XtraEditors.TextEdit();
            this.btnActivate = new DevExpress.XtraEditors.SimpleButton();
            this.memLicence = new DevExpress.XtraEditors.MemoEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnOpenLicenceFile = new DevExpress.XtraEditors.SimpleButton();
            this.dlgOpenLicenceFile = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpires.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memLicence.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Type";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 96);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(35, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Expires";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(15, 44);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(31, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Status";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(95, 41);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtStatus.Properties.Appearance.Options.UseFont = true;
            this.txtStatus.Properties.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(220, 20);
            this.txtStatus.TabIndex = 0;
            this.txtStatus.TabStop = false;
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(95, 67);
            this.txtType.Name = "txtType";
            this.txtType.Properties.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(220, 20);
            this.txtType.TabIndex = 1;
            this.txtType.TabStop = false;
            // 
            // txtExpires
            // 
            this.txtExpires.Location = new System.Drawing.Point(95, 93);
            this.txtExpires.Name = "txtExpires";
            this.txtExpires.Properties.ReadOnly = true;
            this.txtExpires.Size = new System.Drawing.Size(220, 20);
            this.txtExpires.TabIndex = 7;
            this.txtExpires.TabStop = false;
            // 
            // btnActivate
            // 
            this.btnActivate.Enabled = false;
            this.btnActivate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnActivate.ImageOptions.Image")));
            this.btnActivate.Location = new System.Drawing.Point(347, 281);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(197, 22);
            this.btnActivate.TabIndex = 8;
            this.btnActivate.Text = "&Activate";
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // memLicence
            // 
            this.memLicence.Location = new System.Drawing.Point(347, 15);
            this.memLicence.Name = "memLicence";
            this.memLicence.Size = new System.Drawing.Size(400, 260);
            this.memLicence.TabIndex = 1;
            this.memLicence.TextChanged += new System.EventHandler(this.memLicence_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(235, 281);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 22);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            // 
            // labelControl5
            // 
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl5.Location = new System.Drawing.Point(330, 0);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(2, 347);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "labelControl5";
            // 
            // btnOpenLicenceFile
            // 
            this.btnOpenLicenceFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenLicenceFile.ImageOptions.Image")));
            this.btnOpenLicenceFile.Location = new System.Drawing.Point(550, 281);
            this.btnOpenLicenceFile.Name = "btnOpenLicenceFile";
            this.btnOpenLicenceFile.Size = new System.Drawing.Size(197, 22);
            this.btnOpenLicenceFile.TabIndex = 14;
            this.btnOpenLicenceFile.Text = "&Open Licence File";
            this.btnOpenLicenceFile.Click += new System.EventHandler(this.btnOpenLicenceFile_Click);
            // 
            // dlgOpenLicenceFile
            // 
            this.dlgOpenLicenceFile.Filter = "Licence Files (*.lic)|*.lic";
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.Location = new System.Drawing.Point(95, 15);
            this.txtID.Name = "txtID";
            this.txtID.Properties.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(220, 20);
            this.txtID.TabIndex = 15;
            this.txtID.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(11, 13);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "ID";
            // 
            // LicenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 318);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnOpenLicenceFile);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.memLicence);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.txtExpires);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LicenceForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Me - Licence";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpires.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memLicence.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtStatus;
        private DevExpress.XtraEditors.TextEdit txtType;
        private DevExpress.XtraEditors.TextEdit txtExpires;
        private DevExpress.XtraEditors.SimpleButton btnActivate;
        private DevExpress.XtraEditors.MemoEdit memLicence;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnOpenLicenceFile;
        private DevExpress.XtraEditors.XtraOpenFileDialog dlgOpenLicenceFile;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}