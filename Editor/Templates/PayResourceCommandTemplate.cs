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

    //? On failure is implemented by the base PayResourceCommand class and will log a warning that the resource was unable to be payed

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }
}