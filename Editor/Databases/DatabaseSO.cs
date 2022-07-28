using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor;
using UnityEngine;
using SadSapphicGames.CardEngine;


namespace SadSapphicGames.CardEngineEditor {
    [System.Serializable]
    public class DatabaseEntry<TScriptableObject> where TScriptableObject : ScriptableObject {
        public TScriptableObject entrykey;
        public string entryDirectory;

        public DatabaseEntry(TScriptableObject _entryKey, string _entryDirectory)
        {
            entrykey = _entryKey;
            this.entryDirectory = _entryDirectory;
        }
    }
    public class DatabaseSO<TScriptableObject> : ScriptableObject where TScriptableObject : ScriptableObject  {
        // Start is called before the first frame update
        [SerializeField]
        private List<DatabaseEntry<TScriptableObject>> database = new List<DatabaseEntry<TScriptableObject>>();
        public void AddEntry(TScriptableObject obj, string parentDirectory) {
            if(ContainsKey(obj)) {
                Debug.LogWarning($"Database already contains a entry for the object {obj.name}");
            }
            else if(Directory.Exists(parentDirectory)) {
                database.Add(new DatabaseEntry<TScriptableObject>(obj,parentDirectory));
            } else {
                throw new System.Exception($"failed to find directory {parentDirectory} entered for scriptable object {obj.name}");
            }
            EditorUtility.SetDirty(this);
        }
        private void OnValidate() {
            CleanUp();
        }
        public void RemoveEntry(TScriptableObject obj) {
            if(ContainsKey(obj, out var entry)) {
                database.Remove(entry);
            } else {
                Debug.LogWarning($"Attempted to remove scriptable object {obj.name} from a database that does not contain it");
            }
        }
        public void CleanUp() {
            List<DatabaseEntry<TScriptableObject>> entriesToCleanup = new List<DatabaseEntry<TScriptableObject>>();
            foreach (var entry in database) {
                if (entry.entrykey == null) {
                    entriesToCleanup.Add(entry);
                }
            }
            foreach (var entry in entriesToCleanup) {
                if(Directory.Exists(entry.entryDirectory)) {
                    //TODO ask to cleanup directory
                }
                database.Remove(entry);
            }
        }
        public bool ContainsKey(TScriptableObject obj) {
            foreach (var entry in database) {
                if(entry.entrykey == obj) {
                    return true;
                }
            } return false;
        }
        
        private bool ContainsKey(TScriptableObject obj, out DatabaseEntry<TScriptableObject> outEntry) {
            foreach (var entry in database) {
                if(entry.entrykey == obj) {
                    outEntry = entry;
                    return true;
                }
            }
            outEntry = null; 
            return false;
        }
        public DatabaseEntry<TScriptableObject> GetRandomEntry(){
            int randomIndex = Random.Range(0,database.Count);
            return database[randomIndex];
        }
        public DatabaseEntry<TScriptableObject> GetEntryByKey(TScriptableObject obj, bool suppressWarning = false) {
            if(ContainsKey(obj, out var entry)) {
                return entry;
            } else if(!suppressWarning) Debug.LogWarning($"entry for key {obj.name} not found");
            return null;
        }
        public DatabaseEntry<TScriptableObject> GetEntryByName(string objName) {
            foreach(var entry in database) {
                if(entry.entrykey.name == objName) {
                    return entry;
                }
            } 
            Debug.LogWarning($"no entry with name {objName} found in database {this.name}");
            return null;
        }
        public string GetObjectPath(TScriptableObject obj) {
            DatabaseEntry<TScriptableObject> objEntry = GetEntryByKey(obj);
            if(objEntry != null) {
                return $"{objEntry.entryDirectory}/{obj.name}.asset";
            } else {
                return null;
            }
        }
        public string GetObjectDirectory(TScriptableObject obj) {
            DatabaseEntry<TScriptableObject> objEntry = GetEntryByKey(obj);
            if(objEntry != null) {
                return objEntry.entryDirectory;
            } else {
                return null;
            }
        }
        public List<string> GetAllObjectNames() {
            List<string> output = new List<string>();
            foreach (var entry in database) {
                output.Add(entry.entrykey.name);
            }
            return output;
        }

    }
}
