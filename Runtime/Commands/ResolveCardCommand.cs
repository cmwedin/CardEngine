using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine
{
	public class ResolveCardCommand : CompositeCommand, IFailable
	{
        private Card card;

        public ResolveCardCommand(Card card) {
            this.card = card;
            AddChild(new EffectCommand(card.GetData().CardEffect));
            AddChild(new MoveCardCommand(card, ZoneManager.instance.GetActorsZones(card.GetController()).discardZone));
        }

        public bool WouldFail() {
            return !ZoneManager.instance.sharedZones.stagingZone.HasCard(card);
        }
    }
}
