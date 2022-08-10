using UnityEngine;
using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine {
    public abstract class PayResourceCommand : Command, IFailable {
        protected int paymentMagnitude;
        public PayResourceCommand(int paymentMagnitude) {
            this.paymentMagnitude = paymentMagnitude;
        }
        
        public abstract bool WouldFail();
    }
}