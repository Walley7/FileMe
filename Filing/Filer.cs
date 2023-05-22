using CSACore.Utility;
using DevExpress.XtraEditors;
using FileMe.Outlook;
using FileMe.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartFormat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FileMe.Filing {

    public class Filer : Bindable {
        //================================================================================
        public const string                     UNALLOWED_FILENAME_CHARACTERS = @"\/:*?""<>|";
        public const string                     UNALLOWED_FILENAME_END_CHARACTERS = @". ";

        public const long                       COMPLETION_MESSAGE_DURATION = 60000;
        public const long                       COMPLETION_MESSAGE_CHARACTER_INTERVAL = 50; // The rate at which the next character is shown

        //--------------------------------------------------------------------------------
        public enum ERenameMode {
            REPLACE,
            ADD_TO_FRONT,
            ADD_TO_END
        }


        //================================================================================
        private FilerManager                    mManager;

        private string                          mGroup;
        private string                          mName;
        private Color                           mColour;

        private string                          mTargetPath;
        private string                          mFilenamePattern;
        private bool                            mCustomFilenamePattern;

        private bool                            mRename;
        private string                          mRenameText;
        private ERenameMode                     mRenameMode;

        private bool                            mAppendDate;
        private string                          mDateFormat;
        private bool                            mAppendTime;
        private string                          mTimeFormat;
        
        private bool                            mInputText;
        private bool                            mInputDate;
        private bool                            mAuditFilename;

        private bool                            mFileAttachments;
        private bool                            mIgnoreEmbeddedAttachments;
        private bool                            mUseEmailDate;

        private bool                            mAdvancedOptions;

        private BindingList<FilingTask>         mFilingTasks = new BindingList<FilingTask>();

        private long?                           mLastCompletionTime = null;
        private string                          mCompletionMessage = "";

        private List<string>                    mWarnings = new List<string>();


        //================================================================================
        //--------------------------------------------------------------------------------
        internal Filer(FilerManager manager, string group, string name, Color colour, string targetPath, string filenamePattern) {
            mManager = manager;
            mGroup = group;
            mName = name;
            mColour = colour;
            mTargetPath = targetPath;
            mFilenamePattern = filenamePattern;
        }

        //--------------------------------------------------------------------------------
        internal Filer(FilerManager manager) : this(manager, "", "", Color.Gray, null, null) { }

        //--------------------------------------------------------------------------------
        public void Dispose() { }


        // UPDATING ================================================================================
        //--------------------------------------------------------------------------------
        public void Update(float delta) {
            if (mLastCompletionTime != null) {
                if (TimeSinceLastCompletionTime > COMPLETION_MESSAGE_DURATION)
                    mLastCompletionTime = null;
                OnPropertyChanged("FilingInformation");
            }
        }


        // FILING ================================================================================
        //--------------------------------------------------------------------------------
        public FilingTask File(string sourceFilePath, Stream sourceStream, bool isDirectory = false, FilePathGenerator filePathGenerator = null, bool clearWarnings = true) {
            // Warnings
            if (clearWarnings)
                mWarnings.Clear();
            
            // Filing task
            FilingTask filingTask = AddFilingTask(new FilingTask(this, sourceFilePath/*.Replace(@"\", "/")*/, sourceStream, isDirectory, filePathGenerator ?? CreateFilePathGenerator()));
            
            // Filing information
            OnPropertyChanged("FilingInformation");

            // Return
            return filingTask;
        }

        //--------------------------------------------------------------------------------
        public FilingTask File(string sourceFilePath, FilePathGenerator filePathGenerator = null, bool clearWarnings = true) {
            bool isDirectory = System.IO.File.GetAttributes(sourceFilePath).HasFlag(FileAttributes.Directory);
            return File(sourceFilePath, !isDirectory ? new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read) : null, isDirectory, filePathGenerator, clearWarnings);
        }

        //--------------------------------------------------------------------------------
        public bool File(IDataObject data) {
            // Warnings
            mWarnings.Clear();

            // Variables
            bool filed = false;

            // File
            if (data.GetDataPresent(DataFormats.FileDrop, false)) {
                // Files
                string[] filenames = (string[])data.GetData(DataFormats.FileDrop);
                foreach (string f in filenames) {
                    File(f, CreateFilePathGenerator(), false);
                    filed = true;
                }
            }
            else {
                // Outlook attachments
                OutlookDataObject dataObject = new OutlookDataObject(data);
                string[] filenames = (string[])dataObject.GetData("FileGroupDescriptor");
                MemoryStream[] streams = (MemoryStream[])dataObject.GetData("FileContents");

                // Variables
                List<string> messagesWithoutAttachments = new List<string>();

                // Attachments
                for (int i = 0; i < filenames.Length; ++i) {
                    // Message
                    OutlookStorage.Message message = new OutlookStorage.Message(streams[i]);
                    DateTime? emailDate = message.ReceivedDate;
                    string messageBodyRTF = message.BodyRTF;
                    streams[i].Seek(0, SeekOrigin.Begin); // Otherwise below filing won't work for the msg file

                    // Files
                    if (!FileAttachments || !Path.GetExtension(filenames[i]).Equals(".msg")) {
                        File(filenames[i], streams[i], false, CreateFilePathGenerator(emailDate), false);
                        filed = true;
                    }
                    else {
                        // Attachments
                        if (message.Attachments.Count == 0)
                            messagesWithoutAttachments.Add(filenames[i]);
                        foreach (OutlookStorage.Attachment a in message.Attachments) {
                            bool embedded = (a.ContentId != null && messageBodyRTF.Contains($"cid:{a.ContentId}"));
                            if (!embedded || !IgnoreEmbeddedAttachments)
                                File(a.Filename, new MemoryStream(a.Data), false, CreateFilePathGenerator(emailDate), false);
                            filed = true;
                        }
                    }

                    // Dispose
                    message.Dispose();
                }

                // Missing attachments
                if (messagesWithoutAttachments.Count > 0)
                    mWarnings.Add($"The following e-mails had no attachments and were not filed:\n  {string.Join("\n  ", messagesWithoutAttachments)}");
            }

            // Return
            return filed;
        }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        internal void OnFilingTaskProgress(FilingTask filingTask) {
            // Filing information
            OnPropertyChanged("FilingInformation");
        }
        
        //--------------------------------------------------------------------------------
        internal void OnFilingTaskCompleted(FilingTask filingTask, Exception exception) {
            // Remove
            RemoveFilingTask(filingTask);

            // Completed task
            if (filingTask.Succeeded) {
                mManager.AddCompletedFilingTask(filingTask);
                mLastCompletionTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                //mCompletionMessage = "Filing completed.";
                mCompletionMessage = "Filed: " + filingTask.TargetFilename;
            }
            
            // Exception
            if (exception != null) {
                mLastCompletionTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                mCompletionMessage = (exception is DirectoryNotFoundException ? "Path not found." : "Error.");
            }

            // Filing information
            OnPropertyChanged("FilingInformation");
        }


        // INFORMATION ================================================================================
        //--------------------------------------------------------------------------------
        public string FilingInformation {
            get {
                // Completion message
                if (mFilingTasks.Count == 0 && mLastCompletionTime != null)
                    return mCompletionMessage.Substring(0, Math.Min((int)(TimeSinceLastCompletionTime / COMPLETION_MESSAGE_CHARACTER_INTERVAL) + 1, mCompletionMessage.Length));

                // Information
                if (mFilingTasks.Count > 0) {
                    int progress = (from t in mFilingTasks select t.Progress).Sum() / mFilingTasks.Count;
                    return progress + "% (" + mFilingTasks.Count + " remaining)";
                }

                // Nothing
                return "";
            }
        }

        //--------------------------------------------------------------------------------
        public long TimeSinceLastCompletionTime {
            get {
                if (mLastCompletionTime == null)
                    return -1;
                return (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - (long)mLastCompletionTime;
            }
        }


        // MANAGER ================================================================================
        //--------------------------------------------------------------------------------
        public FilerManager Manager { get { return mManager; } }


        // PRESENTATION ================================================================================
        //--------------------------------------------------------------------------------
        public string Group { set { SetProperty("Group", ref mGroup, value); } get { return mGroup; } }
        public string Name { set { SetProperty("Name", ref mName, value); } get { return mName; } }
        public Color Colour { set { SetProperty("Colour", ref mColour, value); } get { return mColour; } }


        // PATH ================================================================================
        //--------------------------------------------------------------------------------
        public string TargetPath { set { SetProperty("TargetPath", ref mTargetPath, value); } get { return mTargetPath; } }
        
        //--------------------------------------------------------------------------------
        public void BrowseToPath() {
            // Checks
            if (string.IsNullOrWhiteSpace(TargetPath))
                return;

            // Browse
            Process.Start(TargetPath);
        }


        // FILENAME PATTERN ================================================================================
        //--------------------------------------------------------------------------------
        public string FilenamePattern { set { SetProperty("FilenamePattern", ref mFilenamePattern, value); } get { return mFilenamePattern; } }
        public bool CustomFilenamePattern { set { mCustomFilenamePattern = value; } get { return mCustomFilenamePattern; } }
        
        //--------------------------------------------------------------------------------
        public static bool TestFilenamePattern(string filenamePattern, out string errorDescription) {
            // Initial state
            errorDescription = "";

            // Checks
            if (string.IsNullOrWhiteSpace(filenamePattern))
                return true;

            // Unallowed characters
            if (filenamePattern.IndexOfAny(UNALLOWED_FILENAME_CHARACTERS.ToCharArray()) != -1) {
                errorDescription = "A filename can't contain any of the following characters:";
                foreach (char c in UNALLOWED_FILENAME_CHARACTERS) {
                    errorDescription += " " + c;
                }
                return false;
            }

            // End character
            if (UNALLOWED_FILENAME_END_CHARACTERS.Contains(filenamePattern.Last())) {
                errorDescription = "Filenames cannot end in a space or dot";
                return false;
            }

            // Pass
            return true;
        }

        //--------------------------------------------------------------------------------
        public static string SanitiseFilename(string filename) {
            foreach (char c in UNALLOWED_FILENAME_CHARACTERS.ToCharArray()) {
                filename = filename.Replace(c, '_');
            }
            return filename;
        }


        // FILENAME ================================================================================
        //--------------------------------------------------------------------------------
        public bool Rename { set { mRename = value; } get { return mRename; } }
        public string RenameText { set { mRenameText = value; } get { return mRenameText; } }
        public ERenameMode RenameMode { set { mRenameMode = value; } get { return mRenameMode; } }

        //--------------------------------------------------------------------------------
        public string RenameModeString {
            set {
                switch (value.ToLower()) {
                    case "replace":         mRenameMode = ERenameMode.REPLACE; break;
                    case "add to front":    mRenameMode = ERenameMode.ADD_TO_FRONT; break;
                    case "add to end":      mRenameMode = ERenameMode.ADD_TO_END; break;
                    default:                mRenameMode = ERenameMode.REPLACE; break;
                }
            }
            get {
                switch (mRenameMode) {
                    case ERenameMode.REPLACE:       return "Replace";
                    case ERenameMode.ADD_TO_FRONT:  return "Add to front";
                    case ERenameMode.ADD_TO_END:    return "Add to end";
                    default:                        return "Replace";
                }
            }
        }


        // DATE / TIME ================================================================================
        //--------------------------------------------------------------------------------
        public bool AppendDate { set { mAppendDate = value; } get { return mAppendDate; } }
        public string DateFormat { set { mDateFormat = value; } get { return mDateFormat; } }
        public bool AppendTime { set { mAppendTime = value; } get { return mAppendTime; } }
        public string TimeFormat { set { mTimeFormat = value; } get { return mTimeFormat; } }
        

        // MANUAL INPUT ================================================================================
        //--------------------------------------------------------------------------------
        public bool AuditFilename { set { mAuditFilename = value; } get { return mAuditFilename; } }
        public bool InputText { set { mInputText = value; } get { return mInputText; } }
        public bool InputDate { set { mInputDate = value; } get { return mInputDate; } }

        
        // EMAILS ================================================================================
        //--------------------------------------------------------------------------------
        public bool FileAttachments { set { mFileAttachments = value; } get { return mFileAttachments; } }
        public bool IgnoreEmbeddedAttachments { set { mIgnoreEmbeddedAttachments = value; } get { return mIgnoreEmbeddedAttachments; } }
        public bool UseEmailDate { set { mUseEmailDate = value; } get { return mUseEmailDate; } }

        
        // ADVANCED OPTIONS ================================================================================
        //--------------------------------------------------------------------------------
        public bool AdvancedOptions { set { mAdvancedOptions = value; } get { return mAdvancedOptions; } }


        // FILE PATH GENERATORS ================================================================================
        //--------------------------------------------------------------------------------
        public FilePathGenerator CreateFilePathGenerator() {
            FilePathGenerator filePathGenerator = new FilePathGenerator(mTargetPath, mFilenamePattern);
            filePathGenerator.DateFormat = mDateFormat;
            filePathGenerator.TimeFormat = mTimeFormat;
            return filePathGenerator;
        }

        //--------------------------------------------------------------------------------
        protected FilePathGenerator CreateFilePathGenerator(DateTime? emailDate) {
            FilePathGenerator filePathGenerator = CreateFilePathGenerator();
            filePathGenerator.EmailDate = emailDate;
            return filePathGenerator;
        }


        // FILING TASKS ================================================================================
        //--------------------------------------------------------------------------------
        internal FilingTask AddFilingTask(FilingTask filingTask) {
            mFilingTasks.Add(filingTask);
            mManager.AddFilingTask(filingTask);
            return filingTask;
        }

        //--------------------------------------------------------------------------------
        internal void RemoveFilingTask(FilingTask filingTask, bool processFilingTasks = true) {
            mFilingTasks.Remove(filingTask);
            mManager.RemoveFilingTask(filingTask, processFilingTasks);
        }


        // COMPLETION MESSAGE ================================================================================
        //--------------------------------------------------------------------------------
        public string CompletionMessage { get { return mCompletionMessage; } }


        // WARNINGS ================================================================================
        //--------------------------------------------------------------------------------
        public List<string> Warnings { get { return mWarnings; } }
        public bool HasWarnings { get { return mWarnings.Count > 0; } }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        public void SaveSettings(JsonTextWriter writer) {
            // Display
            writer.WritePropertyName("Group"); writer.WriteValue(mGroup);
            writer.WritePropertyName("Name"); writer.WriteValue(mName);
            writer.WritePropertyName("Colour"); writer.WriteValue(mColour.ToArgb());

            // Path / filename pattern
            writer.WritePropertyName("TargetPath"); writer.WriteValue(mTargetPath);
            writer.WritePropertyName("FilenamePattern"); writer.WriteValue(mFilenamePattern);
            writer.WritePropertyName("CustomFilenamePattern"); writer.WriteValue(mCustomFilenamePattern);

            // Filename
            writer.WritePropertyName("Rename"); writer.WriteValue(mRename);
            writer.WritePropertyName("RenameText"); writer.WriteValue(mRenameText);
            writer.WritePropertyName("RenameMode"); writer.WriteValue(mRenameMode);

            // Date / time
            writer.WritePropertyName("AppendDate"); writer.WriteValue(mAppendDate);
            writer.WritePropertyName("DateFormat"); writer.WriteValue(mDateFormat);
            writer.WritePropertyName("AppendTime"); writer.WriteValue(mAppendTime);
            writer.WritePropertyName("TimeFormat"); writer.WriteValue(mTimeFormat);

            // Manual input
            writer.WritePropertyName("AuditFilename"); writer.WriteValue(mAuditFilename);
            writer.WritePropertyName("InputText"); writer.WriteValue(mInputText);
            writer.WritePropertyName("InputDate"); writer.WriteValue(mInputDate);

            // Emails
            writer.WritePropertyName("FileAttachments"); writer.WriteValue(mFileAttachments);
            writer.WritePropertyName("IgnoreEmbeddedAttachments"); writer.WriteValue(mIgnoreEmbeddedAttachments);
            writer.WritePropertyName("UseEmailDate"); writer.WriteValue(mUseEmailDate);

            // Advanced options
            writer.WritePropertyName("AdvancedOptions"); writer.WriteValue(mAdvancedOptions);            
        }
        
        //--------------------------------------------------------------------------------
        public void LoadSettings(JToken token) {
            // Display
            mGroup = (string)token.SelectToken("Group");
            mName = (string)token.SelectToken("Name");
            mColour = Color.FromArgb((int)token.SelectToken("Colour"));
            
            // Path / filename pattern
            mTargetPath = (string)token.SelectToken("TargetPath");
            mFilenamePattern = (string)token.SelectToken("FilenamePattern");
            mCustomFilenamePattern = (bool)(token.SelectToken("CustomFilenamePattern") ?? false);

            // Filename
            mRename = (bool)(token.SelectToken("Rename") ?? false);
            mRenameText = (string)(token.SelectToken("RenameText") ?? "");
            mRenameMode = (ERenameMode)(int)(token.SelectToken("RenameMode") ?? 0);

            // Date / time
            mAppendDate = (bool)token.SelectToken("AppendDate");
            mDateFormat = (string)(token.SelectToken("DateFormat") ?? "");
            mAppendTime = (bool)token.SelectToken("AppendTime");
            mTimeFormat = (string)(token.SelectToken("TimeFormat") ?? "");

            // Manual input
            mAuditFilename = (bool)(token.SelectToken("AuditFilename") ?? false);
            mInputText = (bool)(token.SelectToken("InputText") ?? false);
            mInputDate = (bool)(token.SelectToken("InputDate") ?? false);
            
            // Emails
            mFileAttachments = (bool)(token.SelectToken("FileAttachments") ?? false);
            mIgnoreEmbeddedAttachments = (bool)(token.SelectToken("IgnoreEmbeddedAttachments") ?? false);
            mUseEmailDate = (bool)(token.SelectToken("UseEmailDate") ?? false);

            // Advanced options
            mAdvancedOptions = (bool)(token.SelectToken("AdvancedOptions") ?? false);
        }
    }

}
