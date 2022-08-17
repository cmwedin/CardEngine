using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    public interface ITargeted
    {
        public Type TargetType { get; }
        public CardZone TargetZone { get; }
        public GameObject Target { get; set; }
    }
}
