using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SadSapphicGames.CardEngine {
    public class CommandManager : MonoBehaviour
    {
        public static CommandManager instance;
        public bool freezeCommandExecution;
        private Stack<Command> executedCommands = new();
        private Queue<Command> queuedCommands = new(); 
        
        public void QueueCommand(Command command) {
            queuedCommands.Enqueue(command);
        }
        private void ExecuteNextCommand() { 
            if(queuedCommands.TryDequeue(out Command nextCommand)) {
                nextCommand.Execute();
                executedCommands.Push(nextCommand);
            }
        }
        public void UndoPreviousCommand() {
            if(executedCommands.TryPop(out Command previousCommand)) {
                freezeCommandExecution = true;
                previousCommand.Undo();
                freezeCommandExecution = false;
            }
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
            
        }

        // Update is called once per frame
        void Update() {
            if(!freezeCommandExecution && queuedCommands.Count > 0) {
                ExecuteNextCommand();
            }
        }
    }
}
