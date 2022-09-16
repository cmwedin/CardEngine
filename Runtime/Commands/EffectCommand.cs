using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// A command that performs a EffectSO's resolve action
    /// </summary>
    public class EffectCommand : Command
    {
        /// <summary>
        /// The EffectSO's resolve action to preform
        /// </summary>
        private EffectSO effect;

        /// <summary>
        /// Creates an EffectCommand from a given EffectSO
        /// </summary>
        public EffectCommand(EffectSO effect) {
            this.effect = effect;
        }
        /// <summary>
        /// Preforms the EffectSO's resolve effect
        /// </summary>
        public override void Execute()
        {
            effect.ResolveEffect();
        }
    }
}