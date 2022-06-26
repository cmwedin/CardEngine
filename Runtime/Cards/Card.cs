using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine{
    public class Card : MonoBehaviour
    {
        [SerializeField] private GameObject cardNameArea;
        [SerializeField] private GameObject cardDescriptionArea;
        [SerializeField] private GameObject cardBack;
        [SerializeField] private GameObject cardImageArea;
        private CardSO cardData;

        public string CardName { get => cardData.CardName; }
        public string CardText{ get => cardData.CardText;}
        public bool IsVisible { get => !cardBack.activeSelf; set => cardBack.SetActive(!value); }
        public CardType[] cardTypes { get => gameObject.GetComponents<CardType>();}
        public CardZone CurrentZone { get; internal set; }

        public void LoadData(CardSO cardDataSO) {
            cardData = cardDataSO;
            foreach (var cardTypeSO in cardData.CardTypes)
            {
                cardTypeSO.AddTypeTo(this);
            }
        }
    }
}