using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// Component to indicate a game object as selectable
    /// </summary>
    public class Selectable : MonoBehaviour, ISelectHandler, IPointerDownHandler {
        /// <summary>
        /// event invoked when the selectable is clicked on
        /// </summary>
        public event Action<GameObject> SelectionMade;
        /// <summary>
        /// highlights the object to indicate it is selectable
        /// </summary>
        Outline highlight;
        /// <summary>
        /// Invoked when the game object is clicked on
        /// </summary>
        /// <param name="eventData">PointerEventData for the click</param>
        public void OnPointerDown(PointerEventData eventData) {
            EventSystem.current.SetSelectedGameObject(gameObject, eventData);
        }
        /// <summary>
        /// Invoked when the gameobject is selected
        /// </summary>
        /// <param name="eventData">the event data of the selection</param>
        public void OnSelect(BaseEventData eventData) {
            SelectionMade?.Invoke(this.gameObject);
        }
        /// <summary>
        /// adds the outlien component to the game object
        /// </summary>
        private void OnEnable() {
            highlight = gameObject.AddComponent<Outline>();
            highlight.effectColor = Color.cyan;
            highlight.effectDistance = new Vector2(2.5f, 2.5f);
        }
        /// <summary>
        /// destroys the highlight component when the selectable component is destroyed
        /// </summary>
        private void OnDestroy() {
            Destroy(highlight);
        }
    }
}