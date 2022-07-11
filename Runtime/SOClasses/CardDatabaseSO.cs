using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    public class CardDatabaseSO : DatabaseSO<CardSO> {
        public static CardDatabaseSO instance; 
        private void OnEnable() {
            instance = AssetDatabase.LoadAssetAtPath<CardDatabaseSO>("Assets/CardEngine/Config/CardDatabase.asset");
        }
    }
}