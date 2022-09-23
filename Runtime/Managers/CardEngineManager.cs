using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SadSapphicGames.CardEngine
{   
    /// <summary>
    /// Enum for the possible game states
    /// </summary>
    public enum GameState {
        initialize,
        playerTurn,
        enemyTurn,
        victory,
        defeat
    }

    /// <summary>
    /// Singleton manager for instantiating cards and managing game state
    /// </summary>
    public class CardEngineManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance
        /// </summary>
        [HideInInspector] public static CardEngineManager instance;
        /// <summary>
        /// Prefab to use when instantiating cards
        /// </summary>
        [SerializeField] private GameObject cardPrefab;
        /// <summary>
        /// Debug option to show cards regardless of their zone
        /// </summary>
        [SerializeField] private bool showAllCards;

        /// <summary>
        /// Property to get the height of the card prefab
        /// </summary>
        public float cardPrefabHeight { get => cardPrefab.GetComponent<RectTransform>().rect.height;}
        /// <summary>
        /// Property to get the width of the card prefab
        /// </summary>
        public float cardPrefabWidth { get => cardPrefab.GetComponent<RectTransform>().rect.width;}
        /// <summary>
        /// Inspector field to set the player
        /// </summary>
        [SerializeField] private Player player;
        /// <summary>
        /// List of the non-player actors 
        /// </summary>
        [SerializeField] private List<AbstractActor> nonPlayerActors = new List<AbstractActor>();       
        /// <summary>
        /// Combines the non play actor list with the player
        /// </summary>
        private IEnumerable<AbstractActor> AllActors {get => nonPlayerActors.Append<AbstractActor>(player);}

        /// <summary>
        /// Instantiates a card into a given zone with given data and a given controller
        /// </summary>
        public void InstantiateCard(CardZone targetZone, CardSO targetData,AbstractActor controller) {
            GameObject cardObject = Instantiate(cardPrefab);
            cardObject.transform.SetParent(targetZone.transform,false);
            cardObject.GetComponent<Card>().LoadData(targetData);
            cardObject.name = cardObject.GetComponent<Card>().CardName;
            cardObject.GetComponent<Card>().SetController(controller);
            if(!showAllCards) cardObject.GetComponent<Card>().IsVisible = false;
            targetZone.AddCard(cardObject.GetComponent<Card>());
        }
        /// <summary>
        /// Validates a dropping a card by verifying it was dropped outside its current zone
        /// </summary>
        /// <param name="card">The card that was dropped</param>
        /// <param name="dropPointerData">Where the card was dropped</param>
        public void ValidatePlay(Card card,PointerEventData dropPointerData ) {
            if(card.GetController().actState == ActState.CannotAct) return;
            Vector3[] cancelRectCorners = new Vector3[4];
            card.CurrentZone.GetComponent<RectTransform>().GetWorldCorners(cancelRectCorners);
            float[] cancelXBounds = new float[2] {cancelRectCorners[0].x,cancelRectCorners[2].x};
            float[] cancelYBounds = new float[2] {cancelRectCorners[0].y,cancelRectCorners[2].y};

            // Debug.Log($"Cancel zone for card is {card.CurrentZone.name}, ({cancelXBounds[0]},{cancelXBounds[1]}) by ({cancelYBounds[0]},{cancelYBounds[1]})");
            // Vector2 dropPos = dropPointerData.position;
            Vector2 cardPos = card.gameObject.transform.position;
            // Debug.Log($"card dropped at {dropPos.x},{dropPos.y}");

            if ((cancelXBounds[0]  <= cardPos.x + cardPrefabHeight / 2) && (cardPos.x - cardPrefabHeight / 2 <= cancelXBounds[1])) {
                if ((cancelYBounds[0]  <= cardPos.y + cardPrefabWidth / 2) && (cardPos.y - cardPrefabWidth / 2 <= cancelYBounds[1])) {
                    Debug.Log($"Cast of {card.CardName} canceled");
                    return;
                }
            }
            Debug.Log($"Cast of card {card.name} detected");
            CommandManager.instance.QueueCommand(new PlayCardCommand(card,ZoneManager.instance.sharedZones.stagingZone));
        }
        /// <summary>
        /// Sets the singleton instance of the manager
        /// </summary>
        private void Awake() {
            if(instance != null && instance != this) {
                Destroy(this);
            } else {
                instance = this;
            }
        }
        /// <summary>
        /// Initializes all of the actors set in the inspector
        /// </summary>
        void Start()
        {
            foreach (var actor in AllActors)
            {
                actor.InitializeActor();
            }
        }
    }
}
