using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using SadSapphicGames.CardEngine;


namespace SadSapphicGames.CardEngineEditor
{
    public class CardDatabaseSO : DatabaseSO<CardSO> {
        private static CardDatabaseSO instance;
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