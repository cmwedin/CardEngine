using System.Collections.Generic;
using UnityEngine;
    namespace SadSapphicGames.CardEngine {
    [System.Serializable]
    public struct ActorZones {
        public AbstractActor owner;
        public DeckZone deckZone;
        public HandZone handZone;
        public DiscardZone discardZone;
    }
    [System.Serializable]
    public struct SharedZones {
        public StagingZone stagingZone;
    }
    public class ZoneManager : MonoBehaviour {
        public static ZoneManager instance;
        public SharedZones sharedZones;
        [SerializeField]
        private List<ActorZones> actorZonesList;

        private void Awake() {
            if(instance != null && instance != this) {
                Destroy(this);
            } else {
                instance = this;
            }
        }
        public ActorZones GetActorsZones(AbstractActor actor) {
            foreach(ActorZones zones in actorZonesList) {
                if(zones.owner == actor) return zones;
            }
            throw new System.Exception("no set of zones found belonging to actor");
        }
    }
}