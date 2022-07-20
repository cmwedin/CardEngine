using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    public class CardDatabaseSO : DatabaseSO<CardSO> {
        private static CardDatabaseSO instance;
        public static CardDatabaseSO Instance {get {
            if(instance == null) {
                instance = AssetDatabase.LoadAssetAtPath<CardDatabaseSO>("Assets/CardEngine/Config/CardDatabase.asset");
            }
            return instance;
        }}
    }
}