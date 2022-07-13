namespace SadSapphicGames.CardEngine
{
    public abstract class CardEffect
    {
        private Card source;

        protected CardEffect(Card _source) {
            source = _source;
        }

        public abstract void ResolveEffect();        
    }
}