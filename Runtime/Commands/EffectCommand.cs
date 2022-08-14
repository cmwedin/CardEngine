using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine
{
    public class EffectCommand : Command
    {
        private EffectSO effect;

        public EffectCommand(EffectSO effect) {
            this.effect = effect;
        }

        public override void Execute()
        {
            effect.ResolveEffect();
        }
    }
}