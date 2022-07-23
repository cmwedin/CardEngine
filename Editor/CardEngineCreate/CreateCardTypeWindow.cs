using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;
using UnityEngine.Windows;

namespace SadSapphicGames.CardEngineEditor {
    public class CardEngineCreateMenu {

    }
    public class CreateCardTypeWindow : EditorWindow {

        string typeName = ""; 
        string typesDirectory;
        TypeDatabaseSO typeDatabase;

        public CreateCardTypeWindow() : base() {

        }
        private void OnEnable() {
            var settings = SettingsEditor.ReadSettings(); 
            typeDatabase = TypeDatabaseSO.Instance;

            if(typeDatabase == null || settings == null) {
                this.Close();
                Debug.LogWarning("please finish initializing CardEngine before using the CardEngine/Create menu");
            }
            
            typesDirectory = settings.Directories.CardTypes;
            if(!Directory.Exists(typesDirectory)) {
                this.Close();
                Debug.LogWarning("selected directory invalid, please select a valid directory to store card types using the CardEngine/Settings menu");
            }
            
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
                    if(Directory.Exists(typesDirectory + "/" + typeName)) {
                        this.Close();
                        throw new Exception($"Folder for type {typeName} already exists");
                    }
                    AssetDatabase.CreateFolder(typesDirectory, typeName);
                    string typePath = typesDirectory + "/" + typeName;
                    TemplateIO.CopyTemplate("CardTypeTemplate.cs",typeName+".cs",typePath);
                    AssetDatabase.ImportAsset($"{typePath}/{typeName}.cs");
                    TemplateIO.CopyTemplate("TypeDataSOTemplate.cs",typeName+"DataSO.cs",typePath);
                    AssetDatabase.ImportAsset($"{typePath}/{typeName}DataSO.cs");
                    AssetDatabase.Refresh();

                    TypeSO typeSO = ScriptableObject.CreateInstance<TypeSO>();
                    typeSO.name = typeName;
                    AssetDatabase.CreateAsset(typeSO,$"{typePath}/{typeName}.asset");
                    typeDatabase.AddEntry(typeSO, typePath);
                    AssetDatabase.SaveAssets();



                    Debug.LogWarning("Remember to initialize your new TypeSO asset");
                    this.Close();
                }
                if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                    this.Close();
                }
            GUILayout.EndHorizontal();
        }
    }
}