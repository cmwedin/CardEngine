using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SadSapphicGames.CardEngine
{
    public abstract class CardZone : MonoBehaviour {
        public IEnumerable Cards { get; set; }
        public bool CardsDraggable { get; protected set; }
    }
}