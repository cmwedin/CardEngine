using UnityEngine;
using SadSapphicGames.CommandPattern;
using System.Linq;

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
            var results =
                from com in subCommands
                where com is IFailable
                select ((IFailable)com).WouldFail();
            return results.Contains(true);
        }

    }
}