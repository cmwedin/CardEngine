using UnityEngine;
using UnityEditor;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor {
    public class SelectEffectPopup : PopupWindowContent {
        private EffectSO effectSO;    
        
        public SelectEffectPopup(EffectSO target) : base() {
            effectSO = target;
        }

        public override void OnGUI(Rect rect)
        {
            GUILayout.Label("Hello World");
        }
    }

}