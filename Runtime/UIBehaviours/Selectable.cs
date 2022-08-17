using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace SadSapphicGames.CardEngine
{
    public class Selectable : MonoBehaviour, ISelectHandler {
        public event Action<GameObject> SelectionMade;
        public void OnSelect(BaseEventData eventData)
        {
            SelectionMade?.Invoke(this.gameObject);
            //? this should always be a listener but just in case
        }
    }
}