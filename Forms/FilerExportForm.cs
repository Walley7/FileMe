using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using FileMe.Filing;
using Newtonsoft.Json;
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

    public partial class FilerExportForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        private FilerManager                    mFilerManager;


        //================================================================================
        //--------------------------------------------------------------------------------
        public FilerExportForm(FileMeForm fileMeForm, FilerManager filerManager) {
            // Initialise
            InitializeComponent();

            // Top most
            TopMost = fileMeForm.AlwaysOnTop;

            // Filer manager
            mFilerManager = filerManager;
        }

        
        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void ExportForm_Shown(object sender, EventArgs e) {
            // Filers
            List<string> filerGroups = mFilerManager.FilerGroups;
            foreach (string g in filerGroups) {
                TreeListNode node = trlFilers.AppendNode(new object[] { g, "", null }, null);
                IEnumerable<Filer> filers = from Filer f in mFilerManager.Filers where f.Group.Equals(g) select f;
                foreach (Filer f in filers) {
                    trlFilers.AppendNode(new object[] { f.Name, f.TargetPath, f }, node);
                }
            }

            // Expand / check
            trlFilers.ExpandAll();
            CheckAllFilers();
        }

        
        // FILERS ================================================================================
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
        private void btnExport_Click(object sender, EventArgs e) {
            // Checks
            if (CheckedFilerCount == 0) {
                XtraMessageBox.Show(this, "Nothing selected for export", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Location
            if (dlgExportFilers.ShowDialog() != DialogResult.OK)
                return;

            // Export
            StreamWriter streamWriter = new StreamWriter(dlgExportFilers.FileName);
            JsonTextWriter writer = new JsonTextWriter(streamWriter);

            // Formatting
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;

            // Start
            writer.WriteStartObject();

            // Filers
            writer.WritePropertyName("Filers");
            writer.WriteStartArray();
            foreach (Filer f in CheckedFilers) {
                writer.WriteStartObject();
                f.SaveSettings(writer);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            // End
            writer.WriteEndObject();
            
            // Close
            streamWriter.Close();

            // Message
            XtraMessageBox.Show(this, $"Filers exported to '{Path.GetFileName(dlgExportFilers.FileName)}'", "File Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

}
