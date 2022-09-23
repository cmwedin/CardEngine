using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// CardZoneLayout used to arrange cards in a linear manner
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
            //GetComponent<RectTransform>().rect.Set(0,CardHeight/2,(CardWidth + cardSeparation)*zone.CardCount,CardHeight); 
            var zonePos = gameObject.transform.position;
            var Cards = zone.Cards;
            int CardCount = zone.CardCount;
            Vector3 perCardOffset = (CardWidth + cardSeparation) * Vector3.right;
            Vector3 prevCardPos = Vector3.zero;
            if (CardCount % 2 == 0) {
                for (int i = 0; i < zone.CardCount; i++) {
                    if (prevCardPos == Vector3.zero) {
                        Cards[i].transform.position = zonePos - perCardOffset * (CardCount / 2) + ((CardWidth + cardSeparation) / 2) * Vector3.right;
                    } else {
                        Cards[i].transform.position = prevCardPos + perCardOffset;
                    }
                    prevCardPos = Cards[i].transform.position;
                }
            } else {
                for (int i = 0; i < zone.CardCount; i++) {
                    if(prevCardPos == Vector3.zero) {
                        Cards[i].transform.position = zonePos - perCardOffset * (CardCount - 1) / 2;
                    } else {
                        Cards[i].transform.position = prevCardPos + perCardOffset;
                    }
                    prevCardPos = Cards[i].transform.position;
                }
            }
        }
    }
}