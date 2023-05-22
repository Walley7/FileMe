using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using FileMe.Filing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FileMe.Forms {

    public partial class FilerImportForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        private FilerManager                    mFilerManager;

        private string                          mFilename = null;

        private List<Filer>                     mFilers = new List<Filer>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public FilerImportForm(FileMeForm fileMeForm, FilerManager filerManager) {
            // Initialise
            InitializeComponent();

            // Top most
            TopMost = fileMeForm.AlwaysOnTop;

            // Filer manager
            mFilerManager = filerManager;

            // Filename
            if (dlgImportFilers.ShowDialog(this) == DialogResult.OK) {
                mFilename = dlgImportFilers.FileName;
                if (!LoadFilers())
                    mFilename = null;
            }
        }
        

        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void ImportForm_Shown(object sender, EventArgs e) {
            // Checks
            if (mFilename == null)
                Close();

            // Load
            ShowFilers();

            // Expand / check
            trlFilers.ExpandAll();
            CheckAllFilers();
        }
        

        // FILERS ================================================================================
        //--------------------------------------------------------------------------------
        private bool LoadFilers() {
            try {
                // Load
                StreamReader streamReader = new StreamReader(mFilename);
                string json = streamReader.ReadToEnd();
                streamReader.Close();

                // Parse
                JObject jsonObject = JObject.Parse(json);

                // Filers
                JArray filers = (JArray)jsonObject.SelectToken("Filers");
                if (filers != null) {
                    foreach (JToken f in filers) {
                        Filer filer = mFilerManager.CreateFiler(true);
                        filer.LoadSettings(f);
                        mFilers.Add(filer);
                    }
                }
            }
            catch (JsonReaderException e) {
                XtraMessageBox.Show(this, $"File not valid for import: {e.Message}", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            catch (Exception) {
                XtraMessageBox.Show(this, $"File not valid for import: missing data", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Return
            return true;
        }

        //--------------------------------------------------------------------------------
        private void ShowFilers() {
            // Groups
            SortedSet<string> filerGroups = new SortedSet<string>();
            foreach (Filer f in mFilers) {
                filerGroups.Add(f.Group);
            }

            // Show
            foreach (string g in filerGroups) {
                TreeListNode node = trlFilers.AppendNode(new object[] { g, "", null }, null);
                IEnumerable<Filer> filers = from Filer f in mFilers where f.Group.Equals(g) select f;
                foreach (Filer f in filers) {
                    trlFilers.AppendNode(new object[] { f.Name, f.TargetPath, f }, node);
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        private void trlFilers_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
            if (e.Node.ParentNode == null) {
                // Group
                foreach (TreeListNode f in e.Node.Nodes) {
                    f.CheckState = e.Node.CheckState;
                }
            }
            else {
                // Filer
                int checkCount = 0;
                foreach (TreeListNode f in e.Node.ParentNode.Nodes) {
                    if (f.Checked)
                        ++checkCount;
                }

                if (checkCount == 0)
                    e.Node.ParentNode.CheckState = CheckState.Unchecked;
                else if (checkCount == e.Node.ParentNode.Nodes.Count)
                    e.Node.ParentNode.CheckState = CheckState.Checked;
                else
                    e.Node.ParentNode.CheckState = CheckState.Indeterminate;
            }
        }

        //--------------------------------------------------------------------------------
        private void CheckAllFilers() {
            foreach (TreeListNode g in trlFilers.Nodes) {
                g.CheckState = CheckState.Checked;
                foreach (TreeListNode f in g.Nodes) {
                    f.CheckState = CheckState.Checked;
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        private void UncheckAllFilers() {
            foreach (TreeListNode g in trlFilers.Nodes) {
                g.CheckState = CheckState.Unchecked;
                foreach (TreeListNode f in g.Nodes) {
                    f.CheckState = CheckState.Unchecked;
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        private List<Filer> CheckedFilers {
            get {
                List<Filer> filers = new List<Filer>();
                foreach (TreeListNode g in trlFilers.Nodes) {
                    foreach (TreeListNode f in g.Nodes) {
                        if (f.Checked)
                            filers.Add((Filer)f.GetValue(trlFilers_FilerColumn));
                    }
                }
                return filers;
            }
        }
        
        //--------------------------------------------------------------------------------
        private int CheckedFilerCount { get { return CheckedFilers.Count; } }


        // BUTTONS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnCheckAll_Click(object sender, EventArgs e) { CheckAllFilers(); }
        private void btnUncheckAll_Click(object sender, EventArgs e) { UncheckAllFilers(); }
        
        //--------------------------------------------------------------------------------
        private void btnImport_Click(object sender, EventArgs e) {
            // Checks
            if (CheckedFilerCount == 0) {
                XtraMessageBox.Show(this, "Nothing selected for import", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Filers
            foreach (Filer f in CheckedFilers) {
                mFilerManager.AddFiler(f);
            }

            // Message
            XtraMessageBox.Show(this, "Filers imported", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }

}
