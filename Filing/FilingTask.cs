using CSACore.Utility;
using DevExpress.XtraEditors;
using FileMe.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FileMe.Filing {

    public class FilingTask : Bindable { 
        //================================================================================
        public const int                        BUFFER_SIZE = 1024 * 512; // Half a megabyte
        

        //================================================================================
        private Filer                           mFiler;

        private BackgroundWorker                mBackgroundWorker;

        private bool                            mCompleted = false;
        private bool                            mSucceeded = false;
        private bool                            mCancelled = false;
        private int                             mProgress = 0;
        
        private string                          mSourceFilePath;
        private string                          mSourceFilename;
        private Stream                          mSourceStream;
        private bool                            mIsDirectory;
        private FilePathGenerator               mTargetFilePathGenerator;
        private string                          mInputTargetFilePath = null;
        private string                          mTargetFilePath = null;

        private AutoResetEvent                  mFinishedEvent = new AutoResetEvent(false);

        //--------------------------------------------------------------------------------
        public event EventDelegate              ProgressEvent = delegate { }; // To avoid null check and make thread safe
        public event EventDelegate              CompletedEvent = delegate { }; // To avoid null check and make thread safe


        //================================================================================
        //--------------------------------------------------------------------------------
        public FilingTask(Filer filer, string sourceFilePath, Stream sourceStream, bool isDirectory, FilePathGenerator targetFilePathGenerator) {
            // Checks
            if (isDirectory && sourceStream != null)
                throw new ArgumentException();

            // Filer
            mFiler = filer;

            // Source / target
            mSourceFilePath = sourceFilePath;
            mSourceFilename = Path.GetFileName(sourceFilePath);
            mSourceStream = sourceStream;
            mIsDirectory = isDirectory;
            mTargetFilePathGenerator = targetFilePathGenerator;

            // Background worker
            mBackgroundWorker = new BackgroundWorker();
            mBackgroundWorker.WorkerSupportsCancellation = true;
            mBackgroundWorker.WorkerReportsProgress = true;
            mBackgroundWorker.DoWork += DoFiling;
            mBackgroundWorker.ProgressChanged += OnProgressChanged;
            mBackgroundWorker.RunWorkerCompleted += OnCompleted;

            // Inputs
            XtraInputBoxArgs args;

            // Inputs - text
            if (mTargetFilePathGenerator.RequiresInputText) {
                args = new XtraInputBoxArgs();
                args.Caption = "File Me";
                args.Prompt = "Please enter text for \"" + mSourceFilename + "\": ";
                mTargetFilePathGenerator.InputText = (string)XtraInputBox.Show(args) ?? "";
            }

            // Inputs - date
            if (mTargetFilePathGenerator.RequiresInputDate) {
                args = new XtraInputBoxArgs();
                args.Caption = "File Me";
                args.Prompt = "Please enter a date for \"" + mSourceFilename + "\": ";
                args.DefaultResponse = DateTime.Now;
                args.Editor = new DateEdit();
                mTargetFilePathGenerator.InputDate = (DateTime?)XtraInputBox.Show(args) ?? DateTime.MinValue;
            }

            // Inputs - target file path
            if (mFiler.AuditFilename) {
                mInputTargetFilePath = mTargetFilePathGenerator.GeneratePath(mSourceFilename);
                string path = Path.GetDirectoryName(mInputTargetFilePath);
                string filename = Path.GetFileNameWithoutExtension(mInputTargetFilePath);
                string extension = Path.GetExtension(mInputTargetFilePath);

                args = new XtraInputBoxArgs();
                args.Caption = "File Me - Review Filename";
                args.Prompt = "Please enter a final filename for \"" + mSourceFilename + "\": ";
                args.DefaultResponse = filename;
                filename = Filer.SanitiseFilename((string)XtraInputBox.Show(args) ?? filename);
                mInputTargetFilePath = Path.Combine(path, filename + extension);
            }
        }


        // EVENT ================================================================================
        //--------------------------------------------------------------------------------
        private void OnProgressChanged(object sender, ProgressChangedEventArgs args) {
            // Progress
            SetProperty("Progress", ref mProgress, args.ProgressPercentage);

            // Notify
            mFiler.OnFilingTaskProgress(this);
            ProgressEvent?.Invoke(this);
            mFiler.Manager.InvokeFilingTaskProgressEvent(this);
        }

        //--------------------------------------------------------------------------------
        private void OnCompleted(object sender, RunWorkerCompletedEventArgs args) {
            Complete(!args.Cancelled, args.Cancelled);
        }


        // FILER ================================================================================
        //--------------------------------------------------------------------------------
        public Filer Filer { get { return mFiler; } }


        // FILING ================================================================================
        //--------------------------------------------------------------------------------
        public bool Start() {
            // Checks
            if (mBackgroundWorker == null || mTargetFilePath != null)
                return false;

            // Target path
            mTargetFilePath = ""; // Set to a value to avoid re-entry, which was an encountered problem that would cause start to get here twice for the same task
                                  // (not relevant now that inputs are moved out, but might as well leave it)

            // Target stream
            FileStream targetStream = null;
            while (true) {
                try {
                    TargetFilePath = (mInputTargetFilePath != null ? mInputTargetFilePath : mTargetFilePathGenerator.GeneratePath(mSourceFilename));
                    Directory.CreateDirectory(mTargetFilePathGenerator.TargetPath);
                    if (!IsDirectory)
                        targetStream = new FileStream(mTargetFilePath, FileMode.CreateNew, FileAccess.Write);
                    else
                        Directory.CreateDirectory(mTargetFilePath);
                    break;
                }
                catch (DirectoryNotFoundException e) {
                    Complete(false, false, e);
                    return false;
                }
                catch (IOException e) {
                    if (!e.Message.Contains("already exists.")) {
                        Complete(false, false, e);
                        return false;
                    }
                }
            }

            // File
            mBackgroundWorker.RunWorkerAsync(new BackgroundWorkerArguments(mSourceFilePath, mSourceStream, IsDirectory, mTargetFilePath, targetStream));
            return true;
        }

        //--------------------------------------------------------------------------------
        public void Stop() {
            // Not started
            if (mBackgroundWorker == null)
                return;
            if (mTargetFilePath == null && !Completed) {
                Complete(false, true);
                return;
            }

            // Cancel
            mBackgroundWorker.CancelAsync();

            // Wait
            if (mBackgroundWorker.IsBusy)
                mFinishedEvent.WaitOne();
        }

        //--------------------------------------------------------------------------------
        private void Complete(bool succeeded, bool cancelled, Exception exception = null) {
            // Completed
            Completed = true;
            Succeeded = succeeded;
            Cancelled = cancelled;

            // Background worker
            mBackgroundWorker.Dispose();
            mBackgroundWorker = null;

            // Notify
            mFiler.OnFilingTaskCompleted(this, exception);
            CompletedEvent?.Invoke(this);
            mFiler.Manager.InvokeFilingTaskCompletedEvent(this, exception);
        }
        
        //--------------------------------------------------------------------------------
        private void DoFiling(object sender, DoWorkEventArgs args) {
            // Arguments
            BackgroundWorkerArguments arguments = (BackgroundWorkerArguments)args.Argument;
            string sourceFilePath = arguments.sourceFilePath;
            Stream sourceStream = arguments.sourceStream;
            bool isDirectory = arguments.isDirectory;
            string targetFilePath = arguments.targetFilePath;
            Stream targetStream = arguments.targetStream;

            // Filing
            try {
                if (!isDirectory) {
                    // File
                    DoFiling_File(sourceStream, targetStream);
                }
                else {
                    // Directory
                    string[] files = Directory.GetFiles(sourceFilePath, "*", SearchOption.AllDirectories);
                    for (int i = 0; i < files.Length; ++i) {
                        // Cancellation
                        if (mBackgroundWorker.CancellationPending)
                            break;

                        // Path
                        //files[i] = files[i].Replace(@"\", "/");
                        string relativeSubFilePath = files[i].Replace(sourceFilePath, "");
                        if (relativeSubFilePath.StartsWith("\\"))
                            relativeSubFilePath = relativeSubFilePath.Substring(1);
                        string targetSubFilePath = Path.Combine(targetFilePath, relativeSubFilePath)/*.Replace(@"\", "/")*/;

                        // Directory
                        Directory.CreateDirectory(Path.GetDirectoryName(targetSubFilePath));

                        // Streams
                        sourceStream = new FileStream(files[i], FileMode.Open, FileAccess.Read);
                        targetStream = new FileStream(targetSubFilePath, FileMode.CreateNew, FileAccess.Write);

                        // File
                        DoFiling_File(sourceStream, targetStream, i, files.Length);

                        // Close / dispose
                        sourceStream.Close();
                        sourceStream.Dispose();
                        targetStream.Close();
                        targetStream.Dispose();
                    }
                }
            }
            catch (Exception) { }
            finally {
                // Source stream
                if (sourceStream != null) {
                    sourceStream.Close();
                    sourceStream.Dispose();
                    sourceStream = null;
                }

                // Target stream
                if (targetStream != null) {
                    targetStream.Close();
                    targetStream.Dispose();
                    targetStream = null;
                }

                // Cancellation
                if (mBackgroundWorker.CancellationPending) {
                    // Flag
                    args.Cancel = true;

                    // Delete
                    if (!isDirectory)
                        File.Delete(targetFilePath);
                    else
                        Directory.Delete(targetFilePath, true);
                }

                // Notify
                mFinishedEvent.Set();
            }
        }

        //--------------------------------------------------------------------------------
        private void DoFiling_File(Stream sourceStream, Stream targetStream, int fileIndex = 0, int fileCount = 1) {
            byte[] buffer = new byte[BUFFER_SIZE];
            int bytesRead = -1;

            while ((bytesRead = sourceStream.Read(buffer, 0, BUFFER_SIZE)) > 0) {
                // Cancellation
                if (mBackgroundWorker.CancellationPending)
                    break;

                // Write
                targetStream.Write(buffer, 0, bytesRead);
                int progress = (int)((fileIndex * 100.0m / fileCount) +
                                     ((decimal)sourceStream.Position / (decimal)sourceStream.Length * 100.0m / fileCount));
                if (progress != mProgress) {
                    mProgress = progress;
                    mBackgroundWorker.ReportProgress(progress);
                }
            }
        }

        //--------------------------------------------------------------------------------
        public bool Running { get { return mBackgroundWorker != null ? mBackgroundWorker.IsBusy : false; } }

        //--------------------------------------------------------------------------------
        public bool Succeeded {
            private set { SetProperty("Succeeded", ref mSucceeded, value); }
            get { return mSucceeded; }
        }

        //--------------------------------------------------------------------------------
        public bool Completed {
            private set { SetProperty("Completed", ref mCompleted, value); }
            get { return mCompleted; }
        }

        //--------------------------------------------------------------------------------
        public bool Cancelled {
            private set { SetProperty("Cancelled", ref mCancelled, value); }
            get { return mCancelled; }
        }

        //--------------------------------------------------------------------------------
        public int Progress {
            private set {
                SetProperty("Progress", ref mProgress, value);
                OnPropertyChanged("ProgressDouble");
            }
            get { return mProgress; }
        }

        //--------------------------------------------------------------------------------
        public double ProgressDouble { get { return mProgress / 100.0; } }
        

        // SOURCE ================================================================================
        //--------------------------------------------------------------------------------
        public bool IsDirectory { get { return mIsDirectory; } }

        
        // TARGET ================================================================================
        //--------------------------------------------------------------------------------
        public string TargetFilePath {
            private set {
                SetProperty("TargetFilePath", ref mTargetFilePath, value);
                OnPropertyChanged("TargetFilename");
                OnPropertyChanged("TargetPath");
            }
            get { return mTargetFilePath; }
        }

        //--------------------------------------------------------------------------------
        public string TargetFilename {
            get {
                if (mTargetFilePath != null)
                    return mTargetFilePath != null ? Path.GetFileName(mTargetFilePath) : "";
                else
                    return mSourceFilename;
            }
        }
        
        //--------------------------------------------------------------------------------
        public string TargetPath {
            get {
                if (mTargetFilePath != null)
                    return !string.IsNullOrWhiteSpace(mTargetFilePath) ? Path.GetDirectoryName(mTargetFilePath)/*.Replace(@"\", "/")*/ : "";
                else
                    return mTargetFilePathGenerator.TargetPath;
            }
        }
        
        //--------------------------------------------------------------------------------
        public void BrowseToPath() {
            // Checks
            if (string.IsNullOrWhiteSpace(TargetPath))
                return;

            // Browse
            Process.Start(TargetPath);
        }

        
        // FILER ================================================================================
        //--------------------------------------------------------------------------------
        public string FilerGroup { get { return mFiler.Group; } }

        //--------------------------------------------------------------------------------
        public string FilerName { get { return mFiler.Name; } }


        //================================================================================
        //********************************************************************************
        class BackgroundWorkerArguments {
            public string sourceFilePath;
            public Stream sourceStream;
            public bool isDirectory;
            public string targetFilePath;
            public Stream targetStream;

            public BackgroundWorkerArguments(string sourceFilePath, Stream sourceStream, bool isDirectory, string targetFilePath, Stream targetStream) {
                this.sourceFilePath = sourceFilePath;
                this.sourceStream = sourceStream;
                this.isDirectory = isDirectory;
                this.targetFilePath = targetFilePath;
                this.targetStream = targetStream;
            }
        }
        
        //********************************************************************************
        public delegate void EventDelegate(FilingTask task);
    }

}
