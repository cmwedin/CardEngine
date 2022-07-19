using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;
using System;

namespace SadSapphicGames.CardEngineEditor
{

[CustomEditor(typeof(CompositeEffectSO))]
public class CompositeEffectSOEditor : Editor {
        
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        int controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
        GUILayout.BeginHorizontal();
            if(GUILayout.Button("Add Subeffect")) {
                Debug.Log("opening subeffect window");
                PopupWindow.Show(new Rect(), new SelectEffectPopup((CompositeEffectSO)target));
            }
            if (GUILayout.Button("Remove Subeffect")) {
                Debug.Log("opening subeffect removal window");
                PopupWindow.Show(new Rect(), new RemoveEffectPopup((CompositeEffectSO)target));
            }
        GUILayout.EndHorizontal();
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