using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System.Collections.Generic;

namespace SadSapphicGames.CardEngineEditor {
    public class SelectEffectPopup : PopupWindowContent {
        private CompositeEffectSO TargetEffectSO;
        private EffectDatabaseSO effectDatabase;    
        private List<string> effectNames;
        private bool[] effectsToAdd;
        public SelectEffectPopup(CompositeEffectSO target) : base() {
            TargetEffectSO = target;
            effectDatabase = EffectDatabaseSO.instance;
            effectNames = effectDatabase.GetAllObjectNames();
            effectsToAdd = new bool[effectNames.Count];
        }
    

        public override void OnGUI(Rect rect)
        {
            GUILayout.Label("Select subeffect to Add", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
                for (int i = 0; i < effectNames.Count; i++) {
                    effectsToAdd[i] = EditorGUILayout.Toggle(effectNames[i],effectsToAdd[i]);
                }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Add Selected Effects",EditorStyles.miniButtonLeft)) {
                    for (int i = 0; i < effectsToAdd.Length; i++) {
                        if(effectsToAdd[i]) {
                            Debug.Log($"Adding subeffect {effectNames[i]} to {TargetEffectSO.name}");
                            DatabaseEntry<EffectSO> effectEntry = effectDatabase.GetEntryByName(effectNames[i]);
                            EffectSO effectClone = (EffectSO)ScriptableObject.CreateInstance(effectEntry.entrykey.GetType());
                            effectClone.name = $"{TargetEffectSO.name}{effectNames[i]}";
                            AssetDatabase.AddObjectToAsset(effectClone, AssetDatabase.GetAssetPath(TargetEffectSO));
                            TargetEffectSO.AddChild(effectClone);
                            AssetDatabase.SaveAssets();
                        }
                    }
                    editorWindow.Close();
                }
                if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                    editorWindow.Close();
                }
            EditorGUILayout.EndHorizontal();
        }
    }

}