using UnityEditor;
using UnityEngine;
using SadSapphicGames.CardEngine;


namespace SadSapphicGames.CardEngineEditor
{
    public class EffectDatabaseSO : DatabaseSO<EffectSO>
    {
        
        private static EffectDatabaseSO instance;
        public static EffectDatabaseSO Instance { get {
            if(!CardEngineInit.EssentialsImported) {
                CardEngineInitWindow.showInitWindow();
            }
            if(instance == null) {
                instance = AssetDatabase.LoadAssetAtPath<EffectDatabaseSO>("Assets/CardEngine/Config/EffectDatabase.asset");
            }
            return instance;
        }}
    }
}