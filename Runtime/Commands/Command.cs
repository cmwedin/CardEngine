namespace SadSapphicGames.CardEngine
{
    public abstract class Command
    {
        public abstract bool Execute();
        public abstract void Undo();
        public abstract void OnFailure();
    }
}