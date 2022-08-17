using UnityEngine;
using SadSapphicGames.CommandPattern;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SadSapphicGames.CardEngine
{
    public class TargetCommand : Command, IFailable
    {
        private ITargeted effectToTarget;
        bool selectionMade = false;
        GameObject selectedTarget;
        List<GameObject> validTargets = new List<GameObject>();

        public TargetCommand(ITargeted effectToTarget) {
            this.effectToTarget = effectToTarget;
            foreach(var card in effectToTarget.TargetZone.Cards){
                if(card.GetComponent(effectToTarget.TargetType) != null) {
                    validTargets.Add(card.gameObject);
                }
            }
        }
        public TargetCommand(ITargeted effectToTarget, IEnumerable<GameObject> validTargets){
            this.effectToTarget = effectToTarget;
            this.validTargets = validTargets.ToList();
        }

        public async override void Execute() {
            if(validTargets.Count == 0) throw new System.Exception("Attempted to target an effect with no valid targets"); //? this should be impossible if this method is invoked but just incase
            Debug.Log($"Please select a target for {((EffectSO)effectToTarget).name}"); //? this will be displayed on screen eventually
            foreach (var gameObject in validTargets) {
                Selectable selectable = gameObject.AddComponent<Selectable>();
                selectable.SelectionMade += OnSelectionMade;
            }
            while (!selectionMade) {
                await Task.Delay(1);
            }
            if (selectedTarget == null) { throw new System.Exception($"Failure to select target for {((EffectSO)effectToTarget).name}"); }
            effectToTarget.Target = selectedTarget;
        }

        public bool WouldFail()
        {
            return validTargets.Count == 0;
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