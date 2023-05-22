using CSACore.Core;
using CSACore.Licencing;
using DevExpress.XtraEditors;
using FileMe.Configuration;
using Standard.Licensing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FileMe.Forms {

    public partial class LicenceForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        private FileMeForm                      mFileMeForm;


        //================================================================================
        //--------------------------------------------------------------------------------
        public LicenceForm(FileMeForm fileMeForm) {
            // Initialise
            InitializeComponent();

            // File me form
            mFileMeForm = fileMeForm;
            TopMost = mFileMeForm.AlwaysOnTop;

            // Licence
            UpdateLicenceDetails();
        }


        // LICENCE DETAILS ================================================================================
        //--------------------------------------------------------------------------------
        private void UpdateLicenceDetails() {
            // Clear
            txtType.Text = "";
            txtExpires.Text = "";

            // Status
            txtStatus.Text = CSA.Licencer.LicenceStatusString;

            // Activation
            //memLicence.ReadOnly = CSA.Licencer.LicenceActive;
            //btnOpenLicenceFile.Visible = !CSA.Licencer.LicenceActive;
            //btnActivate.Visible = !CSA.Licencer.LicenceActive;

            // No licence
            if (!CSA.Licencer.HasLicence)
                return;

            // Licence details
            txtID.Text = CSA.Licencer.LicenceID;
            txtType.Text = CSA.Licencer.LicenceTypeString;
            txtExpires.Text = CSA.Licencer.LicenceExpiration.Year < 3000 ? CSA.Licencer.LicenceExpiration.ToShortDateString() : "Never";
            memLicence.Text = CSA.Licencer.LicenceString;
        }
        

        // ACTIVATION ================================================================================
        //--------------------------------------------------------------------------------
        private void btnActivate_Click(object sender, EventArgs e) {
            // Variables
            Licencer licencer = new Licencer();
            LicenceActivationStatus licenceStatus = LicenceActivationStatus.INVALID;

            // Licence
            try {
                licencer.LoadLicence(Settings.PUBLIC_LICENCE_KEY, memLicence.Text, false);
                licenceStatus = licencer.LicenceStatus;
            }
            catch (Exception) { }

            // Invalid licence
            if (licenceStatus != LicenceActivationStatus.ACTIVE) {
                string explanation = "This is not a valid licence";
                if (licenceStatus == LicenceActivationStatus.EXPIRED)
                    explanation = $"This licence expired on {licencer.LicenceExpiration.ToShortDateString()}";
                XtraMessageBox.Show(explanation, "File Me", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Apply licence
            if (!CSA.Licencer.HasLicence || !licencer.LicenceID.Equals(CSA.Licencer.LicenceID)) {
                Settings.SaveLicence(memLicence.Text);
                Settings.LoadLicence();
                UpdateLicenceDetails();
                mFileMeForm.ApplyLicence();
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnOpenLicenceFile_Click(object sender, EventArgs e) {
            if (dlgOpenLicenceFile.ShowDialog() == DialogResult.OK) {
                try { memLicence.Text = File.ReadAllText(dlgOpenLicenceFile.FileName); }
                catch (Exception ex) { XtraMessageBox.Show("Failed to open licence: " + ex.Message, "File Me", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        
        //--------------------------------------------------------------------------------
        private void memLicence_TextChanged(object sender, EventArgs e) {
            btnActivate.Enabled = !string.IsNullOrWhiteSpace(memLicence.Text);
        }
    }

}
