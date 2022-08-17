using UnityEngine;
using SadSapphicGames.CommandPattern;
using System.Collections.Generic;

namespace SadSapphicGames.CardEngine
{
    public class TargetCommand : Command
    {
        private ITargeted effectToTarget;
        bool selectionMade = false;
        GameObject selectedTarget;
        IEnumerable<GameObject> validTargets;

        public TargetCommand(ITargeted effectToTarget) {
            this.effectToTarget = effectToTarget;
            validTargets = (GameObject[])GameObject.FindObjectsOfType(effectToTarget.TargetType);
        }

        public override void Execute() {
            foreach (var gameObject in validTargets) {
            }
            while(!selectionMade) { //? wait until OnSelectionMade gets invoked
            }
        }

        private void OnSelectionMade(GameObject selection) {
            foreach(var gameObject in validTargets) {
            }
            selectedTarget = selection;
            selectionMade = true;
        }
    }
}