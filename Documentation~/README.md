# Card Engine
[![License: CC BY-NC 4.0](https://img.shields.io/badge/License-CC_BY--NC_4.0-lightgrey.svg)](https://creativecommons.org/licenses/by-nc/4.0/)

Credit should be given to Chris Wedin at this time.

(a commercial use license will be available in a future paid version of this software)
# Installation 
To install this package first you must install a separate  package I have developed [CommandPattern](https://github.com/cmwedin/CommandPattern.git). To do this open the Package Manager window, select the plus icon in the top left corner, and select "Add package from git URL" and enter

    https://github.com/cmwedin/CommandPattern.git
This package must be installed separately as unity does not currently support resolving git dependencies automatically. Once that package has been installed, you can install CardEngine by again selecting "Add package from git URL" and entering 

    https://github.com/cmwedin/CardEngine.git
# README disclaimer
as this package is heavily in development frequent updates may make parts of this readme outdated. This is a one person project as of now, so I will not be investing the time to rewrite it every minor update as that time could be spent bringing the package closer to release. I will do my best to identify sections that are no longer in date, but I will not be doing extensive rewrites outside of 0.x... updates. 
## Card ECS model
This **in development** unity package provides pre-made architecture for modeling cards in the style of an entity component system. To elaborate consider an example from the popular card game Magic the Gathering (MTG). In this game cards can have a type such as creature, land and sorcery. The exact rules of how these "card types" work isn't relevant for this example, just that they behave differently in the game. While the exact mechanical behavior of such card types can differ widely across games, this type of behavior is common among card games. I will not go into detail for them but other popular examples include Hearthstone and Slay the Spire.  

In this package we model cards in unity through a [Card](https://github.com/cmwedin/CardEngine/blob/main/Runtime/Cards/Card.cs) MonoBehaviour on unity game objects. This MonoBehaviour contains general information all cards have in common, such as a name, image, or description text (more specifically it contains references to TextMeshPro ui components on which to display this information, the data itself is stored in scriptable objects but we will discus those later). Additional card types then have their behaviours represented in further components. Using the example above from MTG, if a cards data has the type "creature" the game object entity would have an additional monobehaviour component added to it representing that type, which would have additional data such as power and toughness.       

I will note here that this package attempts to be as agnostic of systems of the game it might be used in as possible. As such it is expected that the end user will write there own extension wrapping the Card class to suit the needs of their project. This package also currently does not provide any pre-made card types, simply the systems for the end user to attach their own and represent card data.

## Using Card Engine
This package is currently not recommended for use in general projects as it is still heavily in development; however, if you would like to help test the package or see how it will work the following steps will explain how to set it up in a test project.

First you must add the package to your project (again it is not recommended at this time to use this for a live project). To do this, select the green "code" button above and copy the url for this github repository. Then, go into the package manager in Unity, click on the + icon in the top left side of the window and select add package from git url, and paste the url from this repository. 

Once you have added the package to your project, you will need to go to the CardEngine menu in the top of the unity editor and select "Initialize." This will open a window prompting you to import essential resources. These resources will be created within your project in the folder "Assets/CardEngine/Config," in this early version of the software it is important you don't move these files. 

Once you have done that, go to the settings submenu in the CardEngine menu and select a directory for the project to store its various types of assets. In the future this step may be merged into the initialization window. After you have initialized the project and selected your directories, you are ready to begin using the package. 

### Creating your first card type
To start using the package it is best to first create some of the card types you will need for your project. To do this, select the CardEngine menu, and select the "Create" sub-menu. You will be prompted to enter a name for the type, after which a folder will be generated for the type along with a few new files, give these files a moment to compile and navigate to the folder. A warning will be displayed reminding you to initialize the type scriptable object, but first, lets discus what exactly the files that where just generated are.

First we have the C# script named "TypeName" (if not obvious, "TypeName" will be whatever you entered in the type prompt). This script is defines the monobehaviour component that will be added to entity's who's data identifies them as having this type. This is indicated using the scriptable object of the same name that was also just generated. If this type has additional data associated with it as well (as an example many card games have a "monster"-like type that would have additional data of health and attack), this is defined within the "TypeNameDataSO" class that was just generated.

**as of version 0.2.12 TypeSO's initalize automatically**

Once you have added your needed code to these files (you can also modify them latter if you wish), select the Type scriptable object, and click on the initialize button. This will generate two additional assets and connect them to the scriptable object. The first is a prefab game object with the component defined in "TypeName.cs" attached to it. This is used as a reference for the type scriptable object to be able to easily attach the correct component to entity's that load it into their data. Similarly, an instance of the TypeNameDataSO scriptable object will be created. Again, this is a reference so when this type is added to a card's data it can create a new instance of the appropriate type of scriptable object to store the additional data needed for the card.

At this point, if you are satisfied with the definitions you have provided for the type and its data, you have successfully created your first card type.

### Creating your first card
Now that you have some card types created you are ready to start creating cards. It is worth noting at this point that this is technically creating Card data, as the Card class is specifically the component used to indicate a card entity game object; however, as the majority of what defines how a particular card behaves is contained within its data SO, when we use the phrase "Card" here we are referring to a particular card's data rather than a card game object unless otherwise stated.

Similar to creating a card type, the first step to create a card is to go to the CardEngine/Create menu and select card. Here you will be prompted again to enter a name but also given a field to enter a card description as well. The card name is mandatory, however, the description can be left blank at this point if you wish. Once you have created the card within its folder you will find two files, the card's scriptable object, and its effect scriptable object. We will discuss card effects more later, and for now focus on the data scriptable object.

Within the inspector for a CardSO instance you can edit its fields like is name or description. You will also find a button to add a card type to the card. Selecting this button will open up a popup window where you can select any number of types from the types in the TypeDatabaseSO (found in the CardEngine/Config essential files) to add. Doing this will automatically generate scriptable objects for the subdata from the types selected in the cards folder. These scriptable object will be named CardNameTypeNameData.

**---- README is a work in progress ----**

### Using the packages UI and game-state components

### Creating card effects
Card effects are represented using the composite design pattern with the idea that any effect can eventually be split down into a set of "unit effects" that cannot be divided any further (for example a card who's effect is to deal damage to 3 different targets could be split into 3 instances of the "damage" unit effect). If you are already familiar with the composite design pattern you will likely recognize these as the "leaves" of a particular effect, or "composite." This package will require the end user to define the possible unit effects in their game themselves and provide them the tools to mix these effects together to create a variety of composite effects. 

