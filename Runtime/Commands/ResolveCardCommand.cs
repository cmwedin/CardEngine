using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine
{
	public class ResolveCardCommand : Command, IFailable
	{
        private Card card;
        private EffectSO cardEffect;

        public ResolveCardCommand(Card card) {
            this.card = card;
            cardEffect = card.GetData().CardEffect;
        }

        public override void Execute() {
            cardEffect.ResolveEffect();
        }

        public bool WouldFail() {
            return !ZoneManager.instance.stagingZone.HasCard(card);
        }
    }
}
