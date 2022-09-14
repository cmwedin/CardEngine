using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace SadSapphicGames.CardEngineEditor {
    /// <summary>
    /// The object which removes a TypeSO from a CardSO
    /// </summary>
    public class RemoveTypeObject {
        /// <summary>
        /// The CardSO to remove types from
        /// </summary>
        private CardSO targetCardSO;
        /// <summary>
        /// Indicates to the wrapping popup the window should be closed
        /// </summary>
        public bool CloseWindow { get; private set;}
        /// <summary>
        /// Creates a RemoveTypeObject targeting a given CardSO
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public RemoveTypeObject(CardSO _targetCardSO)
        {
            if(_targetCardSO == null) {
                throw new ArgumentNullException();
            }
            targetCardSO = _targetCardSO;
        }
        /// <summary>
        /// Removes a selection of types from the CardSO
        /// </summary>
        /// <param name="typesToRemove">A boolean array of types to remove from the cardSO</param>
        /// <exception cref="ArgumentException">thrown if the length of the argument does not equal the number of card types the CardSO currently has</exception>
        public void RemoveTypes(bool[] typesToRemove) {
            if(typesToRemove.Length != targetCardSO.CardTypes.Count) {
                throw new ArgumentException("invalid argument length");
            }
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
    /// <summary>
    /// The window through which the user interacts with the RemoveTypeObject
    /// </summary>
    public class RemoveTypePopup : PopupWindowContent {
        /// <summary>
        /// The RemoveTypeObject the window wraps
        /// </summary>
        private RemoveTypeObject popupObject;
        /// <summary>
        /// The CardSO to remove the types from
        /// </summary>
        private CardSO targetCard;
        /// <summary>
        /// The cardTypes the target CardSO currently has
        /// </summary>
        private ReadOnlyCollection<TypeSO> cardTypes;
        /// <summary>
        /// A boolean array of what types to remove
        /// </summary>
        private bool[] typesToRemove;
        /// <summary>
        /// Constructs a RemoveTypePopup and targets its wrapped object on a given CardSO
        /// </summary>
        /// <param name="_targetCard"></param>
        public RemoveTypePopup(CardSO _targetCard) {
            targetCard = _targetCard;
            popupObject = new RemoveTypeObject(targetCard);
            cardTypes = targetCard.CardTypes;
            typesToRemove = new bool[cardTypes.Count];
        }
        /// <summary>
        /// What will be displayed on the popup window
        /// </summary>
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
