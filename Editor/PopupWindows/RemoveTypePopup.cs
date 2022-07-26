using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SadSapphicGames.CardEngineEditor {
    public class RemoveTypeObject {
        private CardSO targetCardSO;
        public bool CloseWindow { get; private set;}

        public RemoveTypeObject(CardSO _targetCardSO)
        {
            targetCardSO = _targetCardSO;
        }

        public void RemoveTypes(bool[] typesToRemove) {
            List<TypeSO> typeRefs = new List<TypeSO>();
            for (int i = 0; i < typesToRemove.Length; i++) {
                if(typesToRemove[i]) {
                    typeRefs.Add(targetCardSO.CardTypes[i]);
                }
            } foreach (var type in typeRefs) { //? we do this in a separate loop so we don't mess up the indices
                targetCardSO.RemoveType(type);
            }
            EditorUtility.SetDirty(targetCardSO);
            AssetDatabase.SaveAssets();
            CloseWindow = true;

        }
    }
    public class RemoveTypePopup : PopupWindowContent {
        private RemoveTypeObject popupObject;
        private CardSO targetCard;
        private ReadOnlyCollection<TypeSO> cardTypes;
        private bool[] typesToRemove;

        public RemoveTypePopup(CardSO _targetCard) {
            targetCard = _targetCard;
            popupObject = new RemoveTypeObject(targetCard);
            cardTypes = targetCard.CardTypes;
            typesToRemove = new bool[cardTypes.Count];
        }

        public override void OnGUI(Rect rect)
        {
            if(popupObject.CloseWindow) editorWindow.Close();
            GUILayout.Label("Select types to remove");
                EditorGUILayout.BeginVertical();
                for (int i = 0; i < cardTypes.Count; i++) {
                    typesToRemove[i] = EditorGUILayout.Toggle(cardTypes[i].name,typesToRemove[i]);
                }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Remove Selected types",EditorStyles.miniButtonLeft)) {
                    popupObject.RemoveTypes(typesToRemove);
                }
                if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                    editorWindow.Close();
                }
        }
    }
}
