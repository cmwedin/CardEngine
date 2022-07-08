using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor {
    public class CreateCardWindow : EditorWindow {
        string cardName;
        string cardText;
        string cardsDirectory;
        CardDatabaseSO cardDatabase;
        public CreateCardWindow() : base() {
            var settings = SettingsEditor.ReadSettings(); 
            cardsDirectory = settings.Directories.CardScriptableObjects;
        }
        private void OnEnable() {
            cardDatabase = AssetDatabase.LoadAssetAtPath<CardDatabaseSO>("Assets/CardEngine/Config/CardDatabase.asset");
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
                    cardDatabase.AddEntry(cardSO, cardPath);
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
