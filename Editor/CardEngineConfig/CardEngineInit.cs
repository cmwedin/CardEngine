using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace SadSapphicGames.CardEngineEditor
{
    [System.Serializable]
    public class CardEngineInit
    {
        bool initialized;
        public CardEngineInit() {}

        public void OnGUI() {
            initialized = Directory.Exists("Assets/CardEngine");
            GUILayout.BeginVertical(); {
                // Display options to initialize CardEngine
                GUILayout.BeginVertical(EditorStyles.helpBox); {
                    GUI.enabled = !initialized;
                    if (GUILayout.Button("Initialize CardEngine")) {
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
        private void OnEnable() {
            if(initializerObject == null) {
                initializerObject = new CardEngineInit();
            }
        }
        private void OnGUI() {
            initializerObject.OnGUI();
        }
    }
}