using CSACore.Utility;
using FileMe.Utility;
using SmartFormat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FileMe.Filing {

    public class FilePathGenerator {
        //================================================================================
        private string                          mTargetPath;
        private string                          mFilenamePattern;

        private string                          mDateFormat = "yyyy-MM-dd";
        private string                          mTimeFormat = "hh-mm-ss";
        
        private string                          mInputText = null;
        private DateTime?                       mInputDate = null;

        private DateTime?                       mEmailDate = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public FilePathGenerator(string targetPath, string filenamePattern) {
            mTargetPath = targetPath;
            mFilenamePattern = filenamePattern;
        }


        // PATH ================================================================================
        //--------------------------------------------------------------------------------
        public string GeneratePath(string sourceFilePath, bool clearInputsAtEnd = true) {
            string generatedFilePath = mTargetPath + "/" + GenerateFilename(sourceFilePath, clearInputsAtEnd);
            return UFile.IncrementalFreePath(generatedFilePath);
        }
        
        //--------------------------------------------------------------------------------
        public string TargetPath { get { return mTargetPath; } }


        // FILENAME ================================================================================
        //--------------------------------------------------------------------------------
        public string GenerateFilename(string filename, bool clearInputsAtEnd = true) {
            // Automatic inputs
            DateTime now = DateTime.Now;
            string filenameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
            string extension = Path.GetExtension(filename);
            string extensionWithoutDot = extension.Length > 0 ? extension.Remove(0, 1) : "";

            // Manual inputs
            string inputText = mInputText ?? "";
            DateTime inputDate = mInputDate ?? DateTime.Now;

            // Email inputs
            DateTime emailDate = mEmailDate ?? DateTime.Now;

            // Arguments
            object[] arguments = new object[] {
                new {
                    Filename = filename,
                    FilenameWithoutExtension = filenameWithoutExtension,
                    Extension = extension,
                    ExtensionWithoutDot = extensionWithoutDot,
                    //Date = now.Year.ToString("D4") + "-" + now.Month.ToString("D2") + "-" + now.Day.ToString("D2"),
                    Date = FormatDate(now),
                    Year = now.Year.ToString("D4"),
                    Month = now.Month.ToString("D2"),
                    Day = now.Day.ToString("D2"),
                    //Time = now.Hour.ToString("D2") + "-" + now.Minute.ToString("D2") + "-" + now.Second.ToString("D2"),
                    Time = FormatTime(now),
                    Hour = now.Hour.ToString("D2"),
                    Minute = now.Minute.ToString("D2"),
                    Second = now.Second.ToString("D2"),
                    Millisecond = now.Millisecond.ToString("D2"),
                    InputText = inputText,
                    InputDate = inputDate != DateTime.MinValue ? FormatDate(inputDate) : "",
                    EmailDate = emailDate != DateTime.MinValue ? FormatDate(emailDate) : ""
                }
            };

            // Format
            //return Smart.Format(FilenamePattern.Replace("\t", "").Replace("\n", ""), arguments);
            string generatedFilename = Smart.Format(MassagedFilenamePattern, arguments);

            // Clear inputs
            if (clearInputsAtEnd) {
                mInputText = null;
                mInputDate = null;
            }

            // Return
            return generatedFilename;
        }

        //--------------------------------------------------------------------------------
        public string FilenamePattern { set { mFilenamePattern = value; } get { return mFilenamePattern; } }
        
        //--------------------------------------------------------------------------------
        // We remove empty braces as smart format fills them in with the entire arguments
        // structure.
        public string MassagedFilenamePattern {
            get {
                // Replacements
                string pattern = FilenamePattern.Replace("\t", "").Replace("\n", "");

                // Braces
                while (true) {
                    pattern = pattern.Replace("{}", "");

                    int lastOpenBraceIndex = pattern.LastIndexOf("{");
                    int lastClosedBraceIndex = pattern.LastIndexOf("}");
                    if (lastOpenBraceIndex <= lastClosedBraceIndex)
                        break;

                    pattern = pattern + "}";
                }

                // Return
                return pattern;
            }
        }
        

        // FORMATS ================================================================================
        //--------------------------------------------------------------------------------
        public string FormatDate(DateTime date) {
            string dateString = date.ToString("yyyy-MM-dd");
            if (!string.IsNullOrWhiteSpace(mDateFormat)) {
                try { dateString = date.ToString(mDateFormat); }
                catch (Exception) { }
            }
            return dateString;
        }

        //--------------------------------------------------------------------------------
        public string DateFormat { set { mDateFormat = value; } get { return mDateFormat; } }

        //--------------------------------------------------------------------------------
        public string FormatTime(DateTime time) {
            string timeString = time.ToString("hh-mm-ss");
            if (!string.IsNullOrWhiteSpace(mTimeFormat)) {
                try { timeString = time.ToString(mTimeFormat); }
                catch (Exception) { }
            }
            return timeString;
        }

        //--------------------------------------------------------------------------------
        public string TimeFormat { set { mTimeFormat = value; } get { return mTimeFormat; } }
        

        // INPUTS ================================================================================
        //--------------------------------------------------------------------------------
        public string InputText { set { mInputText = value; } get { return mInputText; } }
        public DateTime? InputDate { set { mInputDate = value; } get { return mInputDate; } }
        public bool RequiresInputText { get { return MassagedFilenamePattern.Contains("{InputText}"); } }
        public bool RequiresInputDate { get { return MassagedFilenamePattern.Contains("{InputDate}"); } }


        // EMAILS ================================================================================
        //--------------------------------------------------------------------------------
        public DateTime? EmailDate { set { mEmailDate = value; } get { return mEmailDate; } }
    }

}
