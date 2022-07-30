using UnityEditor;
using UnityEngine;
using SadSapphicGames.CardEngine;


namespace SadSapphicGames.CardEngineEditor
{
    [CreateAssetMenu]
    public class ResourceDatabaseSO : DatabaseSO<ResourceSO> {
        private static ResourceDatabaseSO instance;
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