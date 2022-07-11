using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        [SerializeField] private List<TypeSO> _cardTypes = new List<TypeSO>();
        public List<TypeSO> CardTypes {get => _cardTypes;}
        
        private Dictionary<TypeSO,TypeDataSO> typesSubData = new Dictionary<TypeSO, TypeDataSO>();

        public void AddType(DatabaseEntry<TypeSO> typeDatabaseEntry) {
            TypeSO typeSO = typeDatabaseEntry.entrykey;
            Type typeDataSOType = typeSO.TypeDataReference.GetType(); 
                //? this name is pretty confusing: 
                //? this is the type of the scriptable object representing the data associated with the card-type we are adding
            CardTypes.Add(typeSO);

            TypeDataSO typeData = (TypeDataSO)ScriptableObject.CreateInstance(typeDataSOType);
            typeData.name = $"{CardName}{typeSO.Name}Data";
            AssetDatabase.CreateAsset(typeData,$"{CardDatabaseSO.instance.GetEntryByKey(this).entryDirectory}/{typeData.name}.asset");
            typesSubData.Add(typeSO,typeData);
            
            AssetDatabase.SaveAssets();
        }
        public TypeDataSO GetTypeSubdata(TypeSO type) {
            if (!CardTypes.Contains(type)) {
                Debug.LogWarning($"{CardName} does not have type {type.Name}");
                return null;
            } else {
                return typesSubData[type];
            }
        }
    }
}
