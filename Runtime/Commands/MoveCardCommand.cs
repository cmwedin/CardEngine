using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine {
    public class MoveCardCommand : Command
    {
        Card card;
        CardZone moveToZone;
        CardZone previousZone;

        public MoveCardCommand(Card card, CardZone moveToZone)
        {
            if(moveToZone == null || card == null) throw new System.ArgumentNullException();
            this.card = card;
            this.moveToZone = moveToZone;
            this.previousZone = card.CurrentZone;
        }

        public override void Execute()
        {
            if(previousZone != null) {
                previousZone.RemoveCard(card);
            }
            moveToZone.AddCard(card);
        }

        public override void Undo()
        {
            moveToZone.RemoveCard(card);
            if(previousZone != null) {
                previousZone.AddCard(card);
            }    
        }
    }
}
