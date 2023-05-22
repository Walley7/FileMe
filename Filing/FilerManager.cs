using DevExpress.XtraEditors;
using FileMe.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FileMe.Filing {

    public class FilerManager {
        //================================================================================
        public const int                                DEFAULT_CONCURRENT_TASKS = 3;


        //================================================================================
        private BindingList<Filer>                      mFilers = new BindingList<Filer>();

        private BindingList<FilingTask>                 mFilingTasks = new BindingList<FilingTask>();

        private BindingList<CompletedFilingTask>        mCompletedFilingTasks = new BindingList<CompletedFilingTask>();

        private Dictionary<string, string>              mFilerGroupLatestPaths = new Dictionary<string, string>();

        //--------------------------------------------------------------------------------
        public event FilingTaskEventDelegate            FilingTaskProgressEvent = delegate { }; // To avoid null check and make thread safe
        public event FilingTaskCompletedEventDelegate   FilingTaskCompletedEvent = delegate { }; // To avoid null check and make thread safe


        //================================================================================
        //--------------------------------------------------------------------------------
        public FilerManager() { }


        // FILERS ================================================================================
        //--------------------------------------------------------------------------------
        public Filer CreateFiler(bool detached = false) {
            Filer filer = new Filer(this);
            if (!detached)
                AddFiler(filer);
            return filer;
        }

        //--------------------------------------------------------------------------------
        public void AddFiler(Filer filer) {
            // Name
            string name = filer.Name;
            if (HasFiler(name)) {
                int counter = 2;
                while (HasFiler(name + $" ({counter})")) {
                    ++counter;
                }
                name = name + $" ({counter})";
            }

            // Add
            filer.Name = name;
            mFilers.Add(filer);
        }

        //--------------------------------------------------------------------------------
        public void DestroyFiler(Filer filer) {
            if (mFilers.Remove(filer))
                filer.Dispose();
        }

        //--------------------------------------------------------------------------------
        public Filer Filer(string name, bool caseSensitive = true) {
            // Name
            if (!caseSensitive)
                name = name.ToLower();

            // Find
            foreach (Filer f in mFilers) {
                if ((caseSensitive ? f.Name : f.Name.ToLower()).Equals(name))
                    return f;
            }

            // Not found
            return null;
        }

        //--------------------------------------------------------------------------------
        public bool HasFiler(string name, bool caseSensitive = true) { return (Filer(name, caseSensitive) != null); }

        //--------------------------------------------------------------------------------
        public BindingList<Filer> Filers { get { return mFilers; } }

        //--------------------------------------------------------------------------------
        public void UpdateFilers(float delta) {
            foreach (Filer f in mFilers) {
                f.Update(delta);
            }
        }


        // FILER GROUPS ================================================================================
        //--------------------------------------------------------------------------------
        public List<string> FilerGroups {
            get {
                SortedSet<string> filerGroups = new SortedSet<string>();
                foreach (Filer f in mFilers) {
                    filerGroups.Add(f.Group);
                }
                return filerGroups.ToList();
            }
        }

        //--------------------------------------------------------------------------------
        public void SetFilerGroupLatestPath(string group, string path) {
            if (!string.IsNullOrWhiteSpace(path))
                mFilerGroupLatestPaths[group] = path;
            else if (mFilerGroupLatestPaths.ContainsKey(group))
                mFilerGroupLatestPaths.Remove(group);
        }
        
        //--------------------------------------------------------------------------------
        public string FilerGroupLatestPath(string group) { return mFilerGroupLatestPaths.ContainsKey(group) ? mFilerGroupLatestPaths[group] : ""; }
        public Dictionary<string, string> FilerGroupLatestPaths { get { return mFilerGroupLatestPaths; } }


        // FILING TASKS ================================================================================
        //--------------------------------------------------------------------------------
        internal void AddFilingTask(FilingTask filingTask) {
            mFilingTasks.Add(filingTask);
            ProcessFilingTasks();
        }

        //--------------------------------------------------------------------------------
        internal void RemoveFilingTask(FilingTask filingTask, bool processFilingTasks = true) {
            mFilingTasks.Remove(filingTask);
            if (processFilingTasks)
                ProcessFilingTasks();
        }

        //--------------------------------------------------------------------------------
        public void StopAllFilingTasks() {
            List<FilingTask> filingTasks = new List<FilingTask>(mFilingTasks);
            foreach (FilingTask t in filingTasks) {
                t.Stop();
            }
        }

        //--------------------------------------------------------------------------------
        public BindingList<FilingTask> FilingTasks { get { return mFilingTasks; } }

        //--------------------------------------------------------------------------------
        private void ProcessFilingTasks() {
            int activeTasks = (from t in mFilingTasks where t.Running select t).Count();
            List<FilingTask> pendingTasks = (from t in mFilingTasks where !t.Running select t).ToList();

            for (int i = 0; i < DEFAULT_CONCURRENT_TASKS - activeTasks; ++i) {
                if (i >= pendingTasks.Count())
                    break;
                pendingTasks[i].Start();
            }
        }

        //--------------------------------------------------------------------------------
        internal void InvokeFilingTaskProgressEvent(FilingTask filingTask) {
            FilingTaskProgressEvent?.Invoke(filingTask);
        }

        //--------------------------------------------------------------------------------
        internal void InvokeFilingTaskCompletedEvent(FilingTask filingTask, Exception exception) {
            FilingTaskCompletedEvent?.Invoke(filingTask, exception);
        }


        // COMPLETED FILING TASKS ================================================================================
        //--------------------------------------------------------------------------------
        internal CompletedFilingTask AddCompletedFilingTask(FilingTask filingTask) {
            CompletedFilingTask completedFilingTask = new CompletedFilingTask(filingTask);
            mCompletedFilingTasks.Add(completedFilingTask);
            return completedFilingTask;
        }

        //--------------------------------------------------------------------------------
        public void RemoveCompletedFilingTask(CompletedFilingTask completedFilingTask) {
            mCompletedFilingTasks.Remove(completedFilingTask);
        }

        //--------------------------------------------------------------------------------
        public void RemoveAllCompletedFilingTasks() {
            mCompletedFilingTasks.Clear();
        }
        
        //--------------------------------------------------------------------------------
        public BindingList<CompletedFilingTask> CompletedFilingTasks { get { return mCompletedFilingTasks; } }


        //================================================================================
        //********************************************************************************
        public delegate void FilingTaskEventDelegate(FilingTask task);
        public delegate void FilingTaskCompletedEventDelegate(FilingTask task, Exception exception);
    }

}
