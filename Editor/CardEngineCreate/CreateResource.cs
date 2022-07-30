using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;
using UnityEngine.Windows;

namespace SadSapphicGames.CardEngineEditor
{
    public class CreateResourceObject {
        public bool CloseWindow { get; private set; }

        internal void CreateResource(string resourceName)
        {
            throw new NotImplementedException();
        }

        internal void InitializeResource()
        {
            throw new NotImplementedException();
        }
    }
    public class CreateResourceWindow : EditorWindow {
        private CreateResourceObject windowObject;
        static CreateResourceWindow instance;
        bool resourceCompiling;
        bool resourceInitializing;
        string resourceName;
        
        [MenuItem("CardEngine/Create/Resource")]
        static void Init() {
            if(instance != null) {
                Debug.LogWarning("Create resource window already open");
            }
            instance = EditorWindow.CreateInstance<CreateResourceWindow>();
            instance.Show();
        }

        private void OnEnable() {
            windowObject = new CreateResourceObject();
        }

        private void OnGUI() {
            if(windowObject.CloseWindow) {this.Close();}
            if(!resourceCompiling) {
                GUILayout.Label("Create New Resource");
                resourceName = EditorGUILayout.TextField("Enter Resource name:",resourceName); 
                GUILayout.BeginHorizontal();
                    if(GUILayout.Button("Create Type",EditorStyles.miniButtonLeft)) {
                        windowObject.CreateResource(resourceName);
                        resourceCompiling = true;
                    }
                    if(GUILayout.Button("Cancel",EditorStyles.miniButtonRight)) {
                        this.Close();
                    }
                GUILayout.EndHorizontal();
            } else if(resourceCompiling) {
                GUILayout.Label("please wait while resource compiles");
            }
            if(instance == null && !resourceInitializing) {
                resourceInitializing = true;
                windowObject.InitializeResource();
            }
        }
    }
}