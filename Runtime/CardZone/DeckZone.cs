using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SadSapphicGames.CardEngine {
    /// <summary>
    /// The CardZone used for cards in a deck
    /// </summary>
    public class DeckZone : CardZone
    {
        /// <summary>
        /// The current order of cards in the deck
        /// </summary>
        private Queue<Card> cardQueue;
        /// <summary>
        /// Sets the card to not being draggable
        /// </summary>
        private void Start() {
            CardsDraggable = false;
        }
        /// <summary>
        /// Loads a decklist into the DeckZone
        /// </summary>
        /// <param name="deckData">The decklist to load</param>
        /// <param name="owner">the owner of the deck</param>
        public void LoadDecklist(DecklistSO deckData,CardActor owner) {
            Debug.Log($"loading decklist {deckData.name}");
            foreach (var entry in deckData.deckList) {
                for (int i = 0; i < entry.count; i++) {
                    CardEngineManager.instance.InstantiateCard(this,entry.card,owner);
                }
            }
            Shuffle();
        }
        /// <summary>
        /// randomizes the order of the cards in the DeckZone's queue
        /// </summary>
        public void Shuffle() {
            int deckSize = cards.Count();
            cardQueue = new Queue<Card>();
            // List<int> order = Enumerable.Range(0,deckSize);
            for (int i = deckSize -1; i >= 0; i--) {
                int j = Random.Range(0,i);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            } for (int i = 0; i < deckSize; i++) { //? there is perhaps a way to combine these into one loop
                cardQueue.Enqueue(cards[i]);
                cards[i].transform.SetSiblingIndex(deckSize - 1 - i);    
            }
        }
        /// <summary>
        /// Draws a card from the deck, outdated by the MoveCardCommand
        /// </summary>
        /// <param name="playerHand">The hand to move the card to</param>
        public void DrawCard(HandZone playerHand) {
            Card nextCard = cardQueue.Dequeue();
            nextCard.IsVisible = true;
            this.MoveCard(nextCard, playerHand);

        }
        /// <summary>
        /// Override to shuffle the deck after adding a card to it
        /// </summary>
        /// <param name="card">the card to add to the deck</param>
        public override void AddCard(Card card)
        {
            base.AddCard(card);
            card.IsVisible = false;
            Shuffle();
        }
    }
}
