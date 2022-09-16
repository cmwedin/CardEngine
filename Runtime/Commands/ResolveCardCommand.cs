using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine
{
	/// <summary>
    /// The Command to resolve a Card from the StagingZone
    /// </summary>
    public class ResolveCardCommand : CompositeCommand, IFailable
	{
        /// <summary>
        /// The card to resolve
        /// </summary>
        private Card card;
        /// <summary>
        /// Constructs a ResolveCardCommand for a given card
        /// </summary>
        /// <param name="card"></param>
        public ResolveCardCommand(Card card) {
            this.card = card;
            AddChild(new EffectCommand(card.GetData().CardEffect));
            AddChild(new MoveCardCommand(card, ZoneManager.instance.GetActorsZones(card.GetController()).discardZone));
        }
        /// <summary>
        /// True if the card is no longer in the StagingZone
        /// </summary>
        /// <returns></returns>
        public bool WouldFail() {
            return !ZoneManager.instance.sharedZones.stagingZone.HasCard(card);
        }
    }
}
