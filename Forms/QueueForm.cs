using DevExpress.XtraEditors;
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

    public partial class QueueForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        private FilerManager                    mFilerManager;


        //================================================================================
        //--------------------------------------------------------------------------------
        public QueueForm(FileMeForm fileMeForm, FilerManager filerManager) {
            // Initialise
            InitializeComponent();

            // Top most
            TopMost = fileMeForm.AlwaysOnTop;

            // Filer manager
            mFilerManager = filerManager;

            // Filing tasks
            grdFilingTasks.DataSource = mFilerManager.FilingTasks;
            grdCompletedFilingTasks.DataSource = mFilerManager.CompletedFilingTasks;
        }
        

        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void QueueForm_Load(object sender, EventArgs e) {
            //CenterToParent();
            CenterToScreen();
        }


        // FILING TASKS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnFilingTasks_Start_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            FilingTask filingTask = (FilingTask)grvFilingTasks.GetRow(grvFilingTasks.FocusedRowHandle);
            filingTask.Start();
        }
        
        //--------------------------------------------------------------------------------
        private void btnFilingTasks_Stop_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            FilingTask filingTask = (FilingTask)grvFilingTasks.GetRow(grvFilingTasks.FocusedRowHandle);
            filingTask.Stop();
        }
        
        //--------------------------------------------------------------------------------
        private void btnFilingTasks_Browse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            FilingTask filingTask = (FilingTask)grvFilingTasks.GetRow(grvFilingTasks.FocusedRowHandle);
            try { filingTask.BrowseToPath(); }
            catch (Exception ex) {
                XtraMessageBox.Show(this, "Failed to browse to path: " + ex.Message, "File Me", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        // FILING TASKS - MENU ================================================================================
        //--------------------------------------------------------------------------------
        private void grdFilingTasks_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right)
                mnuFilingTasks.ShowPopup(new Point(Cursor.Position.X - 14, Cursor.Position.Y - 12));
        }

        //--------------------------------------------------------------------------------
        private void btnFilingTasks_RemoveAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            mFilerManager.StopAllFilingTasks();
        }
        

        // FILING TASKS - TAB ================================================================================
        //--------------------------------------------------------------------------------
        private void grvFilingTasks_RowCountChanged(object sender, EventArgs e) {
            tbpFilingTasks.Text = "Transfers" + (grvFilingTasks.RowCount > 0 ? " (" + grvFilingTasks.RowCount + ")": "");
        } 
        

        // COMPLETED FILING TASKS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnCompletedFilingTasks_Browse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            CompletedFilingTask completedFilingTask = (CompletedFilingTask)grvCompletedFilingTasks.GetRow(grvCompletedFilingTasks.FocusedRowHandle);
            try { completedFilingTask.BrowseToPath(); }
            catch (Exception ex) {
                XtraMessageBox.Show(this, "Failed to browse to path: " + ex.Message, "File Me", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnCompletedFilingTasks_Remove_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            mFilerManager.RemoveCompletedFilingTask((CompletedFilingTask)grvCompletedFilingTasks.GetRow(grvCompletedFilingTasks.FocusedRowHandle));
        }

        
        // COMPLETED FILING TASKS - MENU ================================================================================
        //--------------------------------------------------------------------------------
        private void grdCompletedFilingTasks_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right)
                mnuCompletedFilingTasks.ShowPopup(new Point(Cursor.Position.X - 14, Cursor.Position.Y - 12));
        }

        //--------------------------------------------------------------------------------
        private void btnCompletedFilingTasks_RemoveAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            mFilerManager.RemoveAllCompletedFilingTasks();
        }
        

        // COMPLETED FILING TASKS - TAB ================================================================================
        //--------------------------------------------------------------------------------
        private void grvCompletedFilingTasks_RowCountChanged(object sender, EventArgs e) {
            tbpCompletedFilingTasks.Text = "Completed" + (grvCompletedFilingTasks.RowCount > 0 ? " (" + grvCompletedFilingTasks.RowCount + ")": "");
        }
    }

}
