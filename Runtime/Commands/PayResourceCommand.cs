using UnityEngine;

namespace SadSapphicGames.CardEngine {
    public abstract class PayResourceCommand : Command
    {
        public override void OnFailure() {
            Debug.LogWarning("Unable to pay resource");
        }
    }
}