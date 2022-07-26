using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SadSapphicGames.CardEngineEditor {
    public class RemoveTypePopup : PopupWindowContent {
        private CardSO targetCard;
        private ReadOnlyCollection<TypeSO> cardTypes;
        private bool[] typesToRemove;

        public RemoveTypePopup(CardSO _targetCard) {
            targetCard = _targetCard;
            cardTypes = targetCard.CardTypes;
            typesToRemove = new bool[cardTypes.Count];
        }

        public override void OnGUI(Rect rect)
        {
            GUILayout.Label("Select types to remove");
                EditorGUILayout.BeginVertical();
                for (int i = 0; i < cardTypes.Count; i++) {
                    typesToRemove[i] = EditorGUILayout.Toggle(cardTypes[i].name,typesToRemove[i]);
                }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Remove Selected types",EditorStyles.miniButtonLeft)) {
                    for (int i = 0; i < typesToRemove.Length; i++) {
                        if(typesToRemove[i]) {
                            Debug.Log($"Removing type {cardTypes[i].name} from {targetCard.name}");
                            targetCard.RemoveType(cardTypes[i]);
                            AssetDatabase.SaveAssets();
                        }
                    }
                    editorWindow.Close();
                }
                if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                    editorWindow.Close();
                }
        }
    }
}
