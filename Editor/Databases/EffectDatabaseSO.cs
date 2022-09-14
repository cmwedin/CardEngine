using UnityEditor;
using UnityEngine;
using SadSapphicGames.CardEngine;


namespace SadSapphicGames.CardEngineEditor
{
    /// <summary>
    /// The database for UnitEffectSO's created by the package, may support saving CompositeEffects in the future
    /// </summary>
    public class EffectDatabaseSO : DatabaseSO<EffectSO> {  
        /// <summary>
        /// The Singleton instance of the database
        /// </summary>        
        private static EffectDatabaseSO instance;
        /// <summary>
        /// Property for retrieving the instance of the database. Opens the InitWindow if essentials haven't been imported
        /// </summary>        
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