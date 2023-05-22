using CSACore.Core;
using DevExpress.XtraEditors;
using FileMe.Filing;
using FileMe.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Standard.Licensing;
using Standard.Licensing.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FileMe.Configuration {

    public static class Settings {
        //================================================================================
        public const string                     COMPANY_DIRECTORY = "CSA";
        public const string                     APPLICATION_DIRECTORY = "File Me";
        public const string                     LICENCE_FILENAME = "licence.lic";
        public const string                     FILERS_FILENAME = "filers.json";
        public const string                     CATEGORIES_FILENAME = "categories.json";
        public const string                     LAYOUT_FILENAME = "layout.json";

        public const string                     PUBLIC_LICENCE_KEY = @"MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEEX5GkwxxmTkP3r3b1vqc6a4KsoBWCKsFnAwoV7Ho6bxUH/lMSrwWOFEHVtuscZTCSjE2xUbyI3HNaI4XLkH8jg==";


        //================================================================================
        private static CategoryManager          sCategoryManager;
        private static FilerManager             sFilerManager;

        private static FileMeForm               sFileMeForm;


        // INITIALISATION ================================================================================
        //--------------------------------------------------------------------------------
        public static void Initialise(CategoryManager categoryManager, FilerManager filerManager, FileMeForm fileMeForm) {
            // Managers
            sCategoryManager = categoryManager;
            sFilerManager = filerManager;

            // Forms
            sFileMeForm = fileMeForm;

            // Create path
            CSA.ApplicationData.CreateDataPaths();

            // Licence
            LoadLicence();
        }


        // LICENCE ================================================================================
        //--------------------------------------------------------------------------------
        public static void SaveLicence(string licence) {
            File.WriteAllText(LicencePath, licence);
        }

        //--------------------------------------------------------------------------------
        public static void LoadLicence() {
            CSA.Licencer.LoadLicenceFromFile(PUBLIC_LICENCE_KEY, LicencePath);
        }


        // FILERS ================================================================================
        //--------------------------------------------------------------------------------
        public static void SaveFilers() {
            // Open
            StreamWriter streamWriter = new StreamWriter(FilersSettingsPath);
            JsonTextWriter writer = new JsonTextWriter(streamWriter);

            // Formatting
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;

            // Start
            writer.WriteStartObject();

            // Filers
            writer.WritePropertyName("Filers");
            writer.WriteStartArray();
            foreach (Filer f in sFilerManager.Filers) {
                writer.WriteStartObject();
                f.SaveSettings(writer);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            // End
            writer.WriteEndObject();

            // Close
            streamWriter.Close();
        }
        
        //--------------------------------------------------------------------------------
        public static void LoadFilers() {
            // Reset
            sFilerManager.StopAllFilingTasks();
            sFilerManager.RemoveAllCompletedFilingTasks();

            // Open
            try {
                StreamReader streamReader = new StreamReader(FilersSettingsPath);
                string json = streamReader.ReadToEnd();
                streamReader.Close();

                // Parse
                JObject jsonObject = JObject.Parse(json);

                // Filers
                JArray filers = (JArray)jsonObject.SelectToken("Filers");
                if (filers != null) {
                    foreach (JToken f in filers) {
                        Filer filer = sFilerManager.CreateFiler();
                        filer.LoadSettings(f);
                    }
                }
            }
            catch (FileNotFoundException) { }
            catch (DirectoryNotFoundException) { }
        }


        // CATEGORIES ================================================================================
        //--------------------------------------------------------------------------------
        public static void SaveCategories() {
            // Open
            StreamWriter streamWriter = new StreamWriter(CategoriesSettingsPath);
            JsonTextWriter writer = new JsonTextWriter(streamWriter);

            // Formatting
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;

            // Start
            writer.WriteStartObject();

            // Categories
            writer.WritePropertyName("Categories");
            writer.WriteStartArray();
            foreach (Category c in sCategoryManager.Categories) {
                writer.WriteStartObject();
                c.SaveSettings(writer);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            // End
            writer.WriteEndObject();

            // Close
            streamWriter.Close();
        }

        //--------------------------------------------------------------------------------
        public static void LoadCategories() {
            // Open
            try {
                StreamReader streamReader = new StreamReader(CategoriesSettingsPath);
                string json = streamReader.ReadToEnd();
                streamReader.Close();

                // Parse
                JObject jsonObject = JObject.Parse(json);

                // Categories
                JArray categories = (JArray)jsonObject.SelectToken("Categories");
                if (categories != null) {
                    foreach (JToken c in categories) {
                        Category category = sCategoryManager.CreateCategory();
                        category.LoadSettings(c);
                    }
                }
            }
            catch (FileNotFoundException) { }
            catch (DirectoryNotFoundException) { }
        }


        // LAYOUT ================================================================================
        //--------------------------------------------------------------------------------
        public static void SaveLayout() {
            // Open
            StreamWriter streamWriter = new StreamWriter(LayoutSettingsPath);
            JsonTextWriter writer = new JsonTextWriter(streamWriter);

            // Formatting
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;

            // Start
            writer.WriteStartObject();

            // File me form
            writer.WritePropertyName("FileMeForm");
            writer.WriteStartObject();
            sFileMeForm.SaveSettings(writer);
            writer.WriteEndObject();

            // End
            writer.WriteEndObject();

            // Close
            streamWriter.Close();
        }

        //--------------------------------------------------------------------------------
        public static void LoadLayout() {
            // Open
            try {
                StreamReader streamReader = new StreamReader(LayoutSettingsPath);
                string json = streamReader.ReadToEnd();
                streamReader.Close();

                // Parse
                JObject jsonObject = JObject.Parse(json);

                // File me form
                sFileMeForm.LoadSettings(jsonObject.SelectToken("FileMeForm"));
            }
            catch (FileNotFoundException) { }
            catch (DirectoryNotFoundException) { }
        }


        // PATHS ================================================================================
        //--------------------------------------------------------------------------------
        public static string LicencePath { get { return Path.Combine(CSA.ApplicationData.ProgramDataPath, LICENCE_FILENAME); } }
        public static string FilersSettingsPath { get { return Path.Combine(CSA.ApplicationData.ProgramDataPath, FILERS_FILENAME); } }
        public static string CategoriesSettingsPath { get { return Path.Combine(CSA.ApplicationData.ProgramDataPath, CATEGORIES_FILENAME); } }
        public static string LayoutSettingsPath { get { return Path.Combine(CSA.ApplicationData.ProgramDataPath, LAYOUT_FILENAME); } }
    }

}
