using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using SadSapphicGames.CardEngine;


namespace SadSapphicGames.CardEngineEditor
{

    public class TypeDatabaseSO : DatabaseSO<TypeSO> {
        private static TypeDatabaseSO instance;
        public static TypeDatabaseSO Instance { get {
            if(instance == null) {
                instance = AssetDatabase.LoadAssetAtPath<TypeDatabaseSO>("Assets/CardEngine/Config/TypeDatabase.asset");
            }
            return instance;
        }}
    }
}