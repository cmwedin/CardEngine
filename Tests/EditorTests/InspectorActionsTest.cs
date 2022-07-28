using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SadSapphicGames.CardEngineEditor;
using SadSapphicGames.CardEngine;
using UnityEditor;
public class InspectorActionsTest : IPrebuildSetup, IPostBuildCleanup{
    //? Directories
    string typesDirectory = CardEngineIO.directories.CardTypes;
    string effectsDirectory = CardEngineIO.directories.Effects;
    string cardsDirectory = CardEngineIO.directories.CardScriptableObjects;
    
    //? Test Assets
    CardSO testCard;
    CompositeEffectSO testCompEffect;

    public void Setup() {
        //? generate the test card and its composite effect 
        CreateCardObject createCardObject = new CreateCardObject();
        createCardObject.CreateCard("TestCard", "Test Card Text");
        
        //? Make sure the type and unit effect databases have atleast one entry
        if(
            TypeDatabaseSO.Instance.GetAllObjectNames().Count == 0 ||
            EffectDatabaseSO.Instance.GetAllObjectNames().Count == 0
        ) {
            throw new System.Exception("There must be at least one entry in the type and effect databases each to test inspector actions");
        }
    }

    public void Cleanup() {
        AssetDatabase.DeleteAsset($"{cardsDirectory}/TestCard");
    }
    [Test]
    public void AddTypeTest(){
        testCard = AssetDatabase.LoadAssetAtPath<CardSO>($"{cardsDirectory}/TestCard/TestCard.asset");
        
        //? Create add card object and an array to use as an argument
        //? we pick a random type to add
        AddTypeObject addTypeObject = new AddTypeObject(testCard);
        List<string> typeNames = TypeDatabaseSO.Instance.GetAllObjectNames();
        bool[] typesToAdd = new bool[typeNames.Count];
        int typeIndex = Random.Range(0,typeNames.Count-1);
        typesToAdd[typeIndex] = true;
        
        //? get what the type to be added is and add it to the card
        TypeSO addedType = TypeDatabaseSO.Instance.GetEntryByName(typeNames[typeIndex]).entrykey;
        addTypeObject.AddTypes(typesToAdd);

        //? Verify that the card has the added type
        Assert.IsTrue(testCard.HasType(addedType));

        //? Verify the subdata is being added appropriately
        Object typeSubdata = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(testCard))[0];
        Assert.AreEqual(expected: typeSubdata, actual: testCard.GetTypeSubdata(addedType));
    }
    [Test]
    public void RemoveTypeTest() {
        testCard = AssetDatabase.LoadAssetAtPath<CardSO>($"{cardsDirectory}/TestCard/TestCard.asset");
        
        //? Create remove type object
        RemoveTypeObject removeTypeObject = new RemoveTypeObject(testCard);

        //? Identify a random type to remove
        bool[] typesToRemove = new bool[testCard.CardTypes.Count];
        int typeIndex = Random.Range(0,testCard.CardTypes.Count -1); //? there should only be one option here
        TypeSO removedType = testCard.CardTypes[typeIndex];
        typesToRemove[typeIndex] = true;

        //? Remove type
        removeTypeObject.RemoveTypes(typesToRemove);
        
        //? verify type has been removed
        Assert.IsFalse(testCard.HasType(removedType));

        //? verify subdata has been removed
        //? if we change this test to involved a card with multiple types we will need to rework this line
        Assert.AreEqual(expected: testCard, actual: AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(testCard))[0]); 
    }
    [Test]
    public void AddEffectTest() {
        //? Load composite effect
        testCompEffect = AssetDatabase.LoadAssetAtPath<CompositeEffectSO>($"{cardsDirectory}/TestCard/TestCardEffect.asset");


        //?create Add effect object and the array argument
        //? again adding a random effect
        AddEffectObject addEffectObject = new AddEffectObject(testCompEffect);
        List<string> effectNames = EffectDatabaseSO.Instance.GetAllObjectNames();
        bool[] effectsToAdd = new bool[effectNames.Count];
        int effectIndex = Random.Range(0, effectNames.Count - 1); 
        EffectSO addedEffect = EffectDatabaseSO.Instance.GetEntryByName(effectNames[effectIndex]).entrykey;
        effectsToAdd[effectIndex] = true;

        //? Add effects to composite
        addEffectObject.AddEffects(effectsToAdd);

        //? verify the effect has been added correctly
        Assert.AreEqual(expected:addedEffect.GetType(), actual: testCompEffect.Subeffects[0].GetType());
        Assert.AreEqual(
            expected: testCompEffect.Subeffects[0],
            actual: AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(testCompEffect))[0]
        );
    }


}
