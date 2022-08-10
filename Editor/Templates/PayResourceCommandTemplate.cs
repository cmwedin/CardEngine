using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CardEngine;
using SadSapphicGames.CommandPattern;

public class PayResourceCommandTemplate : PayResourceCommand
{
    public PayResourceCommandTemplate(int paymentMagnitude) : base(paymentMagnitude) {
        //? You can take other information into this constructor as well but you at least need how much of this resource is being payed
    }

    public override void Execute() {
        //? this is where you put the actual logic of paying the resource
        //? payment of one type of resource should have no effect on an actors ability to pay for a different type of resource
        throw new System.NotImplementedException();
    }

    public override bool WouldFail() {
        //? if the paymentMagnitude of the resource cannot be paid in full return true
        throw new System.NotImplementedException();
    }
}