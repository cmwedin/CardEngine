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
            if(stagedCards.TryPop(out Card card )){
                CommandManager.instance.QueueCommand(new ResolveCardCommand(card));
            }
        }

        void Start() {
            
        }


        void Update() {
            
        }
    }
}
