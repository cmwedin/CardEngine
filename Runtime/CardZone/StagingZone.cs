using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine {
    public class StagingZone : CardZone
    {
        private Stack<Card> stagedCards = new Stack<Card>();
        public override void AddCard(Card card)
        {
            base.AddCard(card);
            stagedCards.Push(card);
        }

        //TODO
        public void ResolveTopCard() {
            throw new System.NotImplementedException();
        }

        void Start() {
            
        }


        void Update() {
            
        }
    }
}
