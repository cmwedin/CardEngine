using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SadSapphicGames.CommandPattern;

namespace SadSapphicGames.CardEngine {
    /// <summary>
    /// Singleton manager that wraps a CommandStream
    /// </summary>
    public class CommandManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the manager
        /// </summary>
        public static CommandManager instance;
        /// <summary>
        /// Stops the manager from executing commands from the internal CommandStream
        /// </summary>
        public bool freezeCommandExecution;
        /// <summary>
        /// The internal CommandStream
        /// </summary>
        private CommandStream commandStream = new CommandStream();
        /// <summary>
        /// Exposes the internal CommandStream's QueueCommand method
        /// </summary>
        public void QueueCommand(Command command) {
            commandStream.QueueCommand(command);
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
        /// Executes Commands from the CommandStream unless freezeCommandExecution is true
        /// </summary>
        void Update() {
            if(!freezeCommandExecution) {
                commandStream.TryExecuteNext();
            }
        }
    }
}
