using System;
using UnityEngine;

namespace SadSapphicGames.CardEngine {
    /// <summary>
    /// Scriptable object representing a resource to pay to play a card
    /// </summary>
    public abstract class ResourceSO : ScriptableObject {
        /// <summary>
        /// Creates a command to make an Actor pay the resource
        /// </summary>
        /// <param name="actorToPay">The actor to pay the resource </param>
        /// <param name="paymentMagnitude">the magnitude of the resource to pay</param>
        /// <returns>The command to make the actor pay the resource</returns>
        public abstract PayResourceCommand CreatePayResourceCommand(AbstractActor actorToPay, int paymentMagnitude);
    }
}