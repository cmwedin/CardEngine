using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    public class PlayCardCommand : CompositeCommand {
        private Card card;

        public PlayCardCommand(Card card, CardZone stagingZone) {
            this.card = card;
            subcommands.Add(new MoveCardCommand(card, stagingZone));
            var cardCosts = card.GetData().GetCardCost();
            foreach (var resource in cardCosts.Keys) {
                for (int i = 0; i < cardCosts[resource]; i++) {
                    subcommands.Add(resource.CreatePayResourceCommand());
                }
            }
        }

        public override void OnFailure() {
            Debug.LogWarning($"Failed to play card {card.CardName}");
        }
    }
}