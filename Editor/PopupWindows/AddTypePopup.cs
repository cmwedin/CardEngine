using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;
using System;

namespace SadSapphicGames.CardEngineEditor {
    /// <summary>
    /// The Object that adds a Type to a CardSO
    /// </summary>
    public class AddTypeObject {
        /// <summary>
        /// The CardSO to add the type to
        /// </summary>
        CardSO targetCardSO;
        /// <summary>
        /// Communicates to the popup window that wraps this object it should be closed
        /// </summary>
        public bool CloseWindow { get; private set;}
        /// <summary>
        /// The database of typeSO's that may be added
        /// </summary>
        private TypeDatabaseSO typeDatabase;
        /// <summary>
        /// The list of possible typeSO names
        /// </summary>
        public List<string> typeNames;
        /// <summary>
        /// Constructs a AddTypeObject targeting a given CardSO
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if the targeted CardSO is null</exception>
        public AddTypeObject(CardSO _targetCardSO) {
            if(_targetCardSO == null) {
                throw new ArgumentNullException();
            }
            targetCardSO = _targetCardSO;
            typeDatabase = TypeDatabaseSO.Instance;
            typeNames = typeDatabase.GetAllObjectNames();
        }
        /// <summary>
        /// Adds the selected types to the CardSO
        /// </summary>
        /// <param name="typesToAdd">a boolean array of the selected types</param>
        /// <exception cref="ArgumentException">thrown if the length of the argument does not equal the length of type names</exception>
        public void AddTypes(bool[] typesToAdd) {
            if(typesToAdd.Length != typeNames.Count) {
                throw new ArgumentException("invalid argument length");
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
    /// <summary>
    /// The Popup Window through which the user interacts with the AddTypeObject
    /// </summary>
    public class AddTypePopup : PopupWindowContent {
        /// <summary>
        /// The AddTypeObject that the popup window wraps
        /// </summary>
        private AddTypeObject popupObject;
        /// <summary>
        /// Gets the list of TypeNames from the wrapped AddTypeObject
        /// </summary>
        private List<string> TypeNames { get => popupObject.typeNames;}
        /// <summary>
        /// Boolean array for the types to add
        /// </summary>
        bool[] typesToAdd;
        /// <summary>
        /// The CardSO to add the types too
        /// </summary>
        private CardSO targetCardSO;
        /// <summary>
        /// Constructs a AddTypePopup and targets its wrapped AddTypeObject on a given CardSO
        /// </summary>
        public AddTypePopup(CardSO target) : base() {
            targetCardSO = target;
            popupObject = new AddTypeObject(targetCardSO);
            typesToAdd = new bool[TypeNames.Count];

        }
        /// <summary>
        /// What will be displayed by the popup window
        /// </summary>
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
