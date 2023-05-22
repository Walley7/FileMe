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

    public partial class CategoryGroupForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        //--------------------------------------------------------------------------------
        public CategoryGroupForm(FileMeForm fileMeForm, FilerManager filerManager) {
            // Initialise
            InitializeComponent();

            // Top most
            TopMost = fileMeForm.AlwaysOnTop;

            // Groups
            cboGroup.Properties.Items.AddRange(filerManager.FilerGroups);
        }


        // GROUP ================================================================================
        //--------------------------------------------------------------------------------
        public string Group { get { return cboGroup.Text; } }
    }

}
