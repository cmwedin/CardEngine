using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor {

[CustomEditor(typeof(CardSO))]
    public class CardSOEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            Rect buttonRect = new Rect();
            if(GUILayout.Button("Add Type")) {
                PopupWindow.Show(buttonRect, new SelectTypeSOPopup((CardSO)target));
            }
        }
    }
}
