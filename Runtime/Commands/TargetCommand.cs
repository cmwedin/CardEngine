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
        public TargetCommand(ITargeted effectToTarget, IEnumerable<GameObject> validTargets){
            this.effectToTarget = effectToTarget;
            this.validTargets = validTargets;
        }

        public override void Execute() {
            Debug.Log($"Please select a target for {((EffectSO)effectToTarget).name}"); //? this will be displayed on screen eventually
            foreach (var gameObject in validTargets) {
                Selectable selectable = gameObject.AddComponent<Selectable>();
                selectable.SelectionMade += OnSelectionMade;
            }
            while (!selectionMade) { } //? wait until OnSelectionMade gets invoked
            if (selectedTarget == null) { throw new System.Exception($"Failure to select target for {((EffectSO)effectToTarget).name}"); }
            effectToTarget.Target = selectedTarget;
        }

        private void OnSelectionMade(GameObject selection) {
            foreach(var gameObject in validTargets) {
                UnityEngine.Object.Destroy(gameObject.GetComponent<Selectable>());
            }
            selectedTarget = selection;
            selectionMade = true;
            Debug.Log($"{((EffectSO)effectToTarget).name} targeted to {selection.GetComponent<Card>().name}");
        }
    }
}