using CSACore.Help;
using FileMe.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FileMe.Tutorials {

    class FileMeTutorial : Tutorial {
        //================================================================================
        public enum EState {
            LAUNCHED_FROM_HELP,
            ADD_FILER,
            ADD_FILER2,
            ADD_FILER3,
            ADD_FILER4,
            ADD_FILER5,
            FILING,
            FILING2,
            FILING3
        }


        //================================================================================
        private Form                            mMainForm;

        private EState                          mState = EState.ADD_FILER;


        //================================================================================
        //--------------------------------------------------------------------------------
        public FileMeTutorial(Form mainForm) {
            mMainForm = mainForm;
        }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        protected override void OnEvent(string identifier, params object[] data) {
            // Form
            Form form = (Form)data[0];

            // General
            if (identifier.Equals("file_me_focused") && mState == EState.LAUNCHED_FROM_HELP) {
                ShowBalloon(PaddedText("Click here to create a filer."), mMainForm, "btnAdd");
                mState = EState.ADD_FILER;
            }

            else if (identifier.Equals("add_filer") && mState == EState.ADD_FILER) {
                // Add filer
                ShowBalloon(PaddedText("Enter a name and a group - these are free text fields.\nYou can also select a different colour if you'd like."), form, "txtName");
                mState = EState.ADD_FILER;
            }

            else if (identifier.Equals("filer_name_group_entered") && mState == EState.ADD_FILER) {
                // Filer name group entered
                ShowBalloon(PaddedText("Click here to select a target path.\nThis is where files dropped onto this filer will be copied to."), form, "btnBrowseTargetPath");
                mState = EState.ADD_FILER2;
            }

            else if (identifier.Equals("filer_path_selected") && mState == EState.ADD_FILER2) {
                // Filer path selected
                ShowBalloon(PaddedText("There are various options here which can allow you to:\n" +
                                        "- Set a name for all filed files to be renamed to.\n" +
                                        "- Add dates or times to the end of filenames.\n" +
                                        "- Define a totally custom filename format.\n" +
                                        "- Have an e-mails attachments filed independently.\n\n" +
                                        "Hover over each option for more information, or try them out.\n" +
                                        "The preview at the bottom will show you what the filename will look like.\n\n" +
                                        "For now let's change this filer to keep the file's name but add the date to the end.\n" +
                                        "Start by unticking the Filename checkbox."), form, "chkFilename");
                mState = EState.ADD_FILER3;
            }

            else if (identifier.Equals("filer_filename_unchecked") && mState == EState.ADD_FILER3) {
                // Filer filename unchecked
                ShowBalloon(PaddedText("Now tick the Date checkbox.\n" +
                                       "Notice how the filename preview updates below."), form, "chkAppendDate");
                mState = EState.ADD_FILER4;
            }

            else if (identifier.Equals("filer_date_checked") && mState == EState.ADD_FILER4) {
                // Filer filename unchecked
                ShowBalloon(PaddedText("And now we're ready to get onto the fun stuff! Click here to finish adding the filer."), form, "btnOk");
                mState = EState.ADD_FILER5;
            }

            else if ((identifier.Equals("filer_added") && mState == EState.ADD_FILER5) || (identifier.Equals("file_me_focused") && mState == EState.FILING)) {
                // Filer added
                ShowBalloon(PaddedText("You should now see your new filer as a tile in the grid below.\n" +
                                       "If you can't see it try scrolling down.\n\n" +
                                       "You can interact with a filing tile in the following ways:\n" +
                                       "- Hovering over it will reveal the Edit and Delete buttons.\n" +
                                       "- Left clicking it will browse to the path where it sends files (if the path currently exists!).\n" +
                                       "- Right clicking will bring up a menu where you can also copy, import and export filers.\n\n" +
                                       "And finally the most important function: dragging files onto the tile will file them for you.\n" +
                                       "Let's try that now - find a file on your computer, such as a word document, and drag it onto the tile."), form, "grdTiles");
                mState = EState.FILING;
            }

            else if (identifier.Equals("filer_cancelled") && mState < EState.FILING) {
                // Filer cancelled
                mState = EState.ADD_FILER;
            }

            else if (identifier.Equals("filing_started") && mState == EState.FILING) {
                // Filing started
                ShowBalloon(PaddedText("Your file is now being filed. For large files this can take a while.\n" +
                                       "You can see the progress of the filing at the bottom of the filer's tile.\n\n" +
                                       "Once the filing is completed click on the filer's tile to browse to the destination folder.\n" +
                                       "Here you should see the file with the current date at the end of the name!"), form, "grdTiles");
                mState = EState.FILING2;
            }

            else if (identifier.Equals("browse_to_path") && mState == EState.FILING2) {
                // Browse to path
                mState = EState.FILING3;
            }

            else if (identifier.Equals("file_me_focused") && mState == EState.FILING3) {
                TutorialSystem.StopTutorial();
                FileMeForm fileMeForm = (FileMeForm)form;
                fileMeForm.ShowHelp(true);
            }
        }

        //--------------------------------------------------------------------------------
        protected override void OnStart(params object[] arguments) {
            mState = EState.LAUNCHED_FROM_HELP;
            if (arguments.Length == 0 || !(bool)arguments[0])
                NotifyEvent("file_me_focused", mMainForm);
        }

        //--------------------------------------------------------------------------------
        protected override void OnStop() {
            HideTooltips();
        }
    }

}
