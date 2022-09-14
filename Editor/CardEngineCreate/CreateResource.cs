using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;
using UnityEngine.Windows;

namespace SadSapphicGames.CardEngineEditor
{
    /// <summary>
    /// The object used to create a resource SO
    /// </summary>
    public class CreateResourceObject {
        /// <summary>
        /// Communicates to the wrapping window it should be closed
        /// </summary>
        public bool CloseWindow { get; private set; }
        /// <summary>
        /// The current directory set for resource SO's
        /// </summary>
        string resourceDirectory;
        /// <summary>
        /// The database of ResourceSO's
        /// </summary>
        ResourceDatabaseSO resourceDatabase;

        /// <summary>
        /// Constructs a CreateResourceObject
        /// </summary>
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
        /// <summary>
        /// Creates a ResourceSO with a given name
        /// </summary>
        /// <param name="resourceName">the name of the ResourceSO</param>
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

        /// <summary>
        /// Initializes this objects created ResourceSO
        /// </summary>
        /// <param name="resourceName"></param>
        public void InitializeResource(string resourceName) {
            ResourceSO resourceSO = (ResourceSO)ScriptableObject.CreateInstance(Type.GetType($"{resourceName},Assembly-csharp"));
            resourceSO.name = resourceName;
            AssetDatabase.CreateAsset(resourceSO,$"{resourceDirectory}/{resourceName}/{resourceName}.asset");

            EditorUtility.SetDirty(resourceSO);
            AssetDatabase.SaveAssets();
            CloseWindow = true;
        }
    }

    /// <summary>
    /// The window for the user to interact with the inner CreateResourceObject 
    /// </summary>
    public class CreateResourceWindow : EditorWindow {
        /// <summary>
        /// The inner CreateResourceObject this window wraps
        /// </summary>
        private CreateResourceObject windowObject;
        /// <summary>
        /// The Singleton instance used to determine when the ResourceSO class has finished compiling
        /// </summary>
        static CreateResourceWindow instance;
        /// <summary>
        /// is the window waiting for the resource to compile
        /// </summary>
        bool resourceCompiling;
        /// <summary>
        /// is the window waiting for the resource to initialize
        /// </summary>
        bool resourceInitializing;
        /// <summary>
        /// The entered name for the resource
        /// </summary>
        [SerializeField]string resourceName;
        /// <summary>
        /// Opens the CreateResourceWindow
        /// </summary>
        [MenuItem("CardEngine/Create/Resource")] static void Init() {
            if(instance != null) {
                Debug.LogWarning("Create resource window already open");
                return;
            }
            instance = EditorWindow.CreateInstance<CreateResourceWindow>();
            instance.Show();
        }
        /// <summary>
        /// Creates the inner CreateResourceObject
        /// </summary>
        private void OnEnable() {
            windowObject = new CreateResourceObject();
        }
        /// <summary>
        /// What will be displayed by the window
        /// </summary>
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