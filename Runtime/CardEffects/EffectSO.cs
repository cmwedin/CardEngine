using System.Collections.Generic;
using UnityEngine;


namespace SadSapphicGames.CardEngine
{
    public abstract class EffectSO : ScriptableObject {
        public abstract void ResolveEffect();
    }
}