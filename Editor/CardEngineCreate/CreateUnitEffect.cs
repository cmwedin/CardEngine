using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;
using UnityEngine.Windows;

namespace SadSapphicGames.CardEngineEditor {
    /// <summary>
    /// The object used to create a UnitEffectSO
    /// </summary>
    public class CreateUnitEffectObject {
        /// <summary>
        /// The currently set directory for UnitEffectSO's
        /// </summary>
        string effectsDirectory;
        /// <summary>
        /// The database of EffectSO's
        /// </summary>
        EffectDatabaseSO effectDatabase;
        /// <summary>
        /// Communicates to the wrapping window it should be closed
        /// </summary>
        public bool CloseWindow { get; private set;}
        /// <summary>
        /// Constructs a CreateUnitEffectObject
        /// </summary>
        public CreateUnitEffectObject() {
            var settings = SettingsEditor.ReadSettings(); 
            effectDatabase = EffectDatabaseSO.Instance;

            if(effectDatabase == null || settings == null) {
                CloseWindow = true;
                Debug.LogWarning("please finish initializing CardEngine before using the CardEngine/Create menu");
            } else {
                effectsDirectory = settings.Directories.Effects;
                if(!Directory.Exists(effectsDirectory)) {
                    CloseWindow = true;
                    Debug.LogWarning("selected directory invalid, please select a valid directory to store card effects using the CardEngine/Settings menu");
                } 
            }
        }
        /// <summary>
        /// Creates a UnitEffectSO with a given name
        /// </summary>
        /// <param name="effectName">the desired name of the effect</param>
        public void CreateUnitEffect(string effectName) {
            if(effectName == "") {
                CloseWindow = true;
                Debug.LogWarning("No type name entered");
                return;
            }
            effectName = effectName.Replace(" ",string.Empty);
            if(Directory.Exists(effectsDirectory + "/" + effectName)) {
                CloseWindow = true;
                Debug.LogWarning($"Folder for type {effectName} already exists, please delete it before creating a new effect with the same name");
            }
            AssetDatabase.CreateFolder(effectsDirectory, effectName);
            string newEffectDirectory = effectsDirectory + "/" + effectName;
            TemplateIO.CopyTemplate("UnitEffectTemplate.cs",effectName+".cs",newEffectDirectory);
            AssetDatabase.ImportAsset($"{newEffectDirectory}/{effectName}.cs");
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// Initializes the effect created by this object
        /// </summary>
        /// <param name="effectName">the name of the effect created by the object</param>
        public void InitializeEffect(string effectName) {
            effectName = effectName.Replace(" ",string.Empty);
            Type effectType = Type.GetType(effectName + ",Assembly-CSharp");
            UnitEffectSO effectSO = (UnitEffectSO)ScriptableObject.CreateInstance(effectType);
            effectSO.name = effectName;
            AssetDatabase.CreateAsset(effectSO,$"{effectsDirectory}/{effectName}/{effectName}.asset");
            effectDatabase.AddEntry(effectSO,effectsDirectory);
            CloseWindow = true;
        }
    }
    /// <summary>
    /// The window for the user to interact with the inner CreateUnitEffectObject
    /// </summary>
    public class CreateUnitEffectWindow : EditorWindow {
        /// <summary>
        /// The inner CreateUnitEffect object this window wraps
        /// </summary>
        private CreateUnitEffectObject windowObject;
        /// <summary>
        /// The singleton instance of the window used to determine when the UnitEffectSO has finished compiling
        /// </summary>
        static CreateUnitEffectWindow instance;
        /// <summary>
        /// If the effect is currently compiling
        /// </summary>
        bool effectIsCompiling = false;
        /// <summary>
        /// IF the effect is currently initializing 
        /// </summary>
        bool effectIsInitializing;
        /// <summary>
        /// The entered name for the UnitEffectSO to be created
        /// </summary>
        [SerializeField] string effectName = ""; 

        /// <summary>
        /// Creates the 
        /// </summary>
        private void OnEnable() {
            windowObject = new CreateUnitEffectObject();
        }
        /// <summary>
        /// Opens the window and sets it's singleton instance
        /// </summary>
        [MenuItem("CardEngine/Create/Unit Effect")] static void Init() {
            if(instance != null) {
                Debug.LogWarning("Create effect window already open");
                return;
            }
            instance = EditorWindow.CreateInstance<CreateUnitEffectWindow>();
            instance.Show();
        }
        /// <summary>
        /// What will be displayed by the window
        /// </summary>
        private void OnGUI() {
            if(windowObject.CloseWindow) this.Close();
            if (!effectIsCompiling) {
                GUILayout.Label("Create a new unit effect", EditorStyles.boldLabel);
                effectName = EditorGUILayout.TextField("Enter effect name",effectName);
                GUILayout.BeginHorizontal();
                    if(GUILayout.Button("Create effect",EditorStyles.miniButtonLeft)) {
                        windowObject.CreateUnitEffect(effectName);
                        effectIsCompiling = true;
                    }
                    if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                        this.Close();
                    }
                GUILayout.EndHorizontal();
            } else if(effectIsCompiling) {
                GUILayout.Label("Please wait while effect compiles", EditorStyles.boldLabel);
            }
            if(instance == null && !effectIsInitializing) {
                effectIsInitializing = true;
                windowObject.InitializeEffect(effectName);
            }
        }
    }
}