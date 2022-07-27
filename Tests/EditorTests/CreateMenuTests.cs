using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SadSapphicGames.CardEngineEditor;
using SadSapphicGames.CardEngine;
using UnityEditor;

// ! MAKE SURE YOU RUN ALL TESTS - later tests use assets created my earlier ones
public class CreateMenuTests : IPostBuildCleanup
{
    //? Directories
    string typesDirectory = CardEngineIO.directories.CardTypes;
    string effectsDirectory = CardEngineIO.directories.Effects;
    string cardsDirectory = CardEngineIO.directories.CardScriptableObjects;

    //? creator objects
    CreateCardTypeObject createCardTypeObject = new CreateCardTypeObject();
    CreateUnitEffectObject createUnitEffectObject = new CreateUnitEffectObject();
    CreateCardObject createCardObject = new CreateCardObject();



    //? Test asset names
    string testTypeName = "TestType";
    string testEffectName = "TestEffect";
    string testCardName = "TestCard";

    public void Cleanup() {
        AssetDatabase.DeleteAsset($"{typesDirectory}/{testTypeName}");
        AssetDatabase.DeleteAsset($"{effectsDirectory}/{testEffectName}");
        AssetDatabase.DeleteAsset($"{cardsDirectory}/{testCardName}");
    }

    [UnityTest, Order(0)]
    public IEnumerator CreateCardTypeTest() {
        //? create a card type
        createCardTypeObject.CreateCardType(testTypeName);
        yield return new RecompileScripts();

        //? initialize the type and load relevant assets 
        createCardTypeObject.InitializeType(testTypeName);
        TypeSO testType = AssetDatabase.LoadAssetAtPath<TypeSO>($"{typesDirectory}/{testTypeName}/{testTypeName}.asset");
        TypeDataSO testTypeData = AssetDatabase.LoadAssetAtPath<TypeDataSO>($"{typesDirectory}/{testTypeName}/{testTypeName}DataSORef.asset");
        CardType testTypeComponent = AssetDatabase.LoadAssetAtPath<CardType>($"{typesDirectory}/{testTypeName}/{testTypeName}.prefab");

        //? Verify all assets where found;
        Assert.IsNotNull(testType); 
        Assert.IsNotNull(testTypeData);
        Assert.IsNotNull(testTypeComponent); 

        //? Verify TypeSO had reference set correctly
        Assert.AreEqual(expected: testTypeData, actual: testType.TypeDataReference);
        Assert.AreEqual(expected: testTypeComponent.GetType(), actual: testType.typeComponent);
    }
    [UnityTest, Order(1)]
    public IEnumerator CreateUnitEffectTest() {
        //? create a unit effect
        createUnitEffectObject.CreateUnitEffect(testEffectName);
        yield return new RecompileScripts();

        //? initialize the effect and load its asset
        createUnitEffectObject.InitializeEffect(testEffectName);
        UnitEffectSO testEffect = AssetDatabase.LoadAssetAtPath<UnitEffectSO>($"{effectsDirectory}/{testEffectName}/{testEffectName}.asset");

        //? verify the effect was found
        Assert.IsNotNull(testEffect);
    }
    [Test, Order(2)]
    public void CreateCardTest(){
        //? Create the card
        createCardObject.CreateCard(testCardName,"test card text");

        //? Load the card
        CardSO testCard = AssetDatabase.LoadAssetAtPath<CardSO>($"{cardsDirectory}/{testCardName}/{testCardName}.asset");
        CompositeEffectSO testCardEffect = AssetDatabase.LoadAssetAtPath<CompositeEffectSO>($"{cardsDirectory}/{testCardName}/{testCardName}Effect.asset");

        //? Verify the card was found
        Assert.IsNotNull(testCard);
        Assert.IsNotNull(testCardEffect);

        //? Verify fields where assigned properly
        Assert.AreEqual(expected:testCardName, actual: testCard.CardName);
        Assert.AreEqual(expected:"test card text", actual: testCard.CardText);
    }

//! The following tests are probably bad practice as they require the previous test to have been run first
//! however its easier to test it like this as the alternative is generating a new test card for each test
//! which couples the functionality we are testing here to the functionality of the create card menu

    [Test, Order(3)]
    public void AddTypeTest(){
        //? Load card
        CardSO testCard = AssetDatabase.LoadAssetAtPath<CardSO>($"{cardsDirectory}/{testCardName}/{testCardName}.asset");
        
        //? Create add card object and an array to use as an argument
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
        Object typeSubdata = AssetDatabase.LoadAllAssetsAtPath($"{cardsDirectory}/{testCardName}/{testCardName}.asset")[0];
        Assert.AreEqual(expected: typeSubdata, actual: testCard.GetTypeSubdata(addedType));
    }

    [Test, Order(4)]
    public void RemoveTypeTest() {
        //? Load card
        CardSO testCard = AssetDatabase.LoadAssetAtPath<CardSO>($"{cardsDirectory}/{testCardName}/{testCardName}.asset");
        
        //? Create remove type object
        RemoveTypeObject removeTypeObject = new RemoveTypeObject(testCard);

        //? Identify the type to remove
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
        Assert.AreEqual(expected: testCard, actual: AssetDatabase.LoadAllAssetsAtPath($"{cardsDirectory}/{testCardName}/{testCardName}.asset")[0]); 
    }
}
