using System;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    // [CreateAssetMenu(fileName = "CardSO", menuName = "SadSapphicGames/CardEngine/TypeSO", order = 1)]
    public class TypeSO : ScriptableObject {
        [SerializeField] private CardType componentReferencePrefab;
        [SerializeField] private TypeDataSO typeDataReference;
        
        // private Type typeComponent  { get => Type.GetType(this.name); }
        private Type typeComponent { get => componentReferencePrefab.GetType();}
        public TypeDataSO TypeDataReference { get => typeDataReference; }

        public void AddTypeToGameObject(Card card) {
            if(typeComponent == null) {throw new Exception($"there is no monobehaviour component associated with {this.name} to add");}
            card.gameObject.AddComponent(typeComponent); 
        }
        public void SetComponentReference(CardType _componentReference) {
            if(componentReferencePrefab != null) {
                Debug.LogWarning("the reference prefab for this type has already been set");
                return;
            }
            componentReferencePrefab = _componentReference;
        }
        public void SetDataReference(TypeDataSO _dataReference) {
            if(typeDataReference != null) {
                Debug.LogWarning("the reference prefab for this type has already been set");
                return;
            }
            typeDataReference = _dataReference;
        }
    }
}