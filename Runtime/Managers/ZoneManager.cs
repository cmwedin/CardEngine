using UnityEngine;
    namespace SadSapphicGames.CardEngine {
    public class ZoneManager : MonoBehaviour {
        public static ZoneManager instance;
        public StagingZone stagingZone;

        private void Awake() {
            if(instance != null && instance != this) {
                Destroy(this);
            } else {
                instance = this;
            }
        }
    }
}