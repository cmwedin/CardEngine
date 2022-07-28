using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;
using System;

namespace SadSapphicGames.CardEngineEditor {
    public class AddEffectObject {
        public bool CloseWindow { get; private set;}
        public List<string> effectNames;

        private CompositeEffectSO targetEffect;
        private EffectDatabaseSO effectDatabase;    


        public AddEffectObject (CompositeEffectSO _targetEffect) {
            targetEffect = _targetEffect;
            effectDatabase = EffectDatabaseSO.Instance;
            effectNames = effectDatabase.GetAllObjectNames();
        }

        public void AddEffects(bool[] effectsToAdd) {
            if(effectsToAdd.Length != effectNames.Count) {
                throw new Exception("invalid argument length");
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
    public class AddEffectPopup : PopupWindowContent {
        private AddEffectObject popupObject;
        private CompositeEffectSO targetEffect;
        private List<string> EffectNames {get => popupObject.effectNames;}
        private bool[] effectsToAdd;
        public AddEffectPopup(CompositeEffectSO target) : base() {
            targetEffect = target;
            popupObject = new AddEffectObject(targetEffect);
            effectsToAdd = new bool[EffectNames.Count];
        }
    
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