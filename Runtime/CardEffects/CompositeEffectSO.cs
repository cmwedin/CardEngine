using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    [CreateAssetMenu(fileName = "CompositeEffectSO", menuName = "CardEngineDevelopment/CompositeEffectSO", order = 0)]
    public class CompositeEffectSO : EffectSO {
        [SerializeField] private List<EffectSO> subEffects = new List<EffectSO>();
        public ReadOnlyCollection<EffectSO> Subeffects { get => subEffects.AsReadOnly();}
        public int ChildrenCount { get => subEffects.Count;}
        public override void ResolveEffect() {
            foreach (var effect in subEffects) {
                effect.ResolveEffect();                
            }
        }
        public void AddChildEffect(EffectSO desiredEffect) {
            EffectSO effectClone = (EffectSO)ScriptableObject.CreateInstance(desiredEffect.GetType());
            effectClone.name = $"{this.name}{desiredEffect.name}Subeffect";
            AssetDatabase.AddObjectToAsset(effectClone,AssetDatabase.GetAssetPath(this));
            subEffects.Add(effectClone);
            AssetDatabase.SaveAssets();
        }
        public void RemoveChild(EffectSO childEffect) {
            if(subEffects.Remove(childEffect)) {
                return;
            } else {
                Debug.LogWarning($"subeffect {childEffect.name} not part of composite {this.name}");
            }
        }
    }
}