using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;
using UnityEngine.Windows;

namespace SadSapphicGames.CardEngineEditor
{
    public class CreateResourceObject {
        public bool CloseWindow { get; private set; }
        string resourceDirectory;
        ResourceDatabaseSO resourceDatabase;

        public CreateResourceObject()
        {
            var settings = SettingsEditor.ReadSettings();
            resourceDatabase = ResourceDatabaseSO.Instance;
            if(resourceDatabase == null || settings == null) {
                CloseWindow = true;
                Debug.LogWarning("please finish initializing CardEngine before using the CardEngine/Create menu");
            } else {
                resourceDirectory = settings.Directories.Resources;
                if(!Directory.Exists(resourceDirectory)) {
                    CloseWindow = true;
                    Debug.LogWarning("selected directory invalid, please select a valid directory to store resources using the CardEngine/Settings menu");
                }
            }
        }

        public void CreateResource(string resourceName) {
            if(resourceName == "") {
                CloseWindow = true;
                Debug.Log("No resource name entered");
                return;
            }
            resourceName = resourceName.Replace(" ", string.Empty);
            if(Directory.Exists(resourceDirectory + "/" + resourceName)) {
                    CloseWindow = true;
                    Debug.LogWarning($"Folder for resource {resourceName} already exists, please delete it before creating a new type with that name");
                    return;
            }

            AssetDatabase.CreateFolder(resourceDirectory,resourceName);
            string resourcePath = $"{resourceDirectory}/{resourceName}";
            TemplateIO.CopyTemplate("ResourceTemplate.cs",$"{resourceName}.cs",resourcePath);
            TemplateIO.CopyTemplate("PayResourceCommandTemplate.cs",$"Pay{resourceName}Command.cs",resourcePath);
            AssetDatabase.ImportAsset($"{resourcePath}/{resourceName}.cs");
            AssetDatabase.ImportAsset($"{resourcePath}/Pay{resourceName}Command.cs");

            AssetDatabase.SaveAssets();

        }

        public void InitializeResource(string resourceName) {
            ResourceSO resourceSO = (ResourceSO)ScriptableObject.CreateInstance(Type.GetType($"{resourceName},Assembly-csharp"));
            resourceSO.name = resourceName;
            AssetDatabase.CreateAsset(resourceSO,$"{resourceDirectory}/{resourceName}/{resourceName}.asset");

            EditorUtility.SetDirty(resourceSO);
            AssetDatabase.SaveAssets();
            CloseWindow = true;
        }
    }
    public class CreateResourceWindow : EditorWindow {
        private CreateResourceObject windowObject;
        static CreateResourceWindow instance;
        bool resourceCompiling;
        bool resourceInitializing;
        [SerializeField]string resourceName;
        
        [MenuItem("CardEngine/Create/Resource")]
        static void Init() {
            if(instance != null) {
                Debug.LogWarning("Create resource window already open");
            }
            instance = EditorWindow.CreateInstance<CreateResourceWindow>();
            instance.Show();
        }

        private void OnEnable() {
            windowObject = new CreateResourceObject();
        }

        private void OnGUI() {
            if(windowObject.CloseWindow) {this.Close();}
            if(!resourceCompiling) {
                GUILayout.Label("Create New Resource");
                resourceName = EditorGUILayout.TextField("Enter Resource name:",resourceName); 
                GUILayout.BeginHorizontal();
                    if(GUILayout.Button("Create Resource",EditorStyles.miniButtonLeft)) {
                        windowObject.CreateResource(resourceName);
                        resourceCompiling = true;
                    }
                    if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                        this.Close();
                    }
                GUILayout.EndHorizontal();
            } else if(resourceCompiling) {
                GUILayout.Label("please wait while resource compiles");
            }
            if(instance == null && !resourceInitializing) {
                resourceInitializing = true;
                windowObject.InitializeResource(resourceName);
            }
        }
    }
}