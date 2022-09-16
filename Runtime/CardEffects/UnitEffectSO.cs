using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// The base class of unit effects implemented by the packages end user
    /// </summary>
    public abstract class UnitEffectSO : EffectSO {
        /// <summary>
        /// The logic of the UnitEffectSO
        /// </summary>
        public abstract override void ResolveEffect();
    }
}