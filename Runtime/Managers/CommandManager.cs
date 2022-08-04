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
                if(nextCommand.Execute()) {
                    executedCommands.Push(nextCommand);
                    return;
                } else {
                    nextCommand.OnFailure();
                }
            }
        }
        public void UndoPreviousCommand() {
            if(executedCommands.TryPop(out Command previousCommand)) {
                freezeCommandExecution = true;
                previousCommand.Undo();
                freezeCommandExecution = false;
            }
        }
        public void UndoUntilCommand(Command stopAtCommand, bool inclusive = false) {
            if(!executedCommands.Contains(stopAtCommand)) {
                throw new System.ArgumentException("Argument not contained in previously executed commands");
            }
            freezeCommandExecution = true;
            while(executedCommands.TryPop(out Command previousCommand)) {
                if(previousCommand != stopAtCommand) {
                    previousCommand.Undo();
                } else { //? we have reached the command to stop at
                    if(!inclusive) { //? if we don't what to undo the argument as well put it back on the executed stack
                        executedCommands.Push(previousCommand);
                    } else { //? otherwise undo it
                        previousCommand.Undo();
                    }
                    //? break out of the loop
                    break;
                }
            }
            freezeCommandExecution = false;
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
