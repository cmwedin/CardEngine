using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{   
    public enum GameState {
        initialize,
        playerTurn,
        enemyTurn,
        victory,
        defeat
    }
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private DeckZone playerDeckZone;
        [SerializeField] private DecklistSO playerDecklist;

        [HideInInspector] public static GameManager instance;

        public void InstantiateCard(CardZone targetZone, CardSO targetData) {
            GameObject cardObject = Instantiate(cardPrefab);
            cardObject.transform.SetParent(targetZone.transform,false);
            cardObject.GetComponent<Card>().LoadData(targetData);
        }
        private void Awake() {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
           playerDeckZone.LoadDecklist(playerDecklist); 
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
