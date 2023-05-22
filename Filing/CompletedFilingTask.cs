using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileMe.Filing {

    public class CompletedFilingTask {
        //================================================================================
        private bool                            mSucceeded;
        private bool                            mCancelled;

        private string                          mFilerGroup;
        private string                          mFilerName;

        private string                          mFilePath;


        //================================================================================
        //--------------------------------------------------------------------------------
        public CompletedFilingTask(bool succeeded, bool cancelled, string filerGroup, string filerName, string filePath) {
            mSucceeded = succeeded;
            mCancelled = cancelled;
            mFilerGroup = filerGroup;
            mFilerName = filerName;
            mFilePath = filePath;
        }

        //--------------------------------------------------------------------------------
        public CompletedFilingTask(FilingTask filingTask) : this(filingTask.Succeeded, filingTask.Cancelled, filingTask.FilerGroup, filingTask.FilerName, filingTask.TargetFilePath) { }


        // OUTCOME ================================================================================
        //--------------------------------------------------------------------------------
        public bool Succeeded { get { return mSucceeded; } }
        public bool Cancelled { get { return mCancelled; } }


        // FILER ================================================================================
        //--------------------------------------------------------------------------------
        public string FilerGroup { get { return mFilerGroup; } }
        public string FilerName { get { return mFilerName; } }


        // PATH ================================================================================
        //--------------------------------------------------------------------------------
        public string FilePath { get { return mFilePath; } }
        public string Filename { get { return System.IO.Path.GetFileName(mFilePath); } }
        public string Path { get { return System.IO.Path.GetDirectoryName(mFilePath)/*.Replace(@"\", "/")*/; } }
        
        //--------------------------------------------------------------------------------
        public void BrowseToPath() {
            // Checks
            if (string.IsNullOrWhiteSpace(Path))
                return;

            // Browse
            Process.Start(Path);
        }
    }

}
