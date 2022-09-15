using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// An interface to indicate an effect is targeted
    /// </summary>
    public interface ITargeted
    {
        /// <summary>
        /// The types that can be targeted
        /// </summary>
        public Type TargetType { get; }
        /// <summary>
        /// The zone that the target can be selected from
        /// </summary>
        public CardZone TargetZone { get; }
        /// <summary>
        /// The target of the effect
        /// </summary>
        public GameObject Target { get; set; }
    }
}
