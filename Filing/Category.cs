using CSACore.Utility;
using DevExpress.XtraTab;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FileMe.Filing {

    public class Category : Bindable {
        //================================================================================
        private CategoryManager                 mManager;

        private string                          mName;
        private Color                           mColour;

        private bool                            mGroupStartsWithEnabled;
        private string                          mGroupStartsWith;

        private List<string>                    mGroups = new List<string>();

        private XtraTabPage                     mTabPage = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        internal Category(CategoryManager manager, string name, Color colour) {
            mManager = manager;
            mName = name;
            mColour = colour;
        }

        //--------------------------------------------------------------------------------
        internal Category(CategoryManager manager) : this(manager, "", Color.Gray) { }

        //--------------------------------------------------------------------------------
        public void Dispose() { }


        // PRESENTATION ================================================================================
        //--------------------------------------------------------------------------------
        public string Name { set { SetProperty("Name", ref mName, value); } get { return mName; } }
        public Color Colour { set { SetProperty("Colour", ref mColour, value); } get { return mColour; } }


        // GROUPS ================================================================================
        //--------------------------------------------------------------------------------
        public bool GroupStartsWithEnabled { set { mGroupStartsWithEnabled = value; } get { return mGroupStartsWithEnabled; } }
        public string GroupStartsWith { set { mGroupStartsWith = value; } get { return mGroupStartsWith; } }
        public List<string> Groups { get { return mGroups; } }
        public string GroupsString { get { return string.Join(", ", mGroups); } }


        // TAB PAGE ================================================================================
        //--------------------------------------------------------------------------------
        public XtraTabPage TabPage { set { mTabPage = value; } get { return mTabPage; } }


        // SETTINGS ================================================================================
        //--------------------------------------------------------------------------------
        public void SaveSettings(JsonTextWriter writer) {
            // Display
            writer.WritePropertyName("Name"); writer.WriteValue(mName);
            writer.WritePropertyName("Colour"); writer.WriteValue(mColour.ToArgb());

            // Group starts with
            writer.WritePropertyName("GroupStartsWithEnabled"); writer.WriteValue(mGroupStartsWithEnabled);
            writer.WritePropertyName("GroupStartsWith"); writer.WriteValue(mGroupStartsWith);

            // Groups
            writer.WritePropertyName("Groups");
            writer.WriteRawValue(JsonConvert.SerializeObject(mGroups));
        }
        
        //--------------------------------------------------------------------------------
        public void LoadSettings(JToken token) {
            // Display
            mName = (string)token.SelectToken("Name");
            mColour = Color.FromArgb((int)token.SelectToken("Colour"));
            
            // Path / filename
            mGroupStartsWithEnabled = (bool)token.SelectToken("GroupStartsWithEnabled");
            mGroupStartsWith = (string)token.SelectToken("GroupStartsWith");
            
            // Groups
            JArray groups = (JArray)token.SelectToken("Groups");
            if (groups != null) {
                foreach (JToken g in groups) {
                    mGroups.Add((string)g);
                }
            }
        }
    }

}
