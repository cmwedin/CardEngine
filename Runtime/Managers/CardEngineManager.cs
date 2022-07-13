using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SadSapphicGames.CardEngine
{   
    public enum GameState {
        initialize,
        playerTurn,
        enemyTurn,
        victory,
        defeat
    }

    public class CardEngineManager : MonoBehaviour
    {
        [HideInInspector] public static CardEngineManager instance;

        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private bool showAllCards;


        public float cardPrefabHeight { get => cardPrefab.GetComponent<RectTransform>().rect.height;}
        public float cardPrefabWidth { get => cardPrefab.GetComponent<RectTransform>().rect.width;}

        [SerializeField] private Player player;
        [SerializeField] private List<AbstractActor> nonPlayerActors = new List<AbstractActor>();       
        private IEnumerable<AbstractActor> AllActors {get => nonPlayerActors.Append<AbstractActor>(player);}

        public void InstantiateCard(CardZone targetZone, CardSO targetData,AbstractActor controller) {
            GameObject cardObject = Instantiate(cardPrefab);
            cardObject.transform.SetParent(targetZone.transform,false);
            cardObject.GetComponent<Card>().LoadData(targetData);
            cardObject.name = cardObject.GetComponent<Card>().CardName;
            cardObject.GetComponent<Card>().SetController(controller);
            if(!showAllCards) cardObject.GetComponent<Card>().IsVisible = false;
            targetZone.AddCard(cardObject.GetComponent<Card>());
        }
        public void ValidatePlay(Card card,PointerEventData dropPointerData ) {

        }
        private void Awake() {
            if(instance != null && instance != this) {
                Destroy(this);
            } else {
                instance = this;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            foreach (var actor in AllActors)
            {
                actor.InitializeActor();
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
