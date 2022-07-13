using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace SadSapphicGames.CardEngine{
    public class Card : MonoBehaviour
    {
        [SerializeField] private  TextMeshProUGUI cardNameArea;
        [SerializeField] private TextMeshProUGUI cardDescriptionArea;
        [SerializeField] private GameObject cardBack;
        [SerializeField] private GameObject cardImageArea;
        private AbstractActor controller;
        private CardSO cardData;

        public string CardName { get => cardData.CardName; }
        public string CardText{ get => cardData.CardText;}
        public bool IsVisible { get => !cardBack.activeSelf; set => cardBack.SetActive(!value); }
        public CardType[] cardTypes { get => gameObject.GetComponents<CardType>();}
        public CardZone CurrentZone { get; internal set; }

        public void LoadData(CardSO cardDataSO) {
            cardData = cardDataSO;
            foreach (var cardTypeSO in cardData.CardTypes) {
                cardTypeSO.AddTypeToGameObject(this);
            }
            cardNameArea.text = CardName;
            cardDescriptionArea.text = CardText;
        }
        public void SetController(AbstractActor _controller) {
            controller = _controller;
        }
    }
}