using DevExpress.XtraEditors;
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

    public partial class AdvertisingForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        public const int                        CLOSE_DELAY = 15; // Seconds


        //================================================================================
        private DateTime                        mShowTime;


        //================================================================================
        //--------------------------------------------------------------------------------
        public AdvertisingForm() {
            InitializeComponent();
        }
        

        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void AdvertisingForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (Visible) {
                e.Cancel = true;
                if ((DateTime.Now - mShowTime).Seconds >= CLOSE_DELAY)
                    Hide();
            }
        }
        
        //--------------------------------------------------------------------------------
        private void AdvertisingForm_VisibleChanged(object sender, EventArgs e) {
            if (Visible)
                mShowTime = DateTime.Now;
        }
        

        // TIMER ================================================================================
        //--------------------------------------------------------------------------------
        private void tmrTimer_Tick(object sender, EventArgs e) {
            int seconds = CLOSE_DELAY - (DateTime.Now - mShowTime).Seconds;
            Text = "File Me - Common Sense Apps" + (seconds > 0 ? $" - {seconds} second(s)" : "");
        }
    }

}
