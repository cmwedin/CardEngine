using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace SadSapphicGames.CardEngineEditor {
    public class RemoveEffectObject {
        private CompositeEffectSO targetEffect;
        public bool CloseWindow {get; private set;}

        public RemoveEffectObject(CompositeEffectSO _targetEffect) {
            targetEffect = _targetEffect;
        }
        public void RemoveEffects(bool[] effectsToRemove) {
            if(effectsToRemove.Length != targetEffect.Subeffects.Count) {
                throw new Exception("invalid argument length");
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
    public class RemoveEffectPopup : PopupWindowContent {
        private RemoveEffectObject popupObject;
        private CompositeEffectSO targetEffect;
        private ReadOnlyCollection<EffectSO> subeffects;
        private bool[] effectsToRemove;

        public RemoveEffectPopup(CompositeEffectSO _targetEffect) {
            targetEffect = _targetEffect;
            popupObject = new RemoveEffectObject(targetEffect);
            subeffects = targetEffect.Subeffects;
            effectsToRemove = new bool[subeffects.Count];
        }

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