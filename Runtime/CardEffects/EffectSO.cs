using System.Collections.Generic;
using UnityEngine;


namespace SadSapphicGames.CardEngine
{
[CreateAssetMenu(fileName = "EffectSO", menuName = "CardEngineDevelopment/EffectSO", order = 0)]
    public class EffectSO : ScriptableObject {
        [SerializeReference] List<EffectSO> subEffects;

        public virtual void ResolveEffect() {
            foreach (var effect in subEffects) {
                effect.ResolveEffect();
            }
        }
    }
}