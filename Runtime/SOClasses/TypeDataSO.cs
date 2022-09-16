using System;
using UnityEngine;


namespace SadSapphicGames.CardEngine
{
    
    /// <summary>
    /// Abstract class base for scriptable objects that represent the subdata associated with a specific card type, may not be needed for all types
    /// </summary>
    public abstract class TypeDataSO : ScriptableObject {
        /// <summary>
        /// The type of the scriptable object
        /// </summary>
        private Type dataObjectType;
        /// <summary>
        /// Sets dataObjectType
        /// </summary>
        private void OnEnable() {
            dataObjectType = this.GetType();
        }
        /// <summary>
        /// Creates a scriptable object of the type of dataObjectType, used in the create tools when making card
        /// </summary>
        public void CreateObject() {
            ScriptableObject.CreateInstance(dataObjectType);
            
        }
    }
}