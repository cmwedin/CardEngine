using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;


namespace SadSapphicGames.CardEngine
{
    // [CreateAssetMenu(fileName = "CardSO", menuName = "SadSapphicGames/CardEngine/CardSO", order = 0)]
    public class CardSO : ScriptableObject {
        //properties
        // [SerializeField] private string _cardName;
        
        // [SerializeField] private string _cardName;
        // public string CardName {
        //     get => _cardName; 
        //     set => _cardName = value;
        // }
        public string CardName { get => this.name;}
        [SerializeField] private Sprite _cardSprite;
        public Sprite CardSprite {get => _cardSprite;}
        [SerializeField] private string _cardText;
        public string CardText {get => _cardText; set => _cardText = value;}
        [SerializeField] private List<TypeSO> _cardTypes = new List<TypeSO>();
        public ReadOnlyCollection<TypeSO> CardTypes {get => _cardTypes.AsReadOnly();}
        [SerializeField] private EffectSO _cardEffect;
        public EffectSO CardEffect {get => _cardEffect; set => _cardEffect = value;}
        
        private Dictionary<TypeSO,TypeDataSO> typesSubData = new Dictionary<TypeSO, TypeDataSO>();

        public void AddType(TypeSO typeToAdd) {
            Type typeDataSOType = typeToAdd.TypeDataReference.GetType(); 
                //? this name is pretty confusing: 
                //? this is the type of the scriptable object representing the data associated with the card-type we are adding
            _cardTypes.Add(typeToAdd);

            TypeDataSO typeData = (TypeDataSO)ScriptableObject.CreateInstance(typeDataSOType);
            typeData.name = $"{CardName}{typeToAdd.name}Data";
            AssetDatabase.AddObjectToAsset(typeData,AssetDatabase.GetAssetPath(this));
            typesSubData.Add(typeToAdd,(TypeDataSO)typeData);

            AssetDatabase.SaveAssets();
        }
        public void RemoveType(TypeSO typeToRemove) {
            if(!CardTypes.Contains(typeToRemove)) {
                Debug.LogWarning($"CardSO {CardName} does not have type {typeToRemove.name}");
            } else {
                TypeDataSO subdataToRemove = GetTypeSubdata(typeToRemove);
                typesSubData.Remove(typeToRemove);
                AssetDatabase.RemoveObjectFromAsset(subdataToRemove);
                _cardTypes.Remove(typeToRemove);
            }
        }
        public TypeDataSO GetTypeSubdata(TypeSO type) {
            if (!CardTypes.Contains(type)) {
                Debug.LogWarning($"{CardName} does not have type {type.name}");
                return null;
            } else {
                return typesSubData[type];
            }
        }
    }
}
