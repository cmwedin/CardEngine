using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    //? Asset menu tagged using in development
    // [CreateAssetMenu(fileName = "CompositeEffectSO", menuName = "CardEngineDevelopment/CompositeEffectSO", order = 0)]
    /// <summary>
    /// A scriptable object for encapsulating multiple child effects in a single object, used for representing Card effects
    /// </summary>
    public class CompositeEffectSO : EffectSO {
        /// <summary>
        /// The children encapsulated in the CompositeEffectSO
        /// </summary>
        [SerializeField] private List<EffectSO> subEffects = new List<EffectSO>();
        /// <summary>
        /// Public read only version of the subeffects
        /// </summary>
        public ReadOnlyCollection<EffectSO> Subeffects { get => subEffects.AsReadOnly();}
        /// <summary>
        /// The controller of the effect
        /// </summary>
        public override AbstractActor Controller {
            get => base.Controller; 
            set {
                foreach (var child in subEffects) {
                    child.Controller = value;
                }
                base.Controller = value;
            }
        }
        /// <summary>
        /// The number of child effects
        /// </summary>
        public int ChildrenCount { get => subEffects.Count;}
        /// <summary>
        /// Preforms the effects of the CompositeEffect
        /// </summary>
        public override void ResolveEffect() {
            foreach (var effect in subEffects) {
                effect.ResolveEffect();                
            }
        }
        /// <summary>
        /// Adds a child effect to the composite
        /// </summary>
        /// <param name="desiredEffect">The effect to the composite</param>
        public void AddChildEffect(EffectSO desiredEffect) {
            EffectSO effectClone = (EffectSO)ScriptableObject.CreateInstance(desiredEffect.GetType());
            effectClone.name = $"{this.name}{desiredEffect.name}Subeffect";
            AssetDatabase.AddObjectToAsset(effectClone,AssetDatabase.GetAssetPath(this));
            subEffects.Add(effectClone);
            AssetDatabase.SaveAssets();
        }
        /// <summary>
        /// Removes a child effect of the composite
        /// </summary>
        /// <param name="childEffect">THe effect to remove from the composite</param>
        public void RemoveChild(EffectSO childEffect) {
            if(subEffects.Remove(childEffect)) {
                return;
            } else {
                Debug.LogWarning($"subeffect {childEffect.name} not part of composite {this.name}");
            }
        }
    }
}