using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine {
    /// <summary>
    /// An entry in the decklist as a struct 
    /// </summary>
    [System.Serializable] public struct DecklistEntry {
        /// <summary>
        /// The card data
        /// </summary>
        public CardSO card;
        /// <summary>
        /// The number of copies of the card
        /// </summary>
        public int count;
    }

    /// <summary>
    /// A scriptable object to store a decklist
    /// </summary>
    [CreateAssetMenu(fileName = "DecklistSO", menuName = "SadSapphicGames/CardEngine/DecklistSO", order = 2)] public class DecklistSO : ScriptableObject {
        /// <summary>
        /// The entries in the decklist
        /// </summary>
        public List<DecklistEntry> deckList;
    }
}