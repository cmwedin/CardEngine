using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor
{
    /// <summary>
    /// The object that handles importing essential resources from their .unitypackage file 
    /// </summary>
    [System.Serializable] public class CardEngineInit
    {
        //TODO this probably needs to be tweaked to support reimporting essential resources if the contents of the config folder are deleted
        /// <summary>
        /// Static property to determine if the config folder exist 
        /// </summary>
        public static bool EssentialsImported { get => Directory.Exists("Assets/CardEngine/Config");} 
        /// <summary>
        /// if the initialization process has finished or not
        /// </summary>
        bool initialized;

        /// <summary>
        /// What to display on the GUI of the importer window
        /// </summary>
        public void OnGUI() {
            initialized = Directory.Exists("Assets/CardEngine");
            GUILayout.BeginVertical(); {
                // Display options to initialize CardEngine
                GUILayout.BeginVertical(EditorStyles.helpBox); {
                    GUI.enabled = !initialized;
                    GUILayout.Label("CardEngine Essentials", EditorStyles.boldLabel);
                    GUILayout.Label("Essential resources to run CardEngine have not been found. Please import these resource to use the package. They will be created in \\Assets\\CardEngine\\Config folder. Do not move these files.", new GUIStyle(EditorStyles.label) { wordWrap = true } );
                    if (GUILayout.Button("Import Essential Resources")) {
                        AssetDatabase.ImportPackage(CardEngineIO.GetPackagePath() + "/PackageResources/CardEngineConfig.unitypackage", false);

                        // AssetDatabase.CreateFolder("Assets","CardEngine");
                        // TemplateIO.CopyTemplate("DefaultSettings.json","settings.json","Assets/CardEngine");
                    }
                    GUILayout.Space(5f);
                    GUI.enabled = true;
                } GUILayout.EndVertical();
            } GUILayout.EndVertical();
        }
        
    }
    /// <summary>
    /// The window which wrapper the initializer object
    /// </summary>
    public class CardEngineInitWindow : EditorWindow {
        /// <summary>
        /// The object that actually preforms the initialization
        /// </summary>
        [SerializeField] CardEngineInit initializerObject;
        /// <summary>
        /// Singleton instance of this window
        /// </summary>
        static CardEngineInitWindow initializerWindow;
        /// <summary>
        /// Static method to open the initializer window if it isn't open already
        /// </summary>
        [MenuItem("Tools/CardEngine/Initialize")] public static void showInitWindow() {
            if(initializerWindow == null) {
                initializerWindow = GetWindow<CardEngineInitWindow>();
                initializerWindow.titleContent = new GUIContent("CardEngine Initializer");
                initializerWindow.Focus();
            }
        }
        /// <summary>
        /// Sets the size of the init window
        /// </summary>
        void SetEditorWindowSize()
        {
            Vector2 windowSize = new Vector2(300, 210);
            initializerWindow.minSize = windowSize;
            initializerWindow.maxSize = windowSize;
        }
        /// <summary>
        /// Sets the inner initializer object and invokes SetEditorWindowSize 
        /// </summary>
        private void OnEnable() {
            initializerWindow = this;
            if(initializerObject == null) {
                initializerObject = new CardEngineInit();
            }
            SetEditorWindowSize();
        }
        /// <summary>
        /// Wraps the OnGUI method of the inner initializer object
        /// </summary>
        private void OnGUI() {
            initializerObject.OnGUI();
        }
    }
}