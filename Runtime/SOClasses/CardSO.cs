using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;


namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// Scriptable object that stores the data of particular cards
    /// </summary>
    public class CardSO : ScriptableObject {
        /// <summary>
        /// used to store the subdata for each card type the card has, used as a workaround for unity not serializing dictionaries
        /// </summary>
        [System.Serializable] private class SubdataEntry { 
            /// <summary>
            /// The name of the type this subdata belongs to
            /// </summary>
            public string typeName;
            /// <summary>
            /// The types subdata
            /// </summary>
            public TypeDataSO typeSubdata;
            /// <summary>
            /// Constructs a SubdataEntry 
            /// </summary>
            public SubdataEntry(string typeName, TypeDataSO typeSubdata) {
                this.typeName = typeName;
                this.typeSubdata = typeSubdata;
            }
        }
        /// <summary>
        /// The resources the spells cost and how much of that resource the cost, values set in the inspector
        /// </summary>
        [System.Serializable] private class ResourceCost { 
            /// <summary>
            /// The resource
            /// </summary>
            public ResourceSO resource;
            /// <summary>
            /// How much of that resource the card costs
            /// </summary>
            public int costMagnitude;
        }
        /// <summary>
        /// Property that gets the card name from the name of the scriptable object
        /// </summary>
        public string CardName { get => this.name;}
        /// <summary>
        /// Property fot the cards sprite
        /// </summary>
        [SerializeField] private Sprite _cardSprite;
        public Sprite CardSprite {get => _cardSprite;}
        /// <summary>
        /// property for the cards text
        /// </summary>
        [SerializeField] private string _cardText;
        public string CardText {get => _cardText; set => _cardText = value;}
        /// <summary>
        /// property for the card types
        /// </summary>
        [SerializeField] private List<TypeSO> _cardTypes = new List<TypeSO>();
        public ReadOnlyCollection<TypeSO> CardTypes {get => _cardTypes.AsReadOnly();}
        /// <summary>
        /// Property for the cards effect
        /// </summary>
        [SerializeField] private CompositeEffectSO _cardEffect;
        public CompositeEffectSO CardEffect {get => _cardEffect; set => _cardEffect = value;}
        
        /// <summary>
        /// Collection of the subdata for each type
        /// </summary>
        [SerializeField]private List<SubdataEntry> typesSubData = new List<SubdataEntry>();
        /// <summary>
        /// Collection of the costs for each resource
        /// </summary>
        [SerializeField] private List<ResourceCost> cardCosts = new List<ResourceCost>();
        /// <summary>
        /// Adds a type to the CardSO
        /// </summary>
        /// <param name="typeToAdd">The type to add</param>
        /// <exception cref="ArgumentException">Thrown if the CardSO already has the type</exception>
        public void AddType(TypeSO typeToAdd) {
            if(CardTypes.Contains(typeToAdd)) {
                throw new ArgumentException($"Card already has type {typeToAdd.name}");
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
        /// <summary>
        /// Removes a type from the CardSO
        /// </summary>
        /// <param name="typeToRemove">The type to remove</param>
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
        /// <summary>
        /// Checks if the CardSO has a given card type
        /// </summary>
        /// <returns>If the CardSO has the given type</returns>
        public bool HasType(TypeSO type) {
            return CardTypes.Contains(type);
        }
        /// <summary>
        /// Checks if the CardSO has a given type by name
        /// </summary>
        /// <returns>If the CardSO has the given type</returns>
        public bool HasType(string typeName) {
            foreach (var type in CardTypes) {
                if(type.name == typeName) return true;
            }
            return false;
        }
        /// <summary>
        /// Gets the subdata that corresponds to a given type
        /// </summary>
        /// <returns>The types subdata</returns>
        /// <exception cref="Exception">Throw if the card has the given type but its subdata couldn't be found </exception>
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
                throw new Exception($"Failed to find matching entry subdata list; however the card does have type {type.name}");
            }
        }
        /// <summary>
        /// Gets the costs of the card as a dictionary
        /// </summary>
        public Dictionary<ResourceSO,int> GetCardCost(){
            Dictionary<ResourceSO, int> output = new();
            foreach (var resource in cardCosts) {
                output.Add(resource.resource, resource.costMagnitude);
            }
            return output;
        }
    }
}
