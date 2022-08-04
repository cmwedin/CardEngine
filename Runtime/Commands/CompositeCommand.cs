using System;
using System.Collections.Generic;

namespace SadSapphicGames.CardEngine
{
    public abstract class CompositeCommand : Command {
        protected List<Command> subcommands = new List<Command>();

        public override bool Execute() {
            List<Command> executedSubcommands = new();
            foreach (var command in subcommands) {
                if (command.Execute()) {
                    executedSubcommands.Add(command);
                } else {
                    foreach (var executedCommand in executedSubcommands) {
                        executedCommand.Undo();
                    }
                    return false;
                }
            }
            return true;
        }
        public override void Undo() {
            foreach (var command in subcommands) {
                command.Undo();
            }
        }
    }
}