using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// Currently unused component to layout cards in a zone in a linear manner
    /// </summary>
    public class LinearCardLayout : CardZoneLayout
    {
        /// <summary>
        /// The spacing between cards
        /// </summary>
        [SerializeField] private float cardSeparation;

        /// <summary>
        /// Lays the card out into a line
        /// </summary>
        public override void LayoutCards()
        {
            GetComponent<RectTransform>().rect.Set(0,CardHeight/2,(CardWidth + cardSeparation)*zone.CardCount,CardHeight); 
            //TODO set the position of the cards in the cardzone    
        }
    }
}