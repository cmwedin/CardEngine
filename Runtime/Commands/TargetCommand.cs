using UnityEngine;
using SadSapphicGames.CommandPattern;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SadSapphicGames.CardEngine
{
    /// <summary>
    /// Async command to target a ITargeted effect
    /// </summary>
    public class TargetCommand : AsyncCommand, IFailable
    {
        /// <summary>
        /// The effect that is being targeted
        /// </summary>
        private ITargeted effectToTarget;
        /// <summary>
        /// Has a target been selected
        /// </summary>
        bool selectionMade = false;
        /// <summary>
        /// What the selected target was
        /// </summary>
        GameObject selectedTarget;
        /// <summary>
        /// What the valid targets are
        /// </summary>
        List<GameObject> validTargets = new List<GameObject>();
        /// <summary>
        /// Creates a TargetCommand to target a given effect
        /// </summary>
        public TargetCommand(ITargeted effectToTarget) {
            this.effectToTarget = effectToTarget;
            foreach(var card in effectToTarget.TargetZone.Cards){
                if(card.GetComponent(effectToTarget.TargetType) != null) {
                    validTargets.Add(card.gameObject);
                }
            }
        }
        /// <summary>
        /// Create a TargetCommand to target a given effect with a given collection of valid targets
        /// </summary>
        public TargetCommand(ITargeted effectToTarget, IEnumerable<GameObject> validTargets){
            this.effectToTarget = effectToTarget;
            this.validTargets = validTargets.ToList();
        }
        /// <summary>
        /// Targets the command on the player's selection
        /// </summary>
        /// <exception cref="System.Exception">Thrown if the effect doesnt have any valid targets, this should never happen</exception>
        public async override Task ExecuteAsync() {
            if(validTargets.Count == 0) throw new System.Exception("Attempted to target an effect with no valid targets"); //? this should be impossible if this method is invoked but just incase
            Debug.Log($"Please select a target for {((EffectSO)effectToTarget).name}"); //? this will be displayed on screen eventually
            foreach (var gameObject in validTargets) {
                Selectable selectable = gameObject.AddComponent<Selectable>();
                selectable.SelectionMade += ((selection) => {
                    foreach(var gameObject in validTargets) {
                        UnityEngine.Object.Destroy(gameObject.GetComponent<Selectable>());
                    }
                    selectedTarget = selection;
                    selectionMade = true;
                    Debug.Log($"{((EffectSO)effectToTarget).name} targeted to {selection.GetComponent<Card>().name}");
                });
            }
            while (!selectionMade) {
                await Task.Delay(1);
            }
            if (selectedTarget == null) { throw new System.Exception($"Failure to select target for {((EffectSO)effectToTarget).name}"); }
            effectToTarget.Target = selectedTarget;
        }
        /// <summary>
        /// True if there are no valid targets
        /// </summary>
        /// <returns></returns>
        public bool WouldFail()
        {
            return validTargets.Count == 0;
        }

    }
}