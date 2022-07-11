using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace SadSapphicGames.CardEngine
{

    public class TypeDatabaseSO : DatabaseSO<TypeSO> {
        public static TypeDatabaseSO instance; 
        private void OnEnable() {
            instance = AssetDatabase.LoadAssetAtPath<TypeDatabaseSO>("Assets/CardEngine/Config/TypeDatabase.asset");
        }
    }
}