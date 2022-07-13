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
                Cards.Remove(card);
                moveToZone.AddCard(card); //? this method will take care of card.CurrentZone as well
            } else {
                Debug.LogWarning($"Card {card.CardName} not found in cardzone {this.name}");
            }
            return;
        }

        public void AddCard(Card card) {
            Cards.Add(card);
            card.CurrentZone = this;
            card.transform.SetParent(this.transform);
        }
    }
}