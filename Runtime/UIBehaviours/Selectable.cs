using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace SadSapphicGames.CardEngine
{
    public class Selectable : MonoBehaviour, ISelectHandler, IPointerDownHandler {
        public event Action<GameObject> SelectionMade;
        Outline highlight;

        public void OnPointerDown(PointerEventData eventData) {
            EventSystem.current.SetSelectedGameObject(gameObject, eventData);
        }

        public void OnSelect(BaseEventData eventData) {
            SelectionMade?.Invoke(this.gameObject);
            //? this should always be a listener but just in case
        }
        private void OnEnable() {
            highlight = gameObject.AddComponent<Outline>();
            highlight.effectColor = Color.cyan;
            highlight.effectDistance = new Vector2(2.5f, 2.5f);
        }
        private void OnDestroy() {
            Destroy(highlight);
        }
    }
}