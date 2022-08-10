using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine
{
	public class ResolveCardCommand : Command
	{
        private Card card;
        private EffectSO cardEffect;

        public ResolveCardCommand(Card card) {
            this.card = card;
        }

        public override void Execute() {
            throw new System.NotImplementedException();
        }
    }
}
