using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CardEngine;
using SadSapphicGames.CommandPattern;

/// <summary>
/// This Command will be executed to make an Actor pay this Resource
/// </summary>
public class PayResourceCommandTemplate : PayResourceCommand
{
    /// <summary>
    /// Creates a payment command for this resource with a given magnitude 
    /// </summary>
    /// <param name="paymentMagnitude">The magnitude of the payment for the Actor to make</param>
    public PayResourceCommandTemplate(int paymentMagnitude) : base(paymentMagnitude) {
        //? You can take other information into this constructor as well but you at least need how much of this resource is being payed
    }
    /// <summary>
    /// Invokes when this command is executed, this is where the logic of paying the resource should be placed
    /// </summary>
    public override void Execute() {
        //? this is where you put the actual logic of paying the resource
        //? payment of one type of resource should have no effect on an actors ability to pay for a different type of resource
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// Indicates the Actor will be unable to pay the resource
    /// </summary>
    /// <returns>True if the resource cannot be payed</returns>
    public override bool WouldFail() {
        //? if the paymentMagnitude of the resource cannot be paid in full return true
        throw new System.NotImplementedException();
    }
}