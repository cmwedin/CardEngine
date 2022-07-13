namespace SadSapphicGames.CardEngine
{
    [System.Serializable]
    public abstract class UnitEffect : CardEffect
    {
        public UnitEffect(Card _source) : base(_source)
        {
        }

        public override void ResolveEffect() {
        }
    }
}