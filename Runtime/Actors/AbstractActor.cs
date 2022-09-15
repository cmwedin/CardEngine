using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// Enum to indicate if an Actor can preform actions
    /// </summary>
    public enum ActState {
        CanAct,
        CannotAct
    }
    public abstract class AbstractActor : MonoBehaviour
    {
        /// <summary>
        /// The Actor's current ActState
        /// </summary>
        public ActState actState {get; private set;}
        /// <summary>
        /// Changes the Actor's ActState
        /// </summary>
        public void ChangeActState() {
            actState++;
        }
        public abstract void InitializeActor();
    }
}
