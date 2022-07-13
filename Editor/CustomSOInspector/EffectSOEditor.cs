using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor
{

[CustomEditor(typeof(EffectSO))]
public class EffectSOEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(GUILayout.Button("Add Subeffect")) {
            Debug.Log("opening subeffect window");
            PopupWindow.Show(new Rect(), new SelectEffectPopup((EffectSO)target));
        }
        
    }
}
}