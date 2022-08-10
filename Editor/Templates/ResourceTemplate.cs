using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CardEngine;

public class ResourceTemplate : ResourceSO
{
    public override PayResourceCommand CreatePayResourceCommand(AbstractActor actorToPay, int paymentMagnitude) {
        //? return a new an instance of this resource's payment command with the appropriate magnitude, the package will handle the rest
        
        throw new System.NotImplementedException();
    }
}
