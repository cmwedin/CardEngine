using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SadSapphicGames.CardEngine
{
    public interface ICardZone {
        public IEnumerable Cards { get; set; }
        public bool CardsDraggable { get; set; }
    }
}