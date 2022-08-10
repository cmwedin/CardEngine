using UnityEngine;
using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine
{
    public class PlayCardCommand : CompositeCommand {
        private Card card;

        public PlayCardCommand(Card card, CardZone stagingZone) {
            this.card = card;
            subCommands.Add(new MoveCardCommand(card, stagingZone));
            var cardCosts = card.GetData().GetCardCost();
            foreach (var resource in cardCosts.Keys) {
                subCommands.Add(resource.CreatePayResourceCommand(card.GetController(), cardCosts[resource]));
            }
        }
        public override bool WouldFail() {
            return ((IFailable)subCommands[1]).WouldFail();
        }

    }
}