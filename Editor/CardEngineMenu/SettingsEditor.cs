using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor {
    [System.Serializable]
    public struct Settings {
        public Directories Directories;
    }
    [System.Serializable]
    public struct Directories {
        public string CardTypes;
        public string CardScriptableObjects;
        public string Package;
    }
    public class SettingsEditor : EditorWindow {

        static string settingsPath = "Assets/CardEngine/Settings.json";
        // [MenuItem("CardEngine/Settings")]
        // private static void ShowWindow() {
        //     var window = GetWindow<SettingsEditor>();
        //     window.titleContent = new GUIContent("Settings");
        //     window.Show();
        // }
        [MenuItem("CardEngine/Settings/Set CardType directory")]
        private static void SetCardTypeDirectory() {
            string path = ConvertAbsoluteToRelativePath(EditorUtility.OpenFolderPanel("Select Directory","",""));
            if(path == "") return;
            
            var settings = ReadSettings();
            settings.Directories.CardTypes = path;
            WriteSettings(settings);
        }
        [MenuItem("CardEngine/Settings/Set CardScriptableObject directory")]
        private static void SetCardScriptableObjectDirectory() {
            string path = ConvertAbsoluteToRelativePath(EditorUtility.OpenFolderPanel("Select Directory","",""));
            if(path == "") return;
            
            var settings = ReadSettings();
            settings.Directories.CardScriptableObjects = path;
            WriteSettings(settings);
        }
        private void OnGUI() {
            // if(GUILayout.Button("Select CardType Directory")) {
            //     string path = EditorUtility.OpenFolderPanel("Select Directory","","");
            //     var settings = ReadSettings();
            //     settings.Directories.CardTypes = path;
            //     WriteSettings(settings);
            // }
            // if(GUILayout.Button("Select CardScriptableObject Directory")) {
            //     string path = EditorUtility.OpenFolderPanel("Select Directory","","");
            //     var settings = ReadSettings();
            //     settings.Directories.CardScriptableObjects = path;
            //     WriteSettings(settings);
            // }
        }
        public static Settings ReadSettings() {
            StreamReader reader = new StreamReader(settingsPath);
            string json = reader.ReadToEnd();
            reader.Close();
            // Debug.Log($"settings json: {json}");
            Settings settings = JsonUtility.FromJson<Settings>(json);
            // Debug.Log($"settings struct values, CardType:{settings.Directories.CardTypes},CardScriptableObjects:{settings.Directories.CardScriptableObjects}");
            return settings;
        }
        private static void WriteSettings(Settings settings) {
            string json = JsonUtility.ToJson(settings);
            StreamWriter writer = new StreamWriter(settingsPath);
            writer.WriteLine(json);
            writer.Close();
        }
        public static string ConvertAbsoluteToRelativePath (string absolutePath) {
            if(absolutePath.StartsWith(Application.dataPath)) {
                return "Assets" + absolutePath.Substring(Application.dataPath.Length);
            } else return null;
        }
    }
}
