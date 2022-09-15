using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// An actor that plays cards from a deck
    /// </summary>
    public class CardActor : AbstractActor
    {
        /// <summary>
        /// The deck list the CardActor is using
        /// </summary>
        [SerializeField] private DecklistSO _actorDecklist;

        public DecklistSO ActorDecklist { get => _actorDecklist; set => _actorDecklist = value; }
        /// <summary>
        /// Loads the decklist into the Actors DeckZone
        /// </summary>
        public override void InitializeActor() {
            ZoneManager.instance.GetActorsZones(this).deckZone.LoadDecklist(ActorDecklist, this);
        }
    }
}