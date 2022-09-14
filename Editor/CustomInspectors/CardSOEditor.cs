using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor {

    /// <summary>
    /// Custom editor for CardSO's
    /// </summary>
    [CustomEditor(typeof(CardSO))] public class CardSOEditor : Editor {
        /// <summary>
        /// Overrides the InspectorGUI of a CardSO primarily to prevent editing of fields that are handled by editor tools
        /// </summary>
        public override void OnInspectorGUI() {
            // base.OnInspectorGUI();
            //? directly editable fields
            // EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardName"));
            var labelStyle = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter, fontSize = 20};
            GUILayout.Label(target.name,labelStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("cardCosts"));
            // EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardText"));
            GUILayout.BeginHorizontal();
                GUILayout.Label("Card Text:",EditorStyles.boldLabel);
                serializedObject.FindProperty("_cardText").stringValue = GUILayout.TextArea(serializedObject.FindProperty("_cardText").stringValue);
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardSprite"));

            //? edited  through editor methods
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardEffect"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardTypes"));
            EditorGUI.EndDisabledGroup();
            GUILayout.BeginHorizontal();
                if(GUILayout.Button("Add Type")) {
                    PopupWindow.Show(new Rect(), new AddTypePopup((CardSO)target));
                }
                if(GUILayout.Button("Remove Type")) {
                    PopupWindow.Show(new Rect(), new RemoveTypePopup((CardSO)target));
                }
            GUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
            Repaint();
            // AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(target),serializedObject.FindProperty("_cardName").stringValue.Replace(" ", string.Empty));
        }
    }
}
