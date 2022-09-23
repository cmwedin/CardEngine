using UnityEngine;

namespace SadSapphicGames.CardEngine {
    /// <summary>
    /// Class that controls how cards are arranged in a CardZone
    /// </summary>
    [RequireComponent(typeof(CardZone))] public abstract class CardZoneLayout : MonoBehaviour {
        /// <summary>
        /// The zone to layout
        /// </summary>
        protected CardZone zone;
        /// <summary>
        /// The height of cards
        /// </summary>
        protected float CardHeight { get => CardEngineManager.instance.cardPrefabHeight;} 
        /// <summary>
        /// The width of cards
        /// </summary>
        protected float CardWidth { get => CardEngineManager.instance.cardPrefabWidth;} 
        /// <summary>
        /// sets the CardZone reference
        /// </summary>
        private void OnEnable() {
            zone = GetComponent<CardZone>();
            zone.OnCardAdded += () => LayoutCards();
            zone.OnCardRemoved += () => LayoutCards();
        }
        /// <summary>
        /// return the card positions to the desired layout
        /// </summary>
        public abstract void LayoutCards();
    }
}