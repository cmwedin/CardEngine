using UnityEditor;
using UnityEngine;
using SadSapphicGames.CardEngine;


namespace SadSapphicGames.CardEngineEditor
{
    /// <summary>
    /// The database for ResourceSO's created by the package
    /// </summary>
    public class ResourceDatabaseSO : DatabaseSO<ResourceSO> {
        /// <summary>
        /// The Singleton instance of the database
        /// </summary>
        private static ResourceDatabaseSO instance;
        /// <summary>
        /// Property for retrieving the instance of the database. Opens the InitWindow if essentials haven't been imported
        /// </summary>
        public static ResourceDatabaseSO Instance {get {
            if(!CardEngineInit.EssentialsImported) {
                CardEngineInitWindow.showInitWindow();
                return null;
            }
            if(instance == null) {
                instance = AssetDatabase.LoadAssetAtPath<ResourceDatabaseSO>("Assets/CardEngine/Config/ResourceDatabase.asset");
            }
            return instance;
        }}
    }
}