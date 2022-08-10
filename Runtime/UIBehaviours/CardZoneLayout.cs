using UnityEngine;

namespace SadSapphicGames.CardEngine {
    [RequireComponent(typeof(CardZone))]
    public abstract class CardZoneLayout : MonoBehaviour {
        protected CardZone zone;
        protected float CardHeight { get => CardEngineManager.instance.cardPrefabHeight;} 
        protected float CardWidth { get => CardEngineManager.instance.cardPrefabWidth;} 

        private void OnEnable() {
            zone = GetComponent<CardZone>();
        }
        public abstract void LayoutCards();
    }
}