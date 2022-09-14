using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;
using UnityEngine.Windows;

namespace SadSapphicGames.CardEngineEditor {
    /// <summary>
    /// The object for creating a CartTypeSO
    /// </summary>
    public class CreateCardTypeObject {
        /// <summary>
        /// The current directory set for the types directory
        /// </summary>
        string typesDirectory;
        /// <summary>
        /// The database of the create TypeSO's
        /// </summary>
        TypeDatabaseSO typeDatabase;
        /// <summary>
        /// Communicates to the wrapping window it should be closed
        /// </summary>
        public bool CloseWindow {get; private set;}
        /// <summary>
        /// Constructs the a CreateCardTypeObject
        /// </summary>
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
        /// <summary>
        /// Creates a new CardTypeSO with a given name
        /// </summary>
        /// <param name="typeName">the desired name of the CardType</param>
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
        /// <summary>
        /// Initializes the TypeSO by creating its reference object and asset instance
        /// </summary>
        /// <param name="typeName"></param>
        public void InitializeType(string typeName) {
            typeName = typeName.Replace(" ", string.Empty);

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
    /// <summary>
    /// The window that allows the user to interact with the inner CreateCardTypeObject
    /// </summary>
    public class CreateCardTypeWindow : EditorWindow {
        /// <summary>
        /// The entered name for the TypeSO
        /// </summary>
        [SerializeField] string typeName = ""; 
        /// <summary>
        /// The inner CreateCardTypeObject
        /// </summary>
        private CreateCardTypeObject windowObject;
        /// <summary>
        /// Singleton instance of the window, used to determine when the recompile completes
        /// </summary>
        static CreateCardTypeWindow instance;
        /// <summary>
        /// If the window is waiting for the class of the new type to compile
        /// </summary>
        bool typeCompiling = false;
        /// <summary>
        /// IF the window is waiting for the new type to be initialized
        /// </summary>
        bool typeInitializing = false;

        /// <summary>
        /// creates the inner CreateCardTypeObject
        /// </summary>
        private void OnEnable() {
            windowObject = new CreateCardTypeObject();
        }
        /// <summary>
        /// Opens the window and sets the singleton instance
        /// </summary>
        [MenuItem("Tools/CardEngine/Create/Card Type")] static void Init() {
            if(instance != null) {
                Debug.LogWarning("Create type window already open");
                return;
            }
            instance = EditorWindow.CreateInstance<CreateCardTypeWindow>();
            instance.Show();
        }
        /// <summary>
        /// What the window will display
        /// </summary>
        private void OnGUI() {
            if(windowObject.CloseWindow) this.Close();
            if(!typeCompiling) {
                GUILayout.Label("Create a card type", EditorStyles.boldLabel);
                typeName = EditorGUILayout.TextField("Enter Type name:",typeName);
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
                GUILayout.Label("Please wait while CardType compiles", EditorStyles.boldLabel);
            }
            if(instance == null && !typeInitializing) {
                typeInitializing = true;
                windowObject.InitializeType(typeName);
            }
        }
    }
}