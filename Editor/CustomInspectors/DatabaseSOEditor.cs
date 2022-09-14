using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor
{
    /// <summary>
    /// Custom editor for DatabaseSO's
    /// </summary>
    [CustomEditor(typeof(DatabaseSO<>),true)] public class DatabaseSOEditor : Editor {
        /// <summary>
        /// Overrides the InspectorGUI of a DatabaseSO primarily to prevent editing of fields that are handled by editor tools
        /// </summary>
        public override void OnInspectorGUI() {
            //base.OnInspectorGUI();
            EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("database"));
            EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
            Repaint();
        }
    }
}