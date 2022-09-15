using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace SadSapphicGames.CardEngine{
    /// <summary>
    /// The MonoBehaviour component for a Card, currently designed around ui elements
    /// </summary>
    public class Card : MonoBehaviour
    {
        /// <summary>
        /// The ui component for displaying the card name
        /// </summary>
        [SerializeField] private  TextMeshProUGUI cardNameArea;
        /// <summary>
        /// The ui component for displaying the card text
        /// </summary>
        [SerializeField] private TextMeshProUGUI cardDescriptionArea;
        /// <summary>
        /// The game object to cover the card with the card back
        /// </summary>
        [SerializeField] private GameObject cardBack;
        /// <summary>
        /// The game object to display a sprite for the card
        /// </summary>
        [SerializeField] private GameObject cardImageArea;
        /// <summary>
        /// The controller of the card
        /// </summary>
        private AbstractActor controller;
        /// <summary>
        /// The data for the card
        /// </summary>
        private CardSO cardData;
        /// <summary>
        /// property to get the card name from the data
        /// </summary>
        public string CardName { get => cardData.CardName; }
        /// <summary>
        /// property to get the card text from the data
        /// </summary>
        public string CardText{ get => cardData.CardText;}
        /// <summary>
        /// property to enable or disable the card back and cover the front
        /// </summary>
        public bool IsVisible { get => !cardBack.activeSelf; set => cardBack.SetActive(!value); }
        /// <summary>
        /// The card types the card has
        /// </summary>
        public CardType[] cardTypes { get => gameObject.GetComponents<CardType>();}
        /// <summary>
        /// The current zone the card is in
        /// </summary>
        public CardZone CurrentZone { get; set; }
        /// <summary>
        /// Loads a CardSO into the components data
        /// </summary>
        /// <param name="cardDataSO">The CardSO to use as data</param>
        public void LoadData(CardSO cardDataSO) {
            cardData = cardDataSO;
            foreach (var cardTypeSO in cardData.CardTypes) {
                cardTypeSO.AddTypeToGameObject(this);
            }
            cardNameArea.text = CardName;
            cardDescriptionArea.text = CardText;
        }
        /// <summary>
        /// Gets the CardSO currently set as the cards data
        /// </summary>
        /// <returns>THe current CardSO being used as data</returns>
        public CardSO GetData() {
            return cardData;
        }
        /// <summary>
        /// Sets the controller of the card
        /// </summary>
        /// <param name="_controller">the controller to set as the cards controller</param>
        public void SetController(AbstractActor _controller) {
            controller = _controller;
        }
        /// <summary>
        /// Get the current controller of the card
        /// </summary>
        /// <returns>the current controller of the card</returns>
        public AbstractActor GetController(){
            return controller;
        }
    }
}