using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SadSapphicGames.CardEngine
{
    public abstract class CardZone : MonoBehaviour
    {
        protected List<Card> cards;
        public ReadOnlyCollection<Card> Cards { get => cards.AsReadOnly();}
        public int CardCount { get => cards.Count;}
        public bool CardsDraggable { get; protected set; }
        private void Awake() {
            cards = new List<Card>();
        }
        public void MoveCard(Card card, CardZone moveToZone) { //? might be better to put this method in Card
            if(cards.Contains(card)) {
                CommandManager.instance.QueueCommand(
                    new MoveCardCommand(card, moveToZone)
                );
            } else {
                Debug.LogWarning($"Zone does not contain card {card.CardName}");
            }
        }

        public virtual void AddCard(Card card) {
            cards.Add(card);
            card.CurrentZone = this;
            card.transform.SetParent(this.transform);
            card.transform.localPosition = Vector3.zero;
        }
        public bool HasCard(Card card) {
            return cards.Contains(card);
        }
        public void RemoveCard(Card card) {
            if(!cards.Contains(card)) {
                Debug.LogWarning($"Zone does not contain card {card.CardName}");
            } else {
                cards.Remove(card);
                card.CurrentZone = null;
                card.transform.SetParent(null);
            }
        }
    }
}