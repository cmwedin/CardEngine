using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor {
    public class CardEngineCreateMenu {

    }
    public class CreateCardTypeWindow : EditorWindow {

        string typeName = ""; 
        string typesDirectory;
        string monobehaviourTemplatePath;

        public CreateCardTypeWindow() : base() {
            var settings = SettingsEditor.ReadSettings(); 
            typesDirectory = settings.Directories.CardTypes;
            monobehaviourTemplatePath = settings.Directories.Package + "/Editor/Templates/CardTypeTemplate.cs";
        }
        [MenuItem("CardEngine/Create/Card Type")]
        static void Init() {
            EditorWindow window = EditorWindow.CreateInstance<CreateCardTypeWindow>();
            window.Show();
        }
        private void OnGUI() {
            GUILayout.Label("Create a card type", EditorStyles.boldLabel);
            typeName = EditorGUILayout.TextField("Enter Type name",typeName);
            GUILayout.BeginHorizontal();
                if(GUILayout.Button("Create Type",EditorStyles.miniButtonLeft)) {
                    if(typeName == "") {
                        Debug.LogWarning("No type name entered");
                        this.Close();
                    }
                    AssetDatabase.CreateFolder(typesDirectory, typeName);
                    string typePath = typesDirectory + "/" + typeName;
                    TemplateIO.CopyTemplate(monobehaviourTemplatePath,typeName+".cs",typePath);
                    AssetDatabase.Refresh();
                    this.Close();
                }
                if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                    this.Close();
                }
            GUILayout.EndHorizontal();
        }
    }
}