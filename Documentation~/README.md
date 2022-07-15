# Card Engine
[![License: CC BY-NC 4.0](https://img.shields.io/badge/License-CC_BY--NC_4.0-lightgrey.svg)](https://creativecommons.org/licenses/by-nc/4.0/)

(a commercial use license will be available in a future paid version of this software)
## Card ECS model
This **in development** unity package provides pre-made architecture for modeling cards in the style of an entity component system. To elaborate consider an example from the popular card game Magic the Gathering (MTG). In this game cards can have a type such as creature, land and sorcery. The exact rules of how these "card types" work isn't relevant for this example, just that they behave differently in the game. While the exact mechanical behavior of such card types can differ widely across games, this type of behavior is common among card games. I will not go into detail for them but other popular examples include Hearthstone and Slay the Spire.  

In this package we model cards in unity through a [Card](https://github.com/cmwedin/CardEngine/blob/main/Runtime/Cards/Card.cs) MonoBehaviour on unity game objects. This MonoBehaviour contains general information all cards have in common, such as a name, image, or description text (more specifically it contains references to TextMeshPro ui components on which to display this information, the data itself is stored in scriptable objects but we will discus those later). Additional card types then have their behaviours represented in further components. Using the example above from MTG, if a cards data has the type "creature" the game object entity would have an additional monobehaviour component added to it representing that type, which would have additional data such as power and toughness.       

I will note here that this package attempts to be as agnostic of systems of the game it might be used in as possible. As such it is expected that the end user will write there own extension wrapping the Card class to suit the needs of their project. This package also currently does not provide any pre-made card types, simply the systems for the end user to attach their own and represent card data.

## Using Card Engine
This package is currently not recommended for use in general projects as it is still heavily in development; however, if you would like to help test the package or see how it will work the following steps will explain how to set it up in a test project.

First you will need to go to the CardEngine menu in the top of the unity editor and select "Initialize" this will open a window prompting you to import essential resources. These resources will be created within your project in the folder "Assets/CardEngine/Config," in this early version of the software it is important you don't move these files. 
Once you have done that, go to the settings submenu in the CardEngine menu and select a directory for the project to store your card types and a directory for it to store your cards. Once you have done that you are ready to begin using the package.

**---- README a work in progress ----**
### Creating your first card type
### Creating your first card
### Using the packages UI and game-state components
