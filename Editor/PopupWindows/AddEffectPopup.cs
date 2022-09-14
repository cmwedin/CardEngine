using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;
using System;

namespace SadSapphicGames.CardEngineEditor {
    /// <summary>
    /// The object that adds an effect to a CompositeEffectSO
    /// </summary>
    public class AddEffectObject {
        /// <summary>
        /// Communicates to the wrapping popup the window should be closed
        /// </summary>
        public bool CloseWindow { get; private set;}
        /// <summary>
        /// The list of possible effect names
        /// </summary>
        public List<string> effectNames;
        /// <summary>
        /// The CompositeEffect to add a new child effect to
        /// </summary>
        private CompositeEffectSO targetEffect;
        /// <summary>
        /// The database of UnitEffectSO's
        /// </summary>
        private EffectDatabaseSO effectDatabase;    
        /// <summary>
        /// Constructs an AddEffectObject targeting a given CompositeEffectSO
        /// </summary
        /// <exception cref="ArgumentNullException">Thrown if the targeted CompositeEffectSO is null</exception>
        
        public AddEffectObject (CompositeEffectSO _targetEffect) {
            if(_targetEffect == null) {
                throw new ArgumentNullException();
            }
            targetEffect = _targetEffect;
            effectDatabase = EffectDatabaseSO.Instance;
            effectNames = effectDatabase.GetAllObjectNames();
        }
        /// <summary>
        /// Adds the selected effects to the composite effect
        /// </summary>
        /// <param name="effectsToAdd">a boolean array for what effects to add</param>
        /// <exception cref="ArgumentException">thrown if the length of the argument doesn't </exception>
        public void AddEffects(bool[] effectsToAdd) {
            if(effectsToAdd.Length != effectNames.Count) {
                throw new ArgumentException("invalid argument length");
            }
            for (int i = 0; i < effectsToAdd.Length; i++) {
                if(effectsToAdd[i]) {
                    Debug.Log($"Adding subeffect {effectNames[i]} to {targetEffect.name}");
                    targetEffect.AddChildEffect(effectDatabase.GetEntryByName(effectNames[i]).entrykey);
                    
                }
            }
            EditorUtility.SetDirty(targetEffect);
            CloseWindow = true;
        }
    }
    /// <summary>
    /// The popup window through which the user interacts with the AddEffectObject
    /// </summary>
    public class AddEffectPopup : PopupWindowContent {
        /// <summary>
        /// The AddEffectObject which the window wraps
        /// </summary>
        private AddEffectObject popupObject;
        /// <summary>
        /// The target CompositeEffectSO to add the child effect to
        /// </summary>
        private CompositeEffectSO targetEffect;
        /// <summary>
        /// Gets the effect names from the wrapped AddEffectObject
        /// </summary>
        private List<string> EffectNames {get => popupObject.effectNames;}
        /// <summary>
        /// A boolean list of effects that have been selected to be added
        /// </summary>
        private bool[] effectsToAdd;
        /// <summary>
        /// Construct and AddEffectPopup that will target its wrapped object to a given CompositeEffectSO
        /// </summary>
        public AddEffectPopup(CompositeEffectSO target) : base() {
            targetEffect = target;
            popupObject = new AddEffectObject(targetEffect);
            effectsToAdd = new bool[EffectNames.Count];
        }
        /// <summary>
        /// What will be displayed on the popup window
        /// </summary>
        public override void OnGUI(Rect rect)
        {
            if(popupObject.CloseWindow) editorWindow.Close();
            GUILayout.Label("Select subeffect to Add", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
                for (int i = 0; i < EffectNames.Count; i++) {
                    effectsToAdd[i] = EditorGUILayout.Toggle(EffectNames[i],effectsToAdd[i]);
                }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Add Selected Effects",EditorStyles.miniButtonLeft)) {
                    popupObject.AddEffects(effectsToAdd);
                }
                if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                    editorWindow.Close();
                }
            EditorGUILayout.EndHorizontal();
        }
    }

}