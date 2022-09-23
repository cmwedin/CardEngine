using System;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// Scriptable object to represent a card type in a CardSO data holder
    /// </summary>
    public class TypeSO : ScriptableObject {
        /// <summary>
        /// Reference to the types monobehaviour component through a prefab, generated by the Create/CardType tool
        /// </summary>
        [SerializeField] private CardType componentReferencePrefab;
        /// <summary>
        /// Reference SO for the type subdata to be copied when adds the TypeSO to a CardSO
        /// </summary>
        [SerializeField] private TypeDataSO typeDataReference;
        public TypeDataSO TypeDataReference { get => typeDataReference; }
        
        /// <summary>
        /// A UnitEffect that will be added to the resolution of any effect of this type. Can be used to move the cards of this type to a certain zone upon resolution
        /// </summary>
        [SerializeField] private UnitEffectSO defaultTypeEffect;
        public UnitEffectSO DefaultTypeEffect { get => defaultTypeEffect; }

        /// <summary>
        /// Property to get the type of componentReferencePrefab (the type of the component this TypeSO will add )
        /// </summary>
        public Type typeComponent { get => componentReferencePrefab.GetType();}

        /// <summary>
        /// Adds typeComponent to a Card game object
        /// </summary>
        /// <param name="card">The Card to add typeComponent too</param>
        /// <exception cref="NullReferenceException">thrown if typeComponent is null</exception>
        public void AddTypeToGameObject(Card card) {
            if(typeComponent == null) {throw new NullReferenceException($"this type's associated component is null");}
            card.gameObject.AddComponent(typeComponent); 
        }
        /// <summary>
        /// Sets the component reference using a object with a CardType component, handled by the create/CardType tool
        /// </summary>
        /// <param name="_componentReference">An object that has this TypeSO's associated component</param>
        public void SetComponentReference(CardType _componentReference) {
            if(componentReferencePrefab != null) {
                Debug.LogWarning("the reference prefab for this type has already been set");
                return;
            } else {
                componentReferencePrefab = _componentReference;
            }
        }
        /// <summary>
        /// Sets the data reference of this TypeSO using a TypeDataSO asset, handled by the create/CardType tool
        /// </summary>
        /// <param name="_dataReference">the TypeDataSO asset to use as a reference for this types subdata</param>
        public void SetDataReference(TypeDataSO _dataReference) {
            if(typeDataReference != null) {
                Debug.LogWarning("the reference prefab for this type has already been set");
                return;
            } else {
                typeDataReference = _dataReference;
            }
        }
    }
}