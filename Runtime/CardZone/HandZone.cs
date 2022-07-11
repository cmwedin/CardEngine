using UnityEngine;
using UnityEngine.UI;

namespace SadSapphicGames.CardEngine {
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class HandZone : CardZone {
        private int maxHandSize; 
        private void Start() {
            CardsDraggable = true;
        }
    }
}