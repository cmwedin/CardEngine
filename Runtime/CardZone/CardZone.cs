using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// The base class of all CardZones
    /// </summary>
    public abstract class CardZone : MonoBehaviour
    {
        /// <summary>
        /// The cards currently in the zone
        /// </summary>
        protected List<Card> cards;
        /// <summary>
        /// public read only version of the cards in the zone
        /// </summary>
        public ReadOnlyCollection<Card> Cards { get => cards.AsReadOnly();}
        /// <summary>
        /// The number of cards in the zone
        /// </summary>
        public int CardCount { get => cards.Count;}
        /// <summary>
        /// Wether the cards can be dragged from the zone
        /// </summary>
        public bool CardsDraggable { get; protected set; }
        /// <summary>
        /// Initializes list of cards
        /// </summary>
        private void Awake() {
            cards = new List<Card>();
        }
        /// <summary>
        /// Moves a card from the zone, less used now in lieu of MoveCardCommand
        /// </summary>
        /// <param name="card">the card to move</param>
        /// <param name="moveToZone">the zone to move it to</param>
        public void MoveCard(Card card, CardZone moveToZone) { //? might be better to put this method in Card
            if(cards.Contains(card)) {
                CommandManager.instance.QueueCommand(
                    new MoveCardCommand(card, moveToZone)
                );
            } else {
                Debug.LogWarning($"Zone does not contain card {card.CardName}");
            }
        }
        /// <summary>
        /// Adds a card to the zone
        /// </summary>
        /// <param name="card">the card to add</param>
        public virtual void AddCard(Card card) {
            cards.Add(card);
            card.CurrentZone = this;
            card.transform.SetParent(this.transform);
            card.transform.localPosition = Vector3.zero;
            OnCardAdded?.Invoke();
        }
        /// <summary>
        /// Invoked when cards are added to the zone
        /// </summary>
        public event Action OnCardAdded;
        /// <summary>
        /// Checks if a card is in the CardZone
        /// </summary>
        /// <param name="card">the card to look for</param>
        /// <returns>if the card is in the zone</returns>
        public bool HasCard(Card card) {
            return cards.Contains(card);
        }
        /// <summary>
        /// Removes a card from the zone 
        /// </summary>
        /// <param name="card">the card to remove from the zone</param>
        public void RemoveCard(Card card) {
            if(!cards.Contains(card)) {
                Debug.LogWarning($"Zone does not contain card {card.CardName}");
            } else {
                cards.Remove(card);
                card.CurrentZone = null;
                card.transform.SetParent(null);
                OnCardRemoved?.Invoke();
            }
        }
        /// <summary>
        /// Invoked when cards are removed from the zone
        /// </summary>
        public event Action OnCardRemoved;
    }
}