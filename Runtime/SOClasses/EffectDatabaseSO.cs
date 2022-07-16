using UnityEditor;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    public class EffectDatabaseSO : DatabaseSO<EffectSO>
    {
        
        public static EffectDatabaseSO instance; 
        private void OnEnable() {
            instance = AssetDatabase.LoadAssetAtPath<EffectDatabaseSO>("Assets/CardEngine/Config/EffectDatabase.asset");
        }
    }
}