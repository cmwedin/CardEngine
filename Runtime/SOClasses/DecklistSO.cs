using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine {
    [System.Serializable]
    public struct DecklistEntry {
        public CardSO card;
        public int count;
    }
    
    [CreateAssetMenu(fileName = "DecklistSO", menuName = "SadSapphicGames/CardEngine/DecklistSO", order = 2)]
    public class DecklistSO : ScriptableObject {
        public List<DecklistEntry> deckList;
    }
}