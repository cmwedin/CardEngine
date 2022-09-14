using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace SadSapphicGames.CardEngineEditor {
    /// <summary>
    /// The object that removes an effect from a CompositeEffectSO
    /// </summary>
    public class RemoveEffectObject {
        /// <summary>
        /// The CompositeEffect to remove effects from
        /// </summary>
        private CompositeEffectSO targetEffect;
        /// <summary>
        /// Communicates to the wrapping popup window that it should be closed
        /// </summary>
        public bool CloseWindow {get; private set;}
        /// <summary>
        /// Constructs a RemoveEffectObject targeted on a given CompositeEffectSO
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if the target for the object is null</exception>
        public RemoveEffectObject(CompositeEffectSO _targetEffect) {
            if(_targetEffect == null) {
                throw new ArgumentNullException();
            }
            targetEffect = _targetEffect;
        }
        /// <summary>
        /// Removes the selected effects from the CompositeEffectSO
        /// </summary>
        /// <param name="effectsToRemove">A boolean array of the effects to remove</param>
        /// <exception cref="ArgumentException">Thrown if the length of the argument does not equal the length of the subeffects the CompositeEffectSO has</exception>
        public void RemoveEffects(bool[] effectsToRemove) {
            if(effectsToRemove.Length != targetEffect.Subeffects.Count) {
                throw new ArgumentException("invalid argument length");
            }
            List<EffectSO> effectRefs = new List<EffectSO>();
            for (int i = 0; i < effectsToRemove.Length; i++) {
                if(effectsToRemove[i]) {
                    effectRefs.Add(targetEffect.Subeffects[i]);
                }
            } foreach (var effect in effectRefs) { //? we do this in a separate loop so we dont mess up the indices
                AssetDatabase.RemoveObjectFromAsset(effect);
                targetEffect.RemoveChild(effect);
            }
            EditorUtility.SetDirty(targetEffect);
            AssetDatabase.SaveAssets();
            CloseWindow = true;
        }
    }
    /// <summary>
    /// The popup window through which the user interacts with the RemoveEffectObject
    /// </summary>
    public class RemoveEffectPopup : PopupWindowContent {
        /// <summary>
        /// the RemoveEffectObject the window wraps
        /// </summary>
        private RemoveEffectObject popupObject;
        /// <summary>
        /// The CompositeEffectSO to remove child effects from
        /// </summary>
        private CompositeEffectSO targetEffect;
        /// <summary>
        /// The list of subeffects the target CompositeEffectSO currently has
        /// </summary>
        private ReadOnlyCollection<EffectSO> subeffects;
        /// <summary>
        /// A boolean array indicating what effects to remove
        /// </summary>
        private bool[] effectsToRemove;
        /// <summary>
        /// Creates a RemoveEffectPopup and targets its inner RemoveEffectObject on a given CompositeEffectSO
        /// </summary>
        public RemoveEffectPopup(CompositeEffectSO _targetEffect) {
            targetEffect = _targetEffect;
            popupObject = new RemoveEffectObject(targetEffect);
            subeffects = targetEffect.Subeffects;
            effectsToRemove = new bool[subeffects.Count];
        }
        /// <summary>
        /// What will be displayed by the popup window
        /// </summary>
        /// <param name="rect"></param>
        public override void OnGUI(Rect rect)
        {
            if(popupObject.CloseWindow) editorWindow.Close();
            GUILayout.Label("Select subeffects to remove");
                EditorGUILayout.BeginVertical();
                for (int i = 0; i < subeffects.Count; i++) {
                    effectsToRemove[i] = EditorGUILayout.Toggle(subeffects[i].name,effectsToRemove[i]);
                }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Remove Selected Effects",EditorStyles.miniButtonLeft)) {
                    popupObject.RemoveEffects(effectsToRemove);
                }
                if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                    editorWindow.Close();
                }
        }
    }
}