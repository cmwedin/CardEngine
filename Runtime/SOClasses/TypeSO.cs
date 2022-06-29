using System;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    [CreateAssetMenu(fileName = "CardSO", menuName = "SadSapphicGames/CardEngine/TypeSO", order = 1)]
    public class TypeSO : ScriptableObject {
        [SerializeField] private CardType typeReferencePrefab;
        [SerializeField] private TypeDataSO typeDataReference;

        private void OnEnable() {
            if(typeReferencePrefab == null) {
                Debug.LogWarning($"Type Scriptable Object {this.name} does not have an associated monobehavior component. See documentation for how to create one.");
            } else if (typeReferencePrefab.GetComponents<CardType>().Length != 1) {
                Debug.LogWarning($"the reference object for TypeSO {this.name} has multiple components of type CardType, see documentation for proper method of creating a CardType reference object");
            }
        }
        
        // private Type typeComponent  { get => Type.GetType(this.name); }
        private Type typeComponent { get => typeReferencePrefab.GetType();}

        public void AddTypeTo(Card card) {
            if(typeComponent == null) {throw new Exception($"there is no monobehaviour component associated with {this.name} to add");}
            card.gameObject.AddComponent(typeComponent); 
        }
    }
}