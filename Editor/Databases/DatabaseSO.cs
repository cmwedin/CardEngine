using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor;
using UnityEngine;
using SadSapphicGames.CardEngine;


namespace SadSapphicGames.CardEngineEditor {
    /// <summary>
    /// An entry in a DatabaseSO, used because Dictionaries aren't serialized by unity
    /// </summary>
    /// <typeparam name="TScriptableObject">The type of scriptable object the database contains</typeparam>
    [System.Serializable] public class DatabaseEntry<TScriptableObject> where TScriptableObject : ScriptableObject {
        /// <summary>
        /// The scriptable object for the entry
        /// </summary>
        public TScriptableObject entrykey;
        /// <summary>
        /// The directory that scriptable object is contained in
        /// </summary>
        public string entryDirectory;
        /// <summary>
        /// Constructs the database entry
        /// </summary>
        public DatabaseEntry(TScriptableObject _entryKey, string _entryDirectory)
        {
            entrykey = _entryKey;
            this.entryDirectory = _entryDirectory;
        }
    }
    /// <summary>
    /// A database for recording various scriptable objects and the directories they are contained in
    /// </summary>
    /// <typeparam name="TScriptableObject">The type of scriptable object the database contains</typeparam>
    public class DatabaseSO<TScriptableObject> : ScriptableObject where TScriptableObject : ScriptableObject  {
        /// <summary>
        /// The list of entries in the database
        /// </summary>
        [SerializeField] private List<DatabaseEntry<TScriptableObject>> database = new List<DatabaseEntry<TScriptableObject>>();
        /// <summary>
        /// Adds and entry to the database
        /// </summary>
        /// <param name="obj">The TScriptableObject to add to the directory</param>
        /// <param name="parentDirectory">The directory containing the object to add</param>
        /// <exception cref="System.ArgumentException">Thrown if the parentDirectory cannot be found</exception>
        public void AddEntry(TScriptableObject obj, string parentDirectory) {
            if(ContainsKey(obj)) {
                Debug.LogWarning($"Database already contains a entry for the object {obj.name}");
            }
            else if(Directory.Exists(parentDirectory)) {
                database.Add(new DatabaseEntry<TScriptableObject>(obj,parentDirectory));
            } else {
                throw new System.ArgumentException($"failed to find directory {parentDirectory} entered for scriptable object {obj.name}");
            }
            EditorUtility.SetDirty(this);
        }
        /// <summary>
        /// Invokes the CleanUp method
        /// </summary>
        private void OnValidate() {
            CleanUp();
        }
        /// <summary>
        /// Remove's and entry from the database
        /// </summary>
        /// <param name="obj">The entry to remove</param>
        public void RemoveEntry(TScriptableObject obj) {
            if(ContainsKey(obj, out var entry)) {
                database.Remove(entry);
            } else {
                Debug.LogWarning($"Attempted to remove scriptable object {obj.name} from a database that does not contain it");
            }
        }
        /// <summary>
        /// Remove's any entries from the database that have been deleted
        /// </summary>
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
        /// <summary>
        /// Checks if a database contains a scriptable object
        /// </summary>
        /// <param name="obj">The object to check the database for</param>
        /// <returns>wether the object was contained in the database</returns>
        public bool ContainsKey(TScriptableObject obj) {
            foreach (var entry in database) {
                if(entry.entrykey == obj) {
                    return true;
                }
            } return false;
        }
        /// <summary>
        /// Checks if a database contains a scriptable object with an out parameter for its entry
        /// </summary>
        /// <param name="obj">The object to check the database for</param>
        /// <param name="outEntry">the entry corresponding to the object</param>
        /// <returns>wether the object was contained in the database</returns>
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
        /// <summary>
        /// Gets a random entry from the database (used in testing)
        /// </summary>
        /// <returns>A random entry from the database</returns>
        public DatabaseEntry<TScriptableObject> GetRandomEntry(){
            int randomIndex = Random.Range(0,database.Count);
            return database[randomIndex];
        }
        /// <summary>
        /// Gets an entry from the database by a reference to its object
        /// </summary>
        /// <param name="obj">the object to retrieve the entry for</param>
        /// <param name="suppressWarning">Wether to print a warning if the entry cannot be found</param>
        /// <returns>the entry for the object</returns>
        public DatabaseEntry<TScriptableObject> GetEntryByKey(TScriptableObject obj, bool suppressWarning = false) {
            if(ContainsKey(obj, out var entry)) {
                return entry;
            } else if(!suppressWarning) Debug.LogWarning($"entry for key {obj.name} not found");
            return null;
        }
        /// <summary>
        /// Gets an entry from the database by the name of the object
        /// </summary>
        /// <param name="objName">the name of the object to retrieve the entry for</param>
        /// <param name="suppressWarning">Wether to print a warning if the entry cannot be found</param>
        /// <returns>the entry for the object</returns>
        public DatabaseEntry<TScriptableObject> GetEntryByName(string objName, bool suppressWarning = false) {
            foreach(var entry in database) {
                if(entry.entrykey.name == objName) {
                    return entry;
                } 
            }
            if(!suppressWarning) {Debug.LogWarning($"no entry with name {objName} found in database {this.name}");}
            return null;
        }
        /// <summary>
        /// Gets the full (relative to the project) path of a scriptable object in the database
        /// </summary>
        /// <param name="obj">The object to get the path for</param>
        /// <returns>the path of the scriptable object</returns>
        public string GetObjectPath(TScriptableObject obj) {
            DatabaseEntry<TScriptableObject> objEntry = GetEntryByKey(obj);
            if(objEntry != null) {
                return $"{objEntry.entryDirectory}/{obj.name}.asset";
            } else {
                return null;
            }
        }
        /// <summary>
        /// Gets the directory of the object in the database 
        /// </summary>
        /// <param name="obj">the object to get the directory for</param>
        /// <returns>the directory of the object </returns>
        public string GetObjectDirectory(TScriptableObject obj) {
            DatabaseEntry<TScriptableObject> objEntry = GetEntryByKey(obj);
            if(objEntry != null) {
                return objEntry.entryDirectory;
            } else {
                return null;
            }
        }
        /// <summary>
        /// Gets a list of the names of each object in the database
        /// </summary>
        /// <returns>a list of the names of each object in the database</returns>
        public List<string> GetAllObjectNames() {
            List<string> output = new List<string>();
            foreach (var entry in database) {
                output.Add(entry.entrykey.name);
            }
            return output;
        }

    }
}
