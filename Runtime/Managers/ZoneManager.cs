using System.Collections.Generic;
using UnityEngine;
    
namespace SadSapphicGames.CardEngine {
    /// <summary>
    /// struct for CardZones that belong to a specific Actor
    /// </summary>
    [System.Serializable] public struct ActorZones {
        public AbstractActor owner;
        public DeckZone deckZone;
        public HandZone handZone;
        public DiscardZone discardZone;
    }
    /// <summary>
    /// Zones shared by all Actors
    /// </summary>
    [System.Serializable] public struct SharedZones {
        public StagingZone stagingZone;
    }
    /// <summary>
    /// Singleton manager to provide cross class access to different CardZones without requiring a reference
    /// </summary>
    public class ZoneManager : MonoBehaviour {
        /// <summary>
        /// Singleton instance of the manager
        /// </summary>
        public static ZoneManager instance;
        /// <summary>
        /// The shared zones 
        /// </summary>
        public SharedZones sharedZones;
        /// <summary>
        /// The zones that belong to a specific actor
        /// </summary>
        [SerializeField] private List<ActorZones> actorZonesList;
        /// <summary>
        /// Sets the singleton instance
        /// </summary>
        private void Awake() {
            if(instance != null && instance != this) {
                Destroy(this);
            } else {
                instance = this;
            }
        }
        /// <summary>
        /// Get the zones belonging to a specific actor
        /// </summary>
        /// <exception cref="System.ArgumentException">Thrown none of the ActorZones inf the actorZonesList belong to the argument</exception>
        public ActorZones GetActorsZones(AbstractActor actor) {
            foreach(ActorZones zones in actorZonesList) {
                if(zones.owner == actor) return zones;
            }
            throw new System.ArgumentException("no set of zones found belonging to actor");
        }
    }
}