using UnityEngine;
using SadSapphicGames.CommandPattern;
using System.Linq;

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// The Command for a CardActor to play a Card from their hand
    /// </summary>
    public class PlayCardCommand : CompositeCommand, IFailable {
        /// <summary>
        /// The Card to play
        /// </summary>
        private Card card;
        /// <summary>
        /// The played Card's data
        /// </summary>
        private CardSO cardData;
        /// <summary>
        /// Constructs a Command to play a given Card to a given CardZone
        /// </summary>
        /// <param name="card">The Card to play</param>
        /// <param name="stagingZone">The zone to play the Card to</param>
        public PlayCardCommand(Card card, CardZone stagingZone) {
            this.card = card;
            cardData = card.GetData();
            cardData.CardEffect.Controller = card.GetController();
            subCommands.Add(new MoveCardCommand(card, stagingZone));
            var cardCosts = cardData.GetCardCost();
            foreach (var resource in cardCosts.Keys) {
                subCommands.Add(resource.CreatePayResourceCommand(card.GetController(), cardCosts[resource]));
            }
            foreach (var effect in cardData.CardEffect.Subeffects) {
                if (effect is ITargeted)
                {
                    subCommands.Add(new TargetCommand((ITargeted)effect));
                }
            }
        }
        /// <summary>
        /// True if the PayResourceCommand's of the Card would fail
        /// </summary>
        public bool WouldFail() {
            var results =
                from com in subCommands
                where com is IFailable
                select ((IFailable)com).WouldFail();
            return results.Contains(true);
        }

    }
}