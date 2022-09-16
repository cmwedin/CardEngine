using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SadSapphicGames.CardEngine {
    /// <summary>
    /// Component used to make cards draggable
    /// </summary>
    public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
        /// <summary>
        /// Property to get the card component
        /// </summary>
        private Card card { get => GetComponent<Card>(); }
        /// <summary>
        /// Property to get the current zone
        /// </summary>
        private CardZone CurrentZone { get => card.CurrentZone; }
        
        /// <summary>
        /// The offset from the center of the card to where the card was clicked on
        /// </summary>
        private Vector2 dragOffset;
        /// <summary>
        /// The layout group the Card's current CardZone
        /// </summary>
        private LayoutGroup layoutGroup { get => CurrentZone.GetComponent<LayoutGroup>(); }
        /// <summary>
        /// Updates the layout of layoutGroup
        /// </summary>
        private void PokeLayoutGroup() {
            if(layoutGroup == null) return;
            layoutGroup.enabled = false;
            layoutGroup.enabled = true;
        }
    // * IDragHandler
        /// <summary>
        /// Invoked when a drag starts
        /// </summary>
        /// <param name="eventData">where the card was clicked on</param>
        public void OnBeginDrag(PointerEventData eventData) {
            UnityEngine.Debug.Log($"Dragging {GetComponent<Card>().CardName}");
            dragOffset = eventData.position - (Vector2)gameObject.transform.position;
            // layoutGroup = gameObject.GetComponentInParent<LayoutGroup>();
        } 
        /// <summary>
        /// Invoked while the drag continues
        /// </summary>
        /// <param name="eventData">where the card was clicked on</param>
        public void OnDrag(PointerEventData eventData) {
            if (!CurrentZone.CardsDraggable) { return; }
            gameObject.transform.position = eventData.position - dragOffset;  
        } 
        /// <summary>
        /// Invoked when the card is dropped
        /// </summary>
        /// <param name="eventData">Where the card was dropped</param>
        public void OnEndDrag(PointerEventData eventData) {
            UnityEngine.Debug.Log($"{GetComponent<Card>().CardName} dropped");
            dragOffset = Vector2.zero;
            CardEngineManager.instance.ValidatePlay(gameObject.GetComponent<Card>(),eventData);
            PokeLayoutGroup();
        }
    }
}
