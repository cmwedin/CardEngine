using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SadSapphicGames.CardEngine {
    [RequireComponent(typeof(CanvasGroup))]
    public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
        private Card card { get => GetComponent<Card>(); }
        private CardZone CurrentZone { get => card.CurrentZone; }
        
        //Monobehaviour callbacks
        private void OnEnable() {
            Card card = GetComponent<Card>();
            CardZone currentZone = card.CurrentZone;
        }
        private Vector2 dragOffset;
        private LayoutGroup layoutGroup;
        //? ugly function to return the card to its proper place if it isn't dropped in the cast trigger zone, probably a smoother way to do this
        private void PokeLayoutGroup() {
            layoutGroup.enabled = false;
            layoutGroup.enabled = true;
        }
    // * IDragHandler
        public void OnBeginDrag(PointerEventData eventData) {
            UnityEngine.Debug.Log($"Dragging {GetComponent<Card>().CardName}");
            dragOffset = eventData.position - (Vector2)gameObject.transform.position;
            layoutGroup = gameObject.GetComponentInParent<LayoutGroup>();
        } public void OnDrag(PointerEventData eventData) {
            if (!CurrentZone.CardsDraggable) { return; }
            gameObject.transform.position = eventData.position - dragOffset;  
        } public void OnEndDrag(PointerEventData eventData) {
            UnityEngine.Debug.Log($"{GetComponent<Card>().CardName} dropped");
            dragOffset = Vector2.zero;
            CardEngineManager.instance.ValidatePlay(gameObject.GetComponent<Card>(),eventData);
            PokeLayoutGroup();
        }
    }
}
