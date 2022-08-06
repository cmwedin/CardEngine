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
            if(card.GetController().actState == ActState.CannotAct) return;
            Vector3[] cancelRectCorners = new Vector3[4];
            card.CurrentZone.GetComponent<RectTransform>().GetWorldCorners(cancelRectCorners);
            float[] cancelXBounds = new float[2] {cancelRectCorners[0].x,cancelRectCorners[2].x};
            float[] cancelYBounds = new float[2] {cancelRectCorners[0].y,cancelRectCorners[2].y};

            // Debug.Log($"Cancel zone for card is {card.CurrentZone.name}, ({cancelXBounds[0]},{cancelXBounds[1]}) by ({cancelYBounds[0]},{cancelYBounds[1]})");
            Vector2 dropPos = dropPointerData.position;
            // Debug.Log($"card dropped at {dropPos.x},{dropPos.y}");

            if ((cancelXBounds[0]  <= dropPos.x) && (dropPos.x <= cancelXBounds[1])) {
                if ((cancelYBounds[0]  <= dropPos.y) && (dropPos.y <= cancelYBounds[1])) {
                    Debug.Log($"Cast of {card.CardName} canceled");
                    return;
                }
            }
            Debug.Log($"Cast of card {card.name} detected");
            CommandManager.instance.QueueCommand(new PlayCardCommand(card,ZoneManager.instance.stagingZone));
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
