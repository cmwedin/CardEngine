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
                        AssetDatabase.CreateFolder("Assets","CardEngine");
                        TemplateIO.CopyTemplate("DefaultSettings.json","settings.json","Assets/CardEngine");
                    }
                    GUILayout.Space(5f);
                    GUI.enabled = true;
                } GUILayout.EndVertical();
            } GUILayout.EndVertical();
        }
    }
}