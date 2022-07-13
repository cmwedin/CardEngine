using System.Collections.Generic;
using UnityEngine;


namespace SadSapphicGames.CardEngine
{
[CreateAssetMenu(fileName = "EffectSO", menuName = "CardEngineDevelopment/EffectSO", order = 0)]
    public class EffectSO : ScriptableObject {
        [SerializeField] List<UnitEffect> unitEffects;
    }
}