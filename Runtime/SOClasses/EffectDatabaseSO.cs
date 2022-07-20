using UnityEditor;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    public class EffectDatabaseSO : DatabaseSO<EffectSO>
    {
        
        private static EffectDatabaseSO instance;
        public static EffectDatabaseSO Instance { get {
            if(instance == null) {
                instance = AssetDatabase.LoadAssetAtPath<EffectDatabaseSO>("Assets/CardEngine/Config/EffectDatabase.asset");
            }
            return instance;
        }}
    }
}