# Card Scriptable Object
This class defines the scriptable object that stores the data for a new card you want to add to your game. To create a card simply right click in the project view in the unity editor, mouse over create, SadSapphicGames, CardEngine, then select CardSO. This class is written with the intention that the end user will extend it as needed for their own game; however, which can be done by inheriting from CardSO. You can define how to create your own custom CardSO by placing the `[CreateAssetMenu(fileName = "YourFileName", menuName = "Your/Directory/Structure/YourFileName")]` attribute above the name of your custom CardSO class (note that the directory structure here is simply a way of organizing your create asset menu and is unrelated to your project's actual directory structure). Nonetheless, we provide the asset menu field to create an unmodified CardSO for prototyping purposes.

## Scriptable Object Properties
The default CardSO has the following properties that can be set via the Unity Editor Inspector (if you are unfamiliar with scriptable objects it is worth noting these properties can be changed in play mode and the changes will persist unlike the properties of a monobehaviour). The purpose of the properties are all straightforward from their names.

- Card Name
- Card Sprite
- Card Effect

Common elements of card game such as a resource mechanic are not implemented in this base scriptable object as they can vary heavily depending on the needs of your game.