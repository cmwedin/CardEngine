using UnityEngine;

namespace SadSapphicGames.CardEngine
{

[CreateAssetMenu(fileName = "UnitEffectSO", menuName = "CardEngineDevelopment/UnitEffectSO", order = 0)]
public abstract class UnitEffectSO : EffectSO {
    public abstract override void ResolveEffect();
}
}