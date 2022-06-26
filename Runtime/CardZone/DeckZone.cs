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
            Debug.Log($"loading decklist {deckData.name}");
            foreach (var entry in deckData.deckList) {
                for (int i = 0; i < entry.count; i++) {
                    GameManager.instance.InstantiateCard(this,entry.card);
                }
            }
        }
    }
}
