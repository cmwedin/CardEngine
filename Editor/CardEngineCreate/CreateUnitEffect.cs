using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;
using UnityEngine.Windows;

namespace SadSapphicGames.CardEngineEditor {
    public class CreateUnitEffectObject {
        string effectsDirectory;
        EffectDatabaseSO effectDatabase;
        public bool CloseWindow { get; private set;}

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
    public class CreateUnitEffectWindow : EditorWindow {
        
        private CreateUnitEffectObject windowObject;
        static CreateUnitEffectWindow instance = null;
        bool effectIsCompiling = false;
        bool effectIsInitializing;
        [SerializeField] string effectName = ""; 

        public CreateUnitEffectWindow() : base() {
        }
        private void OnEnable() {
            windowObject = new CreateUnitEffectObject();
        }
        [MenuItem("CardEngine/Create/Unit Effect")]
        static void Init() {
            if(instance != null) {
                Debug.LogWarning("Create effect window already open");
            }
            instance = EditorWindow.CreateInstance<CreateUnitEffectWindow>();
            instance.Show();
        }
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