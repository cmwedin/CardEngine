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
        [System.Serializable] 
        private class SubdataEntry { //? a bit of a hack-ey workaround to unity not serializing dictionaries
            public string typeName;
            public TypeDataSO typeSubdata;

            public SubdataEntry(string typeName, TypeDataSO typeSubdata) {
                this.typeName = typeName;
                this.typeSubdata = typeSubdata;
            }
        }
        [System.Serializable]
        private class ResourceCost { //? Same
            public ResourceSO resource;
            public int costMagnitude;
        }
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
        
        [SerializeField]private List<SubdataEntry> typesSubData = new List<SubdataEntry>();
        [SerializeField] private List<ResourceCost> cardCosts = new List<ResourceCost>();
        public void AddType(TypeSO typeToAdd) {
            if(CardTypes.Contains(typeToAdd)) {
                throw new Exception($"Card already has type {typeToAdd.name}");
            }
            Type typeDataSOType = typeToAdd.TypeDataReference.GetType(); 
                //? this name is pretty confusing: 
                //? this is the type of the scriptable object representing the data associated with the card-type we are adding
            _cardTypes.Add(typeToAdd);

            TypeDataSO typeData = (TypeDataSO)ScriptableObject.CreateInstance(typeDataSOType);
            typeData.name = $"{CardName}{typeToAdd.name}Data";
            AssetDatabase.AddObjectToAsset(typeData,AssetDatabase.GetAssetPath(this));
            typesSubData.Add(new SubdataEntry(typeToAdd.name,typeData));

            AssetDatabase.SaveAssets();
        }
        public void RemoveType(TypeSO typeToRemove) {
            if(!CardTypes.Contains(typeToRemove)) {
                Debug.LogWarning($"CardSO {CardName} does not have type {typeToRemove.name}");
            } else {
                TypeDataSO subdataToRemove = GetTypeSubdata(typeToRemove);
                foreach (var entry in typesSubData) {
                    if(entry.typeName == typeToRemove.name) {
                        typesSubData.Remove(entry);
                        break;
                    }
                }
                AssetDatabase.RemoveObjectFromAsset(subdataToRemove);
                _cardTypes.Remove(typeToRemove);
            }
        }
        public bool HasType(TypeSO type) {
            return CardTypes.Contains(type);
        }
        public bool HasType(string typeName) {
            foreach (var type in CardTypes) {
                if(type.name == typeName) return true;
            }
            return false;
        }
        public TypeDataSO GetTypeSubdata(TypeSO type) {
            if (!CardTypes.Contains(type)) {
                Debug.LogWarning($"{CardName} does not have type {type.name}");
                return null;
            } else {
                foreach (var entry in typesSubData) {
                    if(entry.typeName == type.name) {
                        return entry.typeSubdata;
                    }
                }
                throw new Exception($"Failed to find matching entry subdata list, card does have type {type.name}");
            }
        }
    }
}
