using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;
using UnityEngine.Windows;

namespace SadSapphicGames.CardEngineEditor {
    public class CreateCardTypeObject {
        string typesDirectory;
        TypeDatabaseSO typeDatabase;
        public bool CloseWindow {get; private set;}

        public CreateCardTypeObject() {
            var settings = SettingsEditor.ReadSettings(); 
            typeDatabase = TypeDatabaseSO.Instance;

            if(typeDatabase == null || settings == null) {
                CloseWindow = true;
                Debug.LogWarning("please finish initializing CardEngine before using the CardEngine/Create menu");
            } else {
                typesDirectory = settings.Directories.CardTypes;
                if(!Directory.Exists(typesDirectory)) {
                    CloseWindow = true;
                    Debug.LogWarning("selected directory invalid, please select a valid directory to store card types using the CardEngine/Settings menu");
                }
            }
        }
        public void CreateCardType(string typeName) {
            if(typeName == "") {
                    CloseWindow = true;
                    Debug.LogWarning("No type name entered");
                    return;
            }
            typeName = typeName.Replace(" ", string.Empty);
            if(Directory.Exists(typesDirectory + "/" + typeName)) {
                    CloseWindow = true;
                    Debug.LogWarning($"Folder for type {typeName} already exists, please delete it before creating a new type with that name");
                    return;
            }

            AssetDatabase.CreateFolder(typesDirectory, typeName);
            string typePath = typesDirectory + "/" + typeName;
            TemplateIO.CopyTemplate("CardTypeTemplate.cs",typeName+".cs",typePath);
            AssetDatabase.ImportAsset($"{typePath}/{typeName}.cs");
            TemplateIO.CopyTemplate("TypeDataSOTemplate.cs",typeName+"DataSO.cs",typePath);
            AssetDatabase.ImportAsset($"{typePath}/{typeName}DataSO.cs");
            AssetDatabase.Refresh();

            TypeSO typeSO = ScriptableObject.CreateInstance<TypeSO>();
            typeSO.name = typeName;
            AssetDatabase.CreateAsset(typeSO,$"{typePath}/{typeName}.asset");
            typeDatabase.AddEntry(typeSO, typePath);
            AssetDatabase.SaveAssets();
        }
        public void InitializeType(string typeName) {
            typeName = typeName.Replace(" ", string.Empty);

            GUILayout.Label("Done compiling", EditorStyles.boldLabel);
            TypeSO typeSO = AssetDatabase.LoadAssetAtPath<TypeSO>($"{typesDirectory}/{typeName}/{typeName}.asset");
            GameObject referenceObject = new GameObject($"{typeName}ReferenceObject");
            if(referenceObject.AddComponent(Type.GetType(typeName + ",Assembly-CSharp")) == null) {
                Debug.LogWarning($"failed to create reference prefab of type {typeName}, file not found");
            } else {
                PrefabUtility.SaveAsPrefabAssetAndConnect(referenceObject,$"{typesDirectory}/{typeName}/{typeName}.prefab",UnityEditor.InteractionMode.AutomatedAction);
            }
            GameObject.DestroyImmediate(referenceObject);
            CardType prefabAsset = AssetDatabase.LoadAssetAtPath<CardType>($"{typesDirectory}/{typeName}/{typeName}.prefab");
            typeSO.SetComponentReference(prefabAsset);

            TypeDataSO typeDataSO = (TypeDataSO)ScriptableObject.CreateInstance(Type.GetType(typeName + "DataSO,Assembly-CSharp"));
            typeDataSO.name = typeName + "DataSORef";
            AssetDatabase.CreateAsset(typeDataSO,$"{typesDirectory}/{typeName}/{typeDataSO.name}.asset");
            typeSO.SetDataReference(typeDataSO);

            EditorUtility.SetDirty(typeSO);
            AssetDatabase.SaveAssets();
            CloseWindow = true;
        }
    }
    public class CreateCardTypeWindow : EditorWindow {

        [SerializeField] string typeName = ""; 
        private CreateCardTypeObject windowObject;
        static CreateCardTypeWindow instance;
        bool typeCompiling = false;
        bool typeInitializing = false;

        public CreateCardTypeWindow() : base() {
        }
        private void OnEnable() {
            windowObject = new CreateCardTypeObject();
        }
        [MenuItem("CardEngine/Create/Card Type")]
        static void Init() {
            if(instance != null) {
                Debug.LogWarning("Create type window already open");
            }
            instance = EditorWindow.CreateInstance<CreateCardTypeWindow>();
            instance.Show();
        }
        private void OnGUI() {
            if(windowObject.CloseWindow) this.Close();
            if(!typeCompiling) {
                GUILayout.Label("Create a card type", EditorStyles.boldLabel);
                typeName = EditorGUILayout.TextField("Enter Type name",typeName);
                GUILayout.BeginHorizontal();
                    if(GUILayout.Button("Create Type",EditorStyles.miniButtonLeft)) {
                        windowObject.CreateCardType(typeName);
                        typeCompiling = true;
                    }
                    if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                        this.Close();
                    }
                GUILayout.EndHorizontal();
            } else if (typeCompiling) { //? the if is redundant but included for clarity
                GUILayout.Label("Please wait while effect compiles", EditorStyles.boldLabel);
            }
            if(instance == null && typeInitializing == false) {
                typeInitializing = true;
                windowObject.InitializeType(typeName);
            }
        }
    }
}