using System;
using UnityEngine;


namespace SadSapphicGames.CardEngine
{
    
    public abstract class TypeDataSO : ScriptableObject {
        private Type dataObjectType;
        private void OnEnable() {
            dataObjectType = this.GetType();
        }
        
        public void CreateObject() {
            ScriptableObject.CreateInstance(dataObjectType);
            
        }
    }
}