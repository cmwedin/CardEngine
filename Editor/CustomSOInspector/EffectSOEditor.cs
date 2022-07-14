using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;

namespace SadSapphicGames.CardEngineEditor
{

[CustomEditor(typeof(EffectSO))]
public class EffectSOEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        int controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
        if(GUILayout.Button("Add Subeffect")) {
            Debug.Log("opening subeffect window");
            // PopupWindow.Show(new Rect(), new SelectEffectPopup((EffectSO)target));
            EditorGUIUtility.ShowObjectPicker<UnitEffectSO>(null,false,"",controlID);
        }
        if(Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() ==  controlID) {
            AddUnitEffect((UnitEffectSO)EditorGUIUtility.GetObjectPickerObject());
        } else if (Event.current.commandName == "ObjectSelectorClosed" && EditorGUIUtility.GetObjectPickerControlID() ==  controlID) {
            Debug.Log("Object selector closed");
        }
    }
    private void AddUnitEffect(UnitEffectSO unitEffect)  {
            Type subEffectType = unitEffect.GetType();
            Debug.Log($"adding subeffect {unitEffect.name}");
            var subEffectObj = ScriptableObject.CreateInstance(subEffectType);
            AssetDatabase.AddObjectToAsset(subEffectObj,AssetDatabase.GetAssetPath(target));
            AssetDatabase.SaveAssets();
    }
}
}