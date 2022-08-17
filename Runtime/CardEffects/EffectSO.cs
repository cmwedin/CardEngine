using System.Collections.Generic;
using UnityEngine;


namespace SadSapphicGames.CardEngine
{
    public abstract class EffectSO : ScriptableObject {
        public virtual AbstractActor Controller { get; set; } //? this is set when a card is played according to the current controller of the card
        public abstract void ResolveEffect();
    }
}