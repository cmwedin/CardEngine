using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
// public struct DatabaseEntry<TScriptableObject> where TScriptableObject : ScriptableObject {
//     public TScriptableObject Object;
//     public string path;
// }
namespace SadSapphicGames.CardEngineEditor {
    public class DatabaseSO<TScriptableObject> : ScriptableObject where TScriptableObject : ScriptableObject  {
        // Start is called before the first frame update
        private Dictionary<TScriptableObject,string> database = new Dictionary<TScriptableObject, string>();
        public void AddEntry(TScriptableObject obj, string parentDirectory) {
            if(Directory.Exists(parentDirectory)) {
                database.Add(obj,parentDirectory);
            } else {
                throw new System.Exception($"failed to find directory {parentDirectory} entered for scriptable object {obj.name}");
            }
        }
        public string GetObjectPath(TScriptableObject obj) {
            if(database.ContainsKey(obj)) {
                return $"{database[obj]}/{obj.name}.asset";
            } else {
                Debug.LogWarning($"Failed to find object {obj.name} in database");
                return null;
            }
        }
        public string GetObjectDirectory(TScriptableObject obj) {
            if(database.ContainsKey(obj)) {
            return database[obj];
            } else {
                Debug.LogWarning($"Failed to find object {obj.name} in database");
                return null;
            }
        }

    }
}
