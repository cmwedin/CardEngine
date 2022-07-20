using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;

namespace SadSapphicGames.CardEngineEditor {
    public class SelectTypeSOPopup : PopupWindowContent {
        private List<string> typeNames;
        bool[] typesToAdd;
        private TypeDatabaseSO typeDatabase;
        private CardSO targetCardSO;

        public SelectTypeSOPopup(CardSO target) : base() {
            targetCardSO = target;
            typeDatabase = TypeDatabaseSO.Instance;
            typeNames = typeDatabase.GetAllObjectNames();
            typesToAdd = new bool[typeNames.Count];

        }
        public override void OnOpen() {
            // Debug.Log($"{typeNames.Count} potential types to add");
        }

        public override void OnGUI(Rect rect)
        {
            GUILayout.Label("Select Types To Add", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
                for (int i = 0; i < typeNames.Count; i++) {
                    typesToAdd[i] = EditorGUILayout.Toggle(typeNames[i],typesToAdd[i]);
                }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Add Selected Types",EditorStyles.miniButtonLeft)) {
                    for (int i = 0; i < typesToAdd.Length; i++) {
                        if(typesToAdd[i]) {
                            Debug.Log($"Adding type {typeNames[i]} to {targetCardSO.name}");
                            targetCardSO.AddType((DatabaseEntry<TypeSO>)typeDatabase.GetEntryByName(typeNames[i]));
                        }
                    }
                    editorWindow.Close();
                }
                if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                    editorWindow.Close();
                }
            EditorGUILayout.EndHorizontal();
        }
        public override Vector2 GetWindowSize()
        {
            return new Vector2(200,150);
        }
    }
}
