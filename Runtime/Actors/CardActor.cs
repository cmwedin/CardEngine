using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    public class CardActor : AbstractActor
    {
        [SerializeField] private DecklistSO _actorDecklist;

        public DecklistSO ActorDecklist { get => _actorDecklist; set => _actorDecklist = value; }

        public override void InitializeActor() {
            ZoneManager.instance.GetActorsZones(this).deckZone.LoadDecklist(ActorDecklist, this);
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}