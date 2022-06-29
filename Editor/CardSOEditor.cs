using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SadSapphicGames.CardEngine {

[CustomEditor(typeof(CardSO))]
    public class CardSOEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            
        }
    }
}
