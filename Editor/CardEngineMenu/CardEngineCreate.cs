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

        public CreateCardTypeWindow() : base() {
            var settings = SettingsEditor.ReadSettings(); 
            typesDirectory = settings.Directories.CardTypes;
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
    public class CreateCardWindow : EditorWindow {
        string cardName;
        string cardText;
        string cardsDirectory;
        public CreateCardWindow() : base() {
            var settings = SettingsEditor.ReadSettings(); 
            cardsDirectory = settings.Directories.CardScriptableObjects;
        }
        [MenuItem("CardEngine/Create/Card")]
        static void Init() {
            EditorWindow window = EditorWindow.CreateInstance<CreateCardWindow>();
            window.Show();
        }
        private void OnGUI() {
            GUILayout.Label("Create a card type", EditorStyles.boldLabel);
            GUILayout.BeginVertical();
                cardName = EditorGUILayout.TextField("Enter card name",cardName);
                GUILayout.Label("Card text:");
                cardText = EditorGUILayout.TextArea(cardText);
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();
                if(GUILayout.Button("Create Card",EditorStyles.miniButtonLeft)) {
                    if(cardName == "") {
                        Debug.LogWarning("Card name required");
                        this.Close();
                    }
                    if(Directory.Exists(cardsDirectory + "/" + cardName)) {
                        this.Close();
                        throw new Exception($"Folder for card {cardName} already exists");
                    }
                    AssetDatabase.CreateFolder(cardsDirectory, cardName);
                    string cardPath = cardsDirectory + "/" + cardName;

                    CardSO cardSO = ScriptableObject.CreateInstance<CardSO>();
                    cardSO.name = cardName;
                    cardSO.CardText = cardText;
                                        
                    AssetDatabase.CreateAsset(cardSO,$"{cardPath}/{cardSO.name}.asset");
                    AssetDatabase.SaveAssets();
                    this.Close();
                }
                if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                    this.Close();
                }
            GUILayout.EndHorizontal();
        }
    }
}