using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor
{
    [CustomEditor(typeof(UnitEffectSO))]
    public class UnitEffectSOEditor : Editor {
        EffectDatabaseSO effectDatabase;
        string effectDirectory;

        public UnitEffectSOEditor() : base()
        {
            effectDirectory = SettingsEditor.ReadSettings().Directories.Effects;
            effectDatabase = EffectDatabaseSO.instance;
        }

        private void OnEnable() {
        }
        
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if(GUILayout.Button("Save as new effect")) {
                if(effectDatabase.ContainsKey((EffectSO)target)) {
                    Debug.Log($"Effect {target.name} is already contained in effect database");
                } else {
                    // string copyPath = $"{effectDirectory}/{target.name}/{target.name}.asset";
                    // if(AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(target),copyPath)) {
                    //     effectDatabase.AddEntry(AssetDatabase.LoadAssetAtPath<EffectSO>(copyPath),$"{effectDirectory}/{target.name}");
                    // } else {
                    //     //throw new System.Exception("copy failed");
                    // }
                    effectDatabase.AddEntry((EffectSO)target,$"{effectDirectory}/{target.name}");
                
                }
            }
            
        }
    }
}