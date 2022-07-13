using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    public class CardActor : AbstractActor
    {
        [SerializeField] private DeckZone _actorDeckZone;
        [SerializeField] private DecklistSO _actorDecklist;

        public DeckZone ActorDeckZone { get => _actorDeckZone; set => _actorDeckZone = value; }
        public DecklistSO ActorDecklist { get => _actorDecklist; set => _actorDecklist = value; }

        public override void InitializeActor()
        {
            ActorDeckZone.LoadDecklist(ActorDecklist, this);
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