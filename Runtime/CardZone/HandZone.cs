using UnityEngine;
using UnityEngine.UI;

namespace SadSapphicGames.CardEngine {
    /// <summary>
    /// The zone for cards in a players hand, currently uses a layout group
    /// </summary>
    [RequireComponent(typeof(HorizontalLayoutGroup))] public class HandZone : CardZone {
        /// <summary>
        /// The maximum number of cards that can be in the hand, not implemented
        /// </summary>
        private int maxHandSize; 
        /// <summary>
        /// Sets the cards to be draggable
        /// </summary>
        private void Start() {
            CardsDraggable = true;
        }
    }
}