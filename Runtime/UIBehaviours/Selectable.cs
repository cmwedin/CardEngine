using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace SadSapphicGames.CardEngine
{
    public class Selectable : MonoBehaviour, ISelectHandler, IPointerDownHandler {
        public event Action<GameObject> SelectionMade;

        public void OnPointerDown(PointerEventData eventData) {
            EventSystem.current.SetSelectedGameObject(gameObject, eventData);
        }

        public void OnSelect(BaseEventData eventData) {
            SelectionMade?.Invoke(this.gameObject);
            //? this should always be a listener but just in case
        }
    }
}