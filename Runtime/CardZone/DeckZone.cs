using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SadSapphicGames.CardEngine {
    public class DeckZone : CardZone
    {
        private Queue<Card> cardQueue;
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
            Shuffle();
        }
        public void Shuffle() {
            int deckSize = Cards.Count();
            cardQueue = new Queue<Card>();
            // List<int> order = Enumerable.Range(0,deckSize);
            for (int i = deckSize -1; i >= 0; i--) {
                int j = Random.Range(0,i);
                Card temp = Cards[i];
                Cards[i] = Cards[j];
                Cards[j] = temp;
            } for (int i = 0; i < deckSize; i++) { //? there is perhaps a way to combine these into one loop
                cardQueue.Enqueue(Cards[i]);
                Cards[i].transform.SetSiblingIndex(deckSize - 1 - i);    
            }
        }
    }
}
