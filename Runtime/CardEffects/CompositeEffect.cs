using System.Collections.Generic;

namespace SadSapphicGames.CardEngine
{
    public class CompositeEffect : CardEffect
    {
        private List<CardEffect> subeffects = new List<CardEffect>();
        public CompositeEffect(Card _source) : base(_source) {    
        }
        
        public override void ResolveEffect() {
            foreach (var effect in subeffects) {
                effect.ResolveEffect();   
            }
        }
    }
}