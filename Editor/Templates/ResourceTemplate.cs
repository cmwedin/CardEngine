using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CardEngine;

/// <summary>
/// This Scriptable object can be added to a CardSO to indicate playing it requires the user to this resource
/// </summary>
public class ResourceTemplate : ResourceSO
{
    /// <summary>
    /// Creates and returns an instance of this resources PayResourceCommand
    /// </summary>
    /// <param name="actorToPay">The actor to make pay the resource</param>
    /// <param name="paymentMagnitude">The magnitude of the resource which should be payed</param>
    /// <returns>The created instance of this resources PayResourceCommand</returns>
    public override PayResourceCommand CreatePayResourceCommand(AbstractActor actorToPay, int paymentMagnitude) {
        //? return a new an instance of this resource's payment command with the appropriate magnitude, the package will handle the rest
        
        throw new System.NotImplementedException();
    }
}
