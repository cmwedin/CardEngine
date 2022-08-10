using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    public class LinearCardLayout : CardZoneLayout
    {
        [SerializeField] private float cardSeparation;

        public override void LayoutCards()
        {
            GetComponent<RectTransform>().rect.Set(0,CardHeight/2,(CardWidth + cardSeparation)*zone.CardCount,CardHeight); 
            
        }
    }
}