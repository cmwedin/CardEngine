using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SadSapphicGames.CardEngineEditor {
    public class RemoveEffectPopup : PopupWindowContent {
        private CompositeEffectSO targetEffect;
        private ReadOnlyCollection<EffectSO> subeffects;
        private bool[] effectsToRemove;

        public RemoveEffectPopup(CompositeEffectSO _targetEffect) {
            targetEffect = _targetEffect;
            subeffects = targetEffect.Subeffects;
            effectsToRemove = new bool[subeffects.Count];
        }

        public override void OnGUI(Rect rect)
        {
            GUILayout.Label("Select subeffects to remove");
                EditorGUILayout.BeginVertical();
                for (int i = 0; i < subeffects.Count; i++) {
                    effectsToRemove[i] = EditorGUILayout.Toggle(subeffects[i].name,effectsToRemove[i]);
                }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Remove Selected Effects",EditorStyles.miniButtonLeft)) {
                    for (int i = 0; i < effectsToRemove.Length; i++) {
                        if(effectsToRemove[i]) {
                            Debug.Log($"Removing subeffect {subeffects[i].name} from {targetEffect.name}");
                            AssetDatabase.RemoveObjectFromAsset(subeffects[i]);
                            targetEffect.RemoveChild(subeffects[i]);
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