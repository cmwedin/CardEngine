using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using SadSapphicGames.CardEngine;


namespace SadSapphicGames.CardEngineEditor
{
    /// <summary>
    /// The database for TypeDatabaseSO's created by the package
    /// </summary>
    public class TypeDatabaseSO : DatabaseSO<TypeSO> {
        /// <summary>
        /// The Singleton instance of the database
        /// </summary>
        private static TypeDatabaseSO instance;
        /// <summary>
        /// Property for retrieving the instance of the database. Opens the InitWindow if essentials haven't been imported
        /// </summary>
        public static TypeDatabaseSO Instance { get {
            if(!CardEngineInit.EssentialsImported) {
                CardEngineInitWindow.showInitWindow();
            }
            if(instance == null) {
                instance = AssetDatabase.LoadAssetAtPath<TypeDatabaseSO>("Assets/CardEngine/Config/TypeDatabase.asset");
            }
            return instance;
        }}
    }
}