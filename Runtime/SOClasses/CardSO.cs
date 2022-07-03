using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    [CreateAssetMenu(fileName = "CardSO", menuName = "SadSapphicGames/CardEngine/CardSO", order = 0)]
    public class CardSO : ScriptableObject {
        //properties
        // [SerializeField] private string _cardName;
        public string CardName {get => this.name;}
        [SerializeField] private Sprite _cardSprite;
        public Sprite CardSprite {get => _cardSprite;}
        [SerializeField] private string _cardText;
        public string CardText {get => _cardText; set => _cardText = value;}
        [SerializeField] private List<TypeSO> _cardTypes;
        public List<TypeSO> CardTypes {get => _cardTypes;}

        public void AddType(TypeSO typeSO) {
            CardTypes.Add(typeSO);
        }
    }
}
