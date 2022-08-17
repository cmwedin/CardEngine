using UnityEngine;
using SadSapphicGames.CommandPattern;
using System.Linq;

namespace SadSapphicGames.CardEngine
{
    public class PlayCardCommand : CompositeCommand, IFailable {
        private Card card;
        private CardSO cardData;

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
        public bool WouldFail() {
            var results =
                from com in subCommands
                where com is IFailable
                select ((IFailable)com).WouldFail();
            return results.Contains(true);
        }

    }
}