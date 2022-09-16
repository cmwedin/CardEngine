using UnityEngine;
using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine {
    /// <summary>
    /// An abstract command to make an actor pay a resource
    /// </summary>
    public abstract class PayResourceCommand : Command, IFailable {
        /// <summary>
        /// The magnitude of the payment to make
        /// </summary>
        protected int paymentMagnitude;
        /// <summary>
        /// Constructs the PayResourceCommand with a given magnitude
        /// </summary>
        public PayResourceCommand(int paymentMagnitude) {
            this.paymentMagnitude = paymentMagnitude;
        }
        /// <summary>
        /// Can the payment be made
        /// </summary>
        /// <returns>True if the payment cannot be made</returns>
        public abstract bool WouldFail();
    }
}