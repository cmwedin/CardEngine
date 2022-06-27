using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SadSapphicGames.CardEngine
{
    public abstract class CardZone : MonoBehaviour {
        public List<Card> Cards { get; set; }
        public bool CardsDraggable { get; protected set; }
        private void Awake() {
            Cards = new List<Card>();
        }
    }
}