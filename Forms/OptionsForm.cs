using CSACore.Hotkeys;
using DevExpress.XtraEditors;
using FileMe.Configuration;
using Standard.Licensing.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FileMe.Forms {

    public partial class OptionsForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        private FileMeForm                      mFileMeForm;

        private string                          mCurrentTheme;
        private string                          mCurrentTextSize;
        private int                             mCurrentTileWidth;
        private int                             mCurrentTileHeight;
        private double                          mCurrentFadeOpacity;


        //================================================================================
        //--------------------------------------------------------------------------------
        public OptionsForm(FileMeForm fileMeForm) {
            // Initialise
            InitializeComponent();

            // File me form
            mFileMeForm = fileMeForm;
            TopMost = mFileMeForm.AlwaysOnTop;

            // Version
            Version version = Assembly.GetExecutingAssembly().GetName().Version; // https://stackoverflow.com/questions/826777/how-to-have-an-auto-incrementing-version-number-visual-studio
            lblVersion.Text = version.Major + "." + version.Minor + "." + version.Build;

            // Hotkeys
            InitialiseGlobalHotkey();

            // Current values
            mCurrentTheme = mFileMeForm.Theme;
            mCurrentTextSize = mFileMeForm.TextSizeString;
            mCurrentTileWidth = mFileMeForm.TileSize.Width;
            mCurrentTileHeight = mFileMeForm.TileSize.Height;
            mCurrentFadeOpacity = mFileMeForm.FadeOpacity;

            // Options
            cboTheme.EditValue = mFileMeForm.Theme;
            cboTextSize.EditValue = mFileMeForm.TextSizeString;
            spnTileWidth.Value = mFileMeForm.TileSize.Width;
            spnTileHeight.Value = mFileMeForm.TileSize.Height;
            trkFadeOpacity.Value = (int)(mFileMeForm.FadeOpacity * 10.0);
            chkAlwaysOnTop.Checked = mFileMeForm.AlwaysOnTop;
            chkGlobalHotkey.Checked = mFileMeForm.GlobalHotkeyActive;
            cboGlobalHotkeyModifier.EditValue = mFileMeForm.GlobalHotkeyModifier;
            lueGlobalHotkeyKey.EditValue = mFileMeForm.GlobalHotkeyKey;
        }
        

        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (DialogResult == DialogResult.OK) {
                // Apply
                mFileMeForm.AlwaysOnTop = chkAlwaysOnTop.Checked;
                mFileMeForm.SetGlobalHotkey(chkGlobalHotkey.Checked, (HotkeyEvent.Modifier)GlobalHotkeyModifier, (Keys)lueGlobalHotkeyKey.EditValue);
                mFileMeForm.RegisterGlobalHotkey();
            }
            else {
                // Reset
                mFileMeForm.Theme = mCurrentTheme;
                mFileMeForm.TextSizeString = mCurrentTextSize;
                mFileMeForm.TileSize = new Size(mCurrentTileWidth, mCurrentTileHeight);
                mFileMeForm.FadeOpacity = mCurrentFadeOpacity;
            }
        }

        
        // DEFAULTS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnDefaults_Click(object sender, EventArgs e) {
            cboTheme.EditValue = "Black";
            spnTileWidth.Value = 180;
            spnTileHeight.Value = 60;
            trkFadeOpacity.Value = 5;
            chkAlwaysOnTop.Checked = true;
        }


        // THEME ================================================================================
        //--------------------------------------------------------------------------------
        private void cboTheme_EditValueChanged(object sender, EventArgs e) {
            mFileMeForm.Theme = cboTheme.EditValue.ToString();
            BringToFront(); // On theme change this window can end up behind others such as queue
        }

        //--------------------------------------------------------------------------------
        private void cboTextSize_EditValueChanged(object sender, EventArgs e) {
            mFileMeForm.TextSizeString = cboTextSize.EditValue.ToString();
        }


        // TILE DIMENSIONS ================================================================================
        //--------------------------------------------------------------------------------
        private void spnTileWidth_ValueChanged(object sender, EventArgs e) {
            mFileMeForm.TileSize = new Size((int)spnTileWidth.Value, mFileMeForm.TileSize.Height);
        }

        //--------------------------------------------------------------------------------
        private void spnTileHeight_ValueChanged(object sender, EventArgs e) {
            mFileMeForm.TileSize = new Size(mFileMeForm.TileSize.Width, (int)spnTileHeight.Value);
        }


        // FADE OPACITY ================================================================================
        //--------------------------------------------------------------------------------
        private void trkFadeOpacity_ValueChanged(object sender, EventArgs e) {
            mFileMeForm.FadeOpacity = trkFadeOpacity.Value / 10.0;
            lblFadeOpacity.Text = (trkFadeOpacity.Value * 10) + "%";
        }


        // GLOBAL HOTKEY ================================================================================
        //--------------------------------------------------------------------------------
        private void InitialiseGlobalHotkey() {
            // Hotkeys
            List<object> hotkeys = new List<object>();
            hotkeys.Add(Keys.Back);
            hotkeys.Add(Keys.Tab);
            hotkeys.Add(Keys.Pause);
            hotkeys.Add(Keys.PageUp);
            hotkeys.Add(Keys.PageDown);
            hotkeys.Add(Keys.End);
            hotkeys.Add(Keys.Home);
            hotkeys.Add(Keys.Insert);
            hotkeys.Add(Keys.Delete);
            hotkeys.Add(Keys.D0);
            hotkeys.Add(Keys.D1);
            hotkeys.Add(Keys.D2);
            hotkeys.Add(Keys.D3);
            hotkeys.Add(Keys.D4);
            hotkeys.Add(Keys.D5);
            hotkeys.Add(Keys.D6);
            hotkeys.Add(Keys.D7);
            hotkeys.Add(Keys.D8);
            hotkeys.Add(Keys.D9);
            hotkeys.Add(Keys.A);
            hotkeys.Add(Keys.B);
            hotkeys.Add(Keys.C);
            hotkeys.Add(Keys.D);
            hotkeys.Add(Keys.E);
            hotkeys.Add(Keys.F);
            hotkeys.Add(Keys.G);
            hotkeys.Add(Keys.H);
            hotkeys.Add(Keys.I);
            hotkeys.Add(Keys.J);
            hotkeys.Add(Keys.K);
            hotkeys.Add(Keys.L);
            hotkeys.Add(Keys.M);
            hotkeys.Add(Keys.N);
            hotkeys.Add(Keys.O);
            hotkeys.Add(Keys.P);
            hotkeys.Add(Keys.Q);
            hotkeys.Add(Keys.R);
            hotkeys.Add(Keys.S);
            hotkeys.Add(Keys.T);
            hotkeys.Add(Keys.U);
            hotkeys.Add(Keys.V);
            hotkeys.Add(Keys.W);
            hotkeys.Add(Keys.X);
            hotkeys.Add(Keys.Y);
            hotkeys.Add(Keys.Z);
            hotkeys.Add(Keys.NumPad0);
            hotkeys.Add(Keys.NumPad1);
            hotkeys.Add(Keys.NumPad2);
            hotkeys.Add(Keys.NumPad3);
            hotkeys.Add(Keys.NumPad4);
            hotkeys.Add(Keys.NumPad5);
            hotkeys.Add(Keys.NumPad6);
            hotkeys.Add(Keys.NumPad7);
            hotkeys.Add(Keys.NumPad8);
            hotkeys.Add(Keys.NumPad9);
            hotkeys.Add(Keys.Multiply);
            hotkeys.Add(Keys.Add);
            hotkeys.Add(Keys.Separator);
            hotkeys.Add(Keys.Subtract);
            hotkeys.Add(Keys.Decimal);
            hotkeys.Add(Keys.Divide);
            hotkeys.Add(Keys.F1);
            hotkeys.Add(Keys.F2);
            hotkeys.Add(Keys.F3);
            hotkeys.Add(Keys.F4);
            hotkeys.Add(Keys.F5);
            hotkeys.Add(Keys.F6);
            hotkeys.Add(Keys.F7);
            hotkeys.Add(Keys.F8);
            hotkeys.Add(Keys.F9);
            hotkeys.Add(Keys.F10);
            hotkeys.Add(Keys.F11);
            hotkeys.Add(Keys.F12);

            // Bind
            lueGlobalHotkeyKey.Properties.DataSource = hotkeys;
        }
        
        //--------------------------------------------------------------------------------
        public HotkeyEvent.Modifier GlobalHotkeyModifier {
            get {
                switch (cboGlobalHotkeyModifier.EditValue.ToString().ToLower()) {
                    case "ctrl":    return HotkeyEvent.Modifier.None;
                    case "alt":     return HotkeyEvent.Modifier.Alt;
                    case "shift":   return HotkeyEvent.Modifier.Shift;
                    default:        return HotkeyEvent.Modifier.None;
                }
            }
        }


        // LICENCE ================================================================================
        //--------------------------------------------------------------------------------
        private void btnLicenceInformation_Click(object sender, EventArgs e) {
            LicenceForm licenceForm = new LicenceForm(mFileMeForm);
            licenceForm.ShowDialog();
        }
       
    }

}
