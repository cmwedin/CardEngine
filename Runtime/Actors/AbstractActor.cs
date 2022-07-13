using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine
{
    public enum ActState {
        CanAct,
        CannotAct
    }
    public abstract class AbstractActor : MonoBehaviour
    {
        public ActState actState {get; private set;}
        public void ChangeActState() {
            actState++;
        }
        public abstract void InitializeActor();
        // Start is called before the first frame update
        void Start()
        {
            
        }


        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
