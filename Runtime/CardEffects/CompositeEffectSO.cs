using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    [CreateAssetMenu(fileName = "CompositeEffectSO", menuName = "CardEngineDevelopment/CompositeEffectSO", order = 0)]
    public class CompositeEffectSO : EffectSO {
        [SerializeReference] List<EffectSO> subEffects = new List<EffectSO>();
        public override void ResolveEffect() {
            foreach (var effect in subEffects) {
                effect.ResolveEffect();                
            }
        }
    }
}