using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor
{
    [System.Serializable]
    public class CardEngineInit
    {
        public static bool EssentialsImported { get => Directory.Exists("Assets/CardEngine/Config");} 
        bool initialized;
        public CardEngineInit() {}

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
    public class CardEngineInitWindow : EditorWindow {
        [SerializeField]
        CardEngineInit initializerObject;
        static CardEngineInitWindow initializerWindow;
        [MenuItem("CardEngine/Initialize")]
        public static void showInitWindow() {
            if(initializerWindow == null) {
                initializerWindow = GetWindow<CardEngineInitWindow>();
                initializerWindow.titleContent = new GUIContent("CardEngine Initializer");
                initializerWindow.Focus();
            }
        }
        void SetEditorWindowSize()
        {
            EditorWindow editorWindow = this;

            Vector2 windowSize = new Vector2(300, 210);
            editorWindow.minSize = windowSize;
            editorWindow.maxSize = windowSize;
        }
        private void OnEnable() {
            SetEditorWindowSize();
            if(initializerObject == null) {
                initializerObject = new CardEngineInit();
            }
        }
        private void OnGUI() {
            initializerObject.OnGUI();
        }
    }
}