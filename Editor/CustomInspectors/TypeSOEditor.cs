using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;

namespace SadSapphicGames.CardEngineEditor {
    /// <summary>
    /// Custom editor for TypeSO's
    /// </summary>    
    [CustomEditor(typeof(TypeSO))] public class TypeSOEditor : Editor {
        /// <summary>
        /// Overrides the InspectorGUI of a TypeSO primarily to prevent editing of fields that are handled by editor tools
        /// </summary>
        public override void OnInspectorGUI() {
            EditorGUI.BeginDisabledGroup(true);
            base.OnInspectorGUI();
            EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
            Repaint();
        }
    }
}
