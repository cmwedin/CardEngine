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
            subCommands.Add(new MoveCardCommand(card, stagingZone));
            var cardCosts = cardData.GetCardCost();
            foreach (var resource in cardCosts.Keys) {
                subCommands.Add(resource.CreatePayResourceCommand(card.GetController(), cardCosts[resource]));
            } 
            if(cardData.CardEffect is ITargeted) {
                subCommands.Add(new TargetCommand((ITargeted)cardData.CardEffect));
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