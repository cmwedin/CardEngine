using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    [CreateAssetMenu(fileName = "CardSO", menuName = "SadSapphicGames/CardEngine/CardSO", order = 0)]
    public class CardSO : ScriptableObject {
        //properties
        [SerializeField] private string _CardName;
        public string CardName {get => _CardName;}
        [SerializeField] private string _CardID;
        public string CardID {get => _CardID;}
        [SerializeField] private string _EffectText;
        public string EffectText {get => _EffectText;}
        [SerializeField] private List<TypeSO> _CardTypes;
        public List<TypeSO> CardTypes {get => _CardTypes;} 
    }
}
