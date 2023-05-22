using CSACore.Core;
using DevExpress.XtraEditors;
using FileMe.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FileMe {

    static class Program {
        //================================================================================
        public const string                     MUTEX_NAME = "File Me";


        //================================================================================
        //--------------------------------------------------------------------------------
        [STAThread]
        static void Main() {
            // Initialise
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialise - CSA
            try { CSA.Initialise(null, "CSA", "File Me"); }
            catch (Exception e) {
                XtraMessageBox.Show("Failed to initialise: " + e.Message, "File Me", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Mutex
            Mutex mutex = new Mutex(false, MUTEX_NAME);

            // Run
            try {
                if (!mutex.WaitOne(0, false))
                    XtraMessageBox.Show("Another instance of file me is already running", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    Application.Run(new FileMeForm());
            }
            finally {
                // Shutdown
                CSA.Shutdown();

                // Mutex
                mutex.Close();
            }
        }
    }

}
