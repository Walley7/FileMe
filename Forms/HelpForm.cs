using CSACore.Core;
using FileMe.Tutorials;
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

    public partial class HelpForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        //--------------------------------------------------------------------------------
        public HelpForm(bool fromTutorialEnd) {
            // Initialise
            InitializeComponent();

            // Tutorial
            if (CSA.TutorialSystem.Tutorial != null) {
                btnRunTutorial.Visible = false;
                btnEndTutorial.Visible = true;
            }

            // Tutorial end
            if (fromTutorialEnd)
                navMain.SelectedPage = nvpTutorialCompleted;
        }


        // NAVIGATION ================================================================================
        //--------------------------------------------------------------------------------
        private void lblTutorial_Click(object sender, EventArgs e) { navMain.SelectedPage = nvpTutorial; }
        private void lblHelp_Click(object sender, EventArgs e) { navMain.SelectedPage = nvpHelp; }


        // TUTORIAL ================================================================================
        //--------------------------------------------------------------------------------
        private void btnRunTutorial_Click(object sender, EventArgs e) {
            Close();
            CSA.TutorialSystem.StartTutorial(new FileMeTutorial(Owner), true);
        }
        
        //--------------------------------------------------------------------------------
        private void btnEndTutorial_Click(object sender, EventArgs e) {
            CSA.TutorialSystem.StopTutorial();
            btnEndTutorial.Visible = false;
            btnRunTutorial.Visible = true;
        }
    }

}
