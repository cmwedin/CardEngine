using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine {
    public class DeckZone : CardZone
    {
        private void Start() {
            CardsDraggable = false;
        }

        public void LoadDecklist(DecklistSO deckData) {
            foreach (var entry in deckData.deckList) {
                // Instantiate
            }
        }
    }
}
