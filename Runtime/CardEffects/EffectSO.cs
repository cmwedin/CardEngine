using System.Collections.Generic;
using UnityEngine;


namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// Base abstract class of a EffectSO
    /// </summary>
    public abstract class EffectSO : ScriptableObject {
        /// <summary>
        /// The controller of the effect
        /// </summary>
        public virtual AbstractActor Controller { get; set; } //? this is set when a card is played according to the current controller of the card
        /// <summary>
        /// Preform the effect
        /// </summary>
        public abstract void ResolveEffect();
    }
}