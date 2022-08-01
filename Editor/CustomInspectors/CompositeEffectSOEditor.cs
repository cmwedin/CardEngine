using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;
using System.Collections.ObjectModel;

namespace SadSapphicGames.CardEngineEditor
{

[CustomEditor(typeof(CompositeEffectSO))]
public class CompositeEffectSOEditor : Editor {
    public override void OnInspectorGUI() {
        // base.OnInspectorGUI();
        EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("subEffects"));
        EditorGUI.EndDisabledGroup();
        GUILayout.BeginHorizontal();
            if(GUILayout.Button("Add Subeffect")) {
                Debug.Log("opening subeffect window");
                PopupWindow.Show(new Rect(), new AddEffectPopup((CompositeEffectSO)target));
            }
            if (GUILayout.Button("Remove Subeffect")) {
                Debug.Log("opening subeffect removal window");
                PopupWindow.Show(new Rect(), new RemoveEffectPopup((CompositeEffectSO)target));
            }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
        Repaint();
    }
}
}