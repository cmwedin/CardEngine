using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine {
    public class MoveCardCommand : Command, IUndoable
    {
        Card card;
        CardZone moveToZone;
        CardZone previousZone;

        public MoveCardCommand(Card card, CardZone moveToZone)
        {
            if(card == null) throw new System.ArgumentNullException();
            this.card = card;
            this.moveToZone = moveToZone;
            this.previousZone = card.CurrentZone;
        }

        public override void Execute()
        {
            if(previousZone != null) {
                previousZone.RemoveCard(card);
            }
            if (moveToZone != null) {
                moveToZone.AddCard(card);
            }
        }

        public ICommand GetUndoCommand()
        {
            return new MoveCardCommand(card, previousZone);
        }
    }
}
