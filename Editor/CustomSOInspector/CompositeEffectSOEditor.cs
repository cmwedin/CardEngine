using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;

namespace SadSapphicGames.CardEngineEditor
{

[CustomEditor(typeof(CompositeEffectSO))]
public class CompositeEffectSOEditor : Editor {
    bool selectionMade = true;
    
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        int controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
        if(GUILayout.Button("Add Subeffect")) {
            selectionMade = false;
            Debug.Log("opening subeffect window");
            // PopupWindow.Show(new Rect(), new SelectEffectPopup((EffectSO)target));
            EditorGUIUtility.ShowObjectPicker<UnitEffectSO>(null,false,"",controlID);
        }
        if(Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() ==  controlID && selectionMade == false) {
            selectionMade = true;
            AddUnitEffect((UnitEffectSO)EditorGUIUtility.GetObjectPickerObject());
        } else if (Event.current.commandName == "ObjectSelectorClosed" && EditorGUIUtility.GetObjectPickerControlID() ==  controlID) {
            Debug.Log("Object selector closed");
        }
    }
    private void AddUnitEffect(UnitEffectSO unitEffect)  {
            Type subEffectType = unitEffect.GetType();
            CompositeEffectSO targetObj = (CompositeEffectSO)target;
            Debug.Log($"adding subeffect {unitEffect.name}");
            EffectSO subEffectObj = (EffectSO)ScriptableObject.CreateInstance(subEffectType);
            subEffectObj.name = $"{targetObj.name}Subeffect{targetObj.ChildrenCount + 1}";
            AssetDatabase.AddObjectToAsset(subEffectObj,AssetDatabase.GetAssetPath(targetObj));
            targetObj.AddChild(subEffectObj);
            AssetDatabase.SaveAssets();

    }
}
}