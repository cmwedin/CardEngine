using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor {

[CustomEditor(typeof(CardSO))]
    public class CardSOEditor : Editor {
        public override void OnInspectorGUI() {
            // base.OnInspectorGUI();
            //? directly editable fields
            // EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardName"));
            var labelStyle = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter, fontSize = 20};
            GUILayout.Label(target.name,labelStyle);
            // EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardText"));
            GUILayout.BeginHorizontal();
                GUILayout.Label("Card Text:",EditorStyles.boldLabel);
                GUILayout.TextArea(serializedObject.FindProperty("_cardText").stringValue);
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardSprite"));

            //? edited  through editor methods
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardEffect"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardTypes"));
            EditorGUI.EndDisabledGroup();
            GUILayout.BeginHorizontal();
                if(GUILayout.Button("Add Type")) {
                    PopupWindow.Show(new Rect(), new SelectTypeSOPopup((CardSO)target));
                }
                if(GUILayout.Button("Remove Type")) {

                }
            GUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            // AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(target),serializedObject.FindProperty("_cardName").stringValue.Replace(" ", string.Empty));
        }
    }
}
