using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine {
    /// <summary>
    /// Command to move a card between zones
    /// </summary>
    public class MoveCardCommand : Command, IUndoable
    {
        /// <summary>
        /// The Card to move
        /// </summary>
        Card card;
        /// <summary>
        /// The zone to move the card too
        /// </summary>
        CardZone moveToZone;
        /// <summary>
        /// The zone the card was previously in
        /// </summary>
        CardZone previousZone;
        /// <summary>
        /// The commands undo command
        /// </summary>
        MoveCardCommand undoCommand;
        /// <summary>
        /// Constructs a MoveCardCommand with a given Card and a given CardZone to move it too. If the later is null the Command will just remove the card from its current zone
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Thrown if the given Card is null</exception>
        public MoveCardCommand(Card card, CardZone moveToZone)
        {
            if(card == null) throw new System.ArgumentNullException();
            this.card = card;
            this.moveToZone = moveToZone;
            this.previousZone = card.CurrentZone;
        }
        /// <summary>
        /// Removes the card from its previous zone, if it has one, and move's the card to its new zone, if one was given
        /// </summary>
        public override void Execute()
        {
            if(previousZone != null) {
                previousZone.RemoveCard(card);
            }
            if (moveToZone != null) {
                moveToZone.AddCard(card);
            }
        }
        /// <summary>
        /// returns a command that moves the card back to its previous zone
        /// </summary>
        /// <returns>A command that will move the card back to its previous zone</returns>
        public ICommand GetUndoCommand()
        {
            if(undoCommand == null) {
                undoCommand = new MoveCardCommand(card, previousZone);
            }
            return undoCommand;
        }
    }
}
