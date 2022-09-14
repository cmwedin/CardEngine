using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor {
    /// <summary>
    /// The object used for the logic of creating a cardSO
    /// </summary>
    public class CreateCardObject {
        /// <summary>
        /// The current directory set for CardSO's
        /// </summary>
        string cardsDirectory;
        /// <summary>
        /// The database of CardSO's
        /// </summary>
        CardDatabaseSO cardDatabase;
        /// <summary>
        /// Communicates to the wrapping window it should close
        /// </summary>
        public bool CloseWindow { get; private set;}
        /// <summary>
        /// Constructs the CreateCardObject
        /// </summary>
        public CreateCardObject() {
            var settings = SettingsEditor.ReadSettings(); 
            cardDatabase = CardDatabaseSO.Instance;

            if(cardDatabase == null || settings == null) {
                CloseWindow = true;
                Debug.LogWarning("please finish initializing CardEngine before using the CardEngine/Create menu");
            } else {
                cardsDirectory = settings.Directories.CardScriptableObjects;
                if(!Directory.Exists(cardsDirectory)) {
                    CloseWindow = true;
                    Debug.LogWarning("selected directory invalid, it may have been deleted, please select a valid directory to store card scriptable objects using the CardEngine/Settings menu");
                }
            }
        }
        /// <summary>
        /// Creates a CardSO with a given name and card text
        /// </summary>
        /// <param name="cardName">the card name</param>
        /// <param name="cardText">the card text</param>
        public void CreateCard(string cardName, string cardText) {
            if(cardName == "") {
                        Debug.LogWarning("Card name required");
                        CloseWindow = true;
                        return;
                    }
                    if(Directory.Exists(cardsDirectory + "/" + cardName)) {
                        CloseWindow = true;
                        Debug.LogWarning($"Folder for card {cardName} already exists, please delete it before creating a new card with the same name");
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
                    CloseWindow = true;
        }
    }
    /// <summary>
    /// The window that allows the user to interact with the inner CreateCardObject
    /// </summary>
    public class CreateCardWindow : EditorWindow {
        /// <summary>
        /// The desired card name
        /// </summary>
        string cardName;
        /// <summary>
        /// The desired card text
        /// </summary>
        string cardText;
        /// <summary>
        /// the inner create card object
        /// </summary>
        private CreateCardObject windowObject;
        /// <summary>
        /// singleton instance of the window, for consistency with windows that need to know when recompiling completes
        /// </summary>
        static CreateCardWindow instance;

        /// <summary>
        /// Creates the inner CreateCardObject
        /// </summary>
        private void OnEnable() {
            windowObject = new CreateCardObject();
        }
        /// <summary>
        /// Opens this window
        /// </summary>
        [MenuItem("Tools/CardEngine/Create/Card")] static void Init() {
            if(instance != null) {
                Debug.LogWarning("Create type window already open");
                return;
            }            
            instance = EditorWindow.CreateInstance<CreateCardWindow>();
            instance.Show();
        }
        /// <summary>
        /// What the window will display
        /// </summary>
        private void OnGUI() {
            if(windowObject.CloseWindow) this.Close();
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
