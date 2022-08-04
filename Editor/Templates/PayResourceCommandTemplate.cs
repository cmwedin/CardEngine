using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CardEngine;

public class PayResourceCommandTemplate : PayResourceCommand
{
    public override bool Execute()
    {
        throw new System.NotImplementedException();
    }

    //? OnFailure is implemented by the base PayResourceCommand class and will log a warning that the resource was unable to be payed
    //? If you need to do something more specific you can still override the method here

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }
}