using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SadSapphicGames.CardEngine
{
    public abstract class CardZone : MonoBehaviour {
        protected List<Card> Cards { get; set; }
        public int CardCount { get => Cards.Count;}
        public bool CardsDraggable { get; protected set; }
        private void Awake() {
            Cards = new List<Card>();
        }
        public void MoveCard(Card card, CardZone moveToZone) { //? might be better to put this method in Card
            if(Cards.Contains(card)) {
                CommandManager.instance.QueueCommand(
                    new MoveCardCommand(card, moveToZone)
                );
            } else {
                Debug.LogWarning($"Zone does not contain card {card.CardName}");
            }
        }

        public virtual void AddCard(Card card) {
            Cards.Add(card);
            card.CurrentZone = this;
            card.transform.SetParent(this.transform);
            card.transform.localPosition = Vector3.zero;
        }
        public bool HasCard(Card card) {
            return Cards.Contains(card);
        }
        public void RemoveCard(Card card) {
            if(!Cards.Contains(card)) {
                Debug.LogWarning($"Zone does not contain card {card.CardName}");
            } else {
                Cards.Remove(card);
                card.CurrentZone = null;
                card.transform.SetParent(null);
            }
        }
    }
}