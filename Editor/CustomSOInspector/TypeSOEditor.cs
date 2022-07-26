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
            base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
            Repaint();
        }
    }
}
