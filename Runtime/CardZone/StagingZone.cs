using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine {
    /// <summary>
    /// The Zone to place cards into before their effect is resolved
    /// </summary>
    public class StagingZone : CardZone
    {
        /// <summary>
        /// The cards currently staged to have their effects resolve
        /// </summary>
        private Stack<Card> stagedCards = new Stack<Card>();
        /// <summary>
        /// Adds a card to the zone and the stack of staged card
        /// </summary>
        /// <param name="card">the card to add</param>
        public override void AddCard(Card card)
        {
            base.AddCard(card);
            stagedCards.Push(card);
        }

        /// <summary>
        /// Resolves the top staged card
        /// </summary>
        public void ResolveTopCard() {
            if(stagedCards.TryPop(out Card card )){
                CommandManager.instance.QueueCommand(new ResolveCardCommand(card));
            }
        }
    }
}
