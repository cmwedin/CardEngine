using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;

namespace SadSapphicGames.CardEngineEditor {
    
    [CustomEditor(typeof(TypeSO))]
    public class TypeSOEditor : Editor {
        public override void OnInspectorGUI() {
            if(serializedObject.FindProperty("initialized").boolValue == false) {
                GUILayout.BeginVertical();
                    EditorGUILayout.LabelField("Object has not been initialized",EditorStyles.boldLabel);
                    if(GUILayout.Button("Initialize object")) {
                        string path = SettingsEditor.ReadSettings().Directories.CardTypes + "/" + target.name;
                        Debug.Log($"initializing TypeSO object at {path}/{target.name}.asset");
                        GameObject referenceObject = new GameObject($"{target.name}ReferenceObject");
                        if(referenceObject.AddComponent(Type.GetType(target.name + ",Assembly-CSharp")) == null) {
                            Debug.LogWarning($"failed to create reference prefab of type {target.name}, file not found");
                        } else {
                            PrefabUtility.SaveAsPrefabAssetAndConnect(referenceObject,$"{path}/{target.name}.prefab",UnityEditor.InteractionMode.AutomatedAction);
                        }
                        GameObject.DestroyImmediate(referenceObject);
                        var prefabAsset = AssetDatabase.LoadAssetAtPath($"{path}/{target.name}.prefab",typeof(CardType));
                        serializedObject.FindProperty("typeReferencePrefab").objectReferenceValue = prefabAsset;

                        TypeDataSO typeDataSO = (TypeDataSO)ScriptableObject.CreateInstance(Type.GetType(target.name + "DataSO,Assembly-CSharp"));
                        typeDataSO.name = target.name + "DataSORef";
                        AssetDatabase.CreateAsset(typeDataSO,$"{path}/{typeDataSO.name}.asset");
                        var dataAsset = AssetDatabase.LoadAssetAtPath($"{path}/{typeDataSO.name}.asset",typeof(TypeDataSO));
                        serializedObject.FindProperty("typeDataReference").objectReferenceValue = dataAsset;
                        AssetDatabase.SaveAssets();


                        serializedObject.FindProperty("initialized").boolValue = true;
                        serializedObject.ApplyModifiedProperties();
                    }
                GUILayout.EndHorizontal();
            } else {
                base.OnInspectorGUI();
            }
        }
    }
}