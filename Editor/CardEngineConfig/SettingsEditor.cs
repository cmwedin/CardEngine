using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor {
    //? we use a class not a struct for the root object so that it is nullable
    /// <summary>
    /// The class used to represent the settings json
    /// </summary>
    [System.Serializable] public class Settings {
        public Directories Directories;
    }
    /// <summary>
    /// The struct used to store the values of the directories entry of the setting json
    /// </summary>
    [System.Serializable]
    public struct Directories {
        public string CardTypes;
        public string CardScriptableObjects;
        public string Effects;
        public string Resources;
    }
    /// <summary>
    /// the static class used to modify the entries of the settings json
    /// </summary>
    public static class SettingsEditor {
        /// <summary>
        /// The path for the settings json
        /// </summary>
        static string settingsPath = $"Assets/CardEngine/config/settings.json";

        /// <summary>
        /// The menu item to set the value of the CardType directory in the settings json
        /// </summary>
        [MenuItem("Tools/CardEngine/Directories/Set CardType directory")] private static void SetCardTypeDirectory() {
            Settings settings;
            try {
                settings = ReadSettings();
            } catch (SettingsNotFoundException) {
                return;
            }
            
            string path = ConvertAbsoluteToRelativePath(EditorUtility.OpenFolderPanel("Select Directory","",""));
            if(path == "") return;

            settings.Directories.CardTypes = path;
            WriteSettings(settings);
        }
        /// <summary>
        /// The menu item to set the value of the Effect directory in the settings json
        /// </summary>
        [MenuItem("Tools/CardEngine/Directories/Set Effect directory")] private static void SetEffectDirectory() {
            Settings settings;
            try {
                settings = ReadSettings();
            } catch (SettingsNotFoundException) {
                return;
            }

            string path = ConvertAbsoluteToRelativePath(EditorUtility.OpenFolderPanel("Select Directory","",""));
            if(path == "") return;

            settings.Directories.Effects = path;
            WriteSettings(settings);
        }
        /// <summary>
        /// The menu item to set the value of the CardSO directory in the settings json
        /// </summary>
        [MenuItem("Tools/CardEngine/Directories/Set CardScriptableObject directory")] private static void SetCardScriptableObjectDirectory() {
            Settings settings;
            try {
                settings = ReadSettings();
            } catch (SettingsNotFoundException) {
                return;
            }

            string path = ConvertAbsoluteToRelativePath(EditorUtility.OpenFolderPanel("Select Directory","",""));
            if(path == "") return;
            
            settings.Directories.CardScriptableObjects = path;
            WriteSettings(settings);
        }
        /// <summary>
        /// The menu item to set the value of the Resource directory in the settings json
        /// </summary>
        [MenuItem("CardEngine/Settings/Set Resource directory")] private static void SetResourceDirectory() {
            Settings settings;
            try {
                settings = ReadSettings();
            } catch (SettingsNotFoundException) {
                return;
            }

            string path = ConvertAbsoluteToRelativePath(EditorUtility.OpenFolderPanel("Select Directory","",""));
            if(path == "") return;
            
            settings.Directories.Resources = path;
            WriteSettings(settings);
        }

        /// <summary>
        /// Converts the contents of the settings json into a Settings object
        /// </summary>
        /// <returns> The settings object created from the current values of the settings json</returns>
        public static Settings ReadSettings() {
            if(!CardEngineInit.EssentialsImported) {
                Debug.LogWarning("Settings file not found, please initialize");
                CardEngineInitWindow.showInitWindow();
                throw new SettingsNotFoundException(); 
            }
            using StreamReader reader = new StreamReader(settingsPath);
            string json = reader.ReadToEnd();
            reader.Close();
            Settings settings = JsonUtility.FromJson<Settings>(json);
            return settings;
        }
        /// <summary>
        /// Sets the values of the settings json to the 
        /// </summary>
        /// <param name="settings"></param>
        private static void WriteSettings(Settings settings) {
            string json = JsonUtility.ToJson(settings);
            using StreamWriter writer = new StreamWriter(settingsPath);
            writer.WriteLine(json);
            writer.Close();
        }
        /// <summary>
        /// Converts the absolute path of an asset on the hard drive to the relative path of an asset in the project
        /// </summary>
        /// <param name="absolutePath">The path of the hard drive</param>
        /// <returns>the path of an asset in the project</returns>
        public static string ConvertAbsoluteToRelativePath (string absolutePath) {
            if(absolutePath.StartsWith(Application.dataPath)) {
                return "Assets" + absolutePath.Substring(Application.dataPath.Length);
            } else return null;
        }
    }
    /// <summary>
    /// An exception thrown if the settings file could not be found
    /// </summary>
    [System.Serializable]
    public class SettingsNotFoundException : System.Exception
    {
        public SettingsNotFoundException() : base("Failed to find settings.json file") { }
        public SettingsNotFoundException(string message) : base(message) { }
        public SettingsNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected SettingsNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
