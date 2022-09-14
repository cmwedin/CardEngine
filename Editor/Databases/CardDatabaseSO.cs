using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using SadSapphicGames.CardEngine;


namespace SadSapphicGames.CardEngineEditor
{
    /// <summary>
    /// The database for CardSO's created by the package
    /// </summary>
    public class CardDatabaseSO : DatabaseSO<CardSO> {
        /// <summary>
        /// The Singleton instance of the database
        /// </summary>
        private static CardDatabaseSO instance;
        /// <summary>
        /// Property for retrieving the instance of the database. Opens the InitWindow if essentials haven't been imported
        /// </summary>
        public static CardDatabaseSO Instance {get {
            if(!CardEngineInit.EssentialsImported) {
                CardEngineInitWindow.showInitWindow();
                return null;
            }
            if(instance == null) {
                instance = AssetDatabase.LoadAssetAtPath<CardDatabaseSO>("Assets/CardEngine/Config/CardDatabase.asset");
            }
            return instance;
        }}
    }
}