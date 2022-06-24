using UnityEngine;
using UnityEngine.EventSystems;

namespace SadSapphicGames.CardEngine
{
    public class DropZone : MonoBehaviour, IDropHandler {
        // [SerializeField] private CardStack stack;
        public void OnDrop(PointerEventData eventData) {
            UnityEngine.Debug.Log("drop detected in dropzone");
            Card cardDropped = eventData.pointerDrag.GetComponent<Card>();
            if(cardDropped == null) {throw new System.Exception("No card component found on dropped gameobject");} 
            // stack.CastToStack(cardDropped);
        }
    }
}