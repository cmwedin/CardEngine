using System;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    [CreateAssetMenu(fileName = "CardSO", menuName = "SadSapphicGames/CardEngine/TypeSO", order = 1)]
    public class TypeSO : ScriptableObject {
        private void OnEnable() {
            if(typeComponent == null) {
                Debug.LogWarning($"Type Scriptable Object {this.name} does not have an associated monobehavior component. Create a monobehaviour with the same name as the scriptable object");
            }
        }
        private Type typeComponent  { get => Type.GetType(this.name); }
        public void AddType(Card card) {
            card.gameObject.AddComponent(typeComponent); 
        }
    }
}