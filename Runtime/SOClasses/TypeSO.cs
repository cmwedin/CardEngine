using System;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    // [CreateAssetMenu(fileName = "CardSO", menuName = "SadSapphicGames/CardEngine/TypeSO", order = 1)]
    public class TypeSO : ScriptableObject {
        [SerializeField] private CardType typeReferencePrefab;
        [SerializeField] private TypeDataSO typeDataReference;
        [HideInInspector] public bool initialized = false; 

        private void OnEnable() {
            if(initialized == false) {
                Debug.LogWarning($"TypeSO {name} not initialized");
            }
        }
        
        // private Type typeComponent  { get => Type.GetType(this.name); }
        private Type typeComponent { get => typeReferencePrefab.GetType();}
        public TypeDataSO TypeDataReference { get => typeDataReference; }

        public void AddTypeToGameObject(Card card) {
            if(typeComponent == null) {throw new Exception($"there is no monobehaviour component associated with {this.name} to add");}
            card.gameObject.AddComponent(typeComponent); 
        }
    }
}