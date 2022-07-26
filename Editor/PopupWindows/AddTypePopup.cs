using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;
using System;

namespace SadSapphicGames.CardEngineEditor {
    public class AddTypeObject {
        CardSO targetCardSO;
        public bool CloseWindow { get; private set;}
        private TypeDatabaseSO typeDatabase;
        public List<string> typeNames;

        public AddTypeObject(CardSO _targetCardSO) {
            targetCardSO = _targetCardSO;
            typeDatabase = TypeDatabaseSO.Instance;
            typeNames = typeDatabase.GetAllObjectNames();
        }
        public void AddTypes(bool[] typesToAdd) {
            if(typesToAdd.Length != typeNames.Count) {
                throw new Exception("invalid argument length");
            }
            for (int i = 0; i < typesToAdd.Length; i++) {
                if(typesToAdd[i]) {
                    Debug.Log($"Adding type {typeNames[i]} to {targetCardSO.name}");
                    targetCardSO.AddType(typeDatabase.GetEntryByName(typeNames[i]).entrykey);
                }
            }
            EditorUtility.SetDirty(targetCardSO);
            CloseWindow = true;
        }
    }
    public class AddTypePopup : PopupWindowContent {
        private AddTypeObject popupObject;
        private List<string> TypeNames { get => popupObject.typeNames;}
        bool[] typesToAdd;
        private CardSO targetCardSO;

        public AddTypePopup(CardSO target) : base() {
            targetCardSO = target;
            popupObject = new AddTypeObject(targetCardSO);
            typesToAdd = new bool[TypeNames.Count];

        }
        public override void OnOpen() {
            // Debug.Log($"{typeNames.Count} potential types to add");
        }

        public override void OnGUI(Rect rect) {
            if(popupObject.CloseWindow) editorWindow.Close();
            GUILayout.Label("Select Types To Add", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
                for (int i = 0; i < TypeNames.Count; i++) {
                    if(targetCardSO.HasType(TypeNames[i])) continue;
                    typesToAdd[i] = EditorGUILayout.Toggle(TypeNames[i],typesToAdd[i]);
                }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Add Selected Types",EditorStyles.miniButtonLeft)) {
                    popupObject.AddTypes(typesToAdd);
                }
                if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                    editorWindow.Close();
                }
            EditorGUILayout.EndHorizontal();
        }
        public override Vector2 GetWindowSize() {
            return new Vector2(200,150);
        }
    }
}
