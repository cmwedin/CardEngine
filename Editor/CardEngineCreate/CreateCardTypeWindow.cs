using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;
using UnityEngine.Windows;

namespace SadSapphicGames.CardEngineEditor {
    public class CardEngineCreateMenu {

    }
    public class CreateCardTypeWindow : EditorWindow {

        [SerializeField]string typeName = ""; 
        [SerializeField]string typesDirectory;
        TypeDatabaseSO typeDatabase;
        bool closeWindow;
        static CreateCardTypeWindow instance;
        bool typeCompiling = false;

        public CreateCardTypeWindow() : base() {

        }
        private void OnEnable() {
            var settings = SettingsEditor.ReadSettings(); 
            typeDatabase = TypeDatabaseSO.Instance;

            if(typeDatabase == null || settings == null) {
                closeWindow = true;
                Debug.LogWarning("please finish initializing CardEngine before using the CardEngine/Create menu");
            } else {
                typesDirectory = settings.Directories.CardTypes;
                if(!Directory.Exists(typesDirectory)) {
                    closeWindow = true;
                    Debug.LogWarning("selected directory invalid, please select a valid directory to store card types using the CardEngine/Settings menu");
                }
            }
            
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
            if(closeWindow) this.Close();
            if(!typeCompiling) {
                GUILayout.Label("Create a card type", EditorStyles.boldLabel);
                typeName = EditorGUILayout.TextField("Enter Type name",typeName);
                GUILayout.BeginHorizontal();
                    if(GUILayout.Button("Create Type",EditorStyles.miniButtonLeft)) {
                        if(typeName == "") {
                            Debug.LogWarning("No type name entered");
                            this.Close();
                        }
                        if(Directory.Exists(typesDirectory + "/" + typeName)) {
                            this.Close();
                            throw new Exception($"Folder for type {typeName} already exists");
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



                        Debug.LogWarning("Remember to initialize your new TypeSO asset");
                        typeCompiling = true;
                    }
                    if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                        this.Close();
                    }
                GUILayout.EndHorizontal();
            } else if (typeCompiling) {
                GUILayout.Label("Please wait while effect compiles", EditorStyles.boldLabel);
            }
            if(instance == null) {
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
                this.Close();
            }
        }
    }
}