using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor {
    public class CreateCardObject {
        string cardsDirectory;
        CardDatabaseSO cardDatabase;
        public bool closeWindow = false;
        public CreateCardObject() {
            var settings = SettingsEditor.ReadSettings(); 
            cardDatabase = CardDatabaseSO.Instance;

            if(cardDatabase == null || settings == null) {
                closeWindow = true;
                Debug.LogWarning("please finish initializing CardEngine before using the CardEngine/Create menu");
            } else {
                cardsDirectory = settings.Directories.CardScriptableObjects;
                if(!Directory.Exists(cardsDirectory)) {
                    closeWindow = true;
                    Debug.LogWarning("selected directory invalid, please select a valid directory to store card scriptable objects using the CardEngine/Settings menu");
                }
            }
        }
        public void CreateCard(string cardName, string cardText) {
            if(cardName == "") {
                        Debug.LogWarning("Card name required");
                        closeWindow = true;
                        return;
                    }
                    if(Directory.Exists(cardsDirectory + "/" + cardName)) {
                        closeWindow = true;
                        Debug.LogWarning($"Folder for card {cardName} already exists");
                        return;
                    }
                    AssetDatabase.CreateFolder(cardsDirectory, cardName);
                    string cardPath = cardsDirectory + "/" + cardName;

                    CardSO cardSO = ScriptableObject.CreateInstance<CardSO>();
                    cardSO.name = cardName;
                    cardSO.CardText = cardText;


                    CompositeEffectSO cardEffect = ScriptableObject.CreateInstance<CompositeEffectSO>();
                    cardEffect.name = cardName + "Effect";
                    cardSO.CardEffect = cardEffect;

                    AssetDatabase.CreateAsset(cardEffect,$"{cardPath}/{cardEffect.name}.asset");
                    AssetDatabase.CreateAsset(cardSO,$"{cardPath}/{cardSO.name}.asset");
                    AssetDatabase.SaveAssets();
                    cardDatabase.AddEntry(cardSO, cardPath);

                    EditorUtility.SetDirty(cardSO);
                    closeWindow = true;
        }
    }
    public class CreateCardWindow : EditorWindow {
        string cardName;
        string cardText;
        public CreateCardObject windowObject;
        
        public CreateCardWindow() : base() {
        }
        private void OnEnable() {
            
        }
        [MenuItem("CardEngine/Create/Card")]
        static void Init() {
            CreateCardWindow window = EditorWindow.CreateInstance<CreateCardWindow>();
            window.windowObject = new CreateCardObject();
            window.Show();
        }
        private void OnGUI() {
            if(windowObject.closeWindow) this.Close();
            GUILayout.Label("Create a card type", EditorStyles.boldLabel);
            GUILayout.BeginVertical();
                cardName = EditorGUILayout.TextField("Enter card name",cardName);
                GUILayout.Label("Card text:");
                cardText = EditorGUILayout.TextArea(cardText);
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();
                if(GUILayout.Button("Create Card",EditorStyles.miniButtonLeft)) {
                    windowObject.CreateCard(cardName,cardText);
                }
                if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                    this.Close();
                }
            GUILayout.EndHorizontal();
        }
    }
}
