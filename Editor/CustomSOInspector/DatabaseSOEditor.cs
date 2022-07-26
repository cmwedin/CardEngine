using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor
{

    [CustomEditor(typeof(DatabaseSO<>),true)]
    public class DatabaseSOEditor : Editor {
        public override void OnInspectorGUI() {
            //base.OnInspectorGUI();
            EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("database"));
            EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();
            Repaint();
        }
    }
}