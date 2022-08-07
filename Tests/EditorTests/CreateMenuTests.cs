using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SadSapphicGames.CardEngineEditor;
using SadSapphicGames.CardEngine;
using UnityEditor;

public class CreateMenuTests : IPostBuildCleanup
{
    //? Directories
    string typesDirectory = CardEngineIO.directories.CardTypes;
    string effectsDirectory = CardEngineIO.directories.Effects;
    string cardsDirectory = CardEngineIO.directories.CardScriptableObjects;
    string resourcesDirectory = CardEngineIO.directories.Resources;

    //? creator objects
    CreateCardTypeObject createCardTypeObject = new CreateCardTypeObject();
    CreateUnitEffectObject createUnitEffectObject = new CreateUnitEffectObject();
    CreateCardObject createCardObject = new CreateCardObject();
    CreateResourceObject createResourceObject = new CreateResourceObject();

    //? Test asset names
    string testTypeName = "TestType";
    string testEffectName = "TestEffect";
    string testCardName = "TestCard2";
    string testResourceName = "TestResource";

    public void Cleanup() {
        AssetDatabase.DeleteAsset($"{typesDirectory}/{testTypeName}");
        AssetDatabase.DeleteAsset($"{effectsDirectory}/{testEffectName}");
        AssetDatabase.DeleteAsset($"{cardsDirectory}/{testCardName}");
        AssetDatabase.DeleteAsset($"{resourcesDirectory}/{testResourceName}");
    }

    [UnityTest]
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
    [UnityTest]
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
    [UnityTest]
    public IEnumerator CreateResourceTest() {
        //? Create a resource
        createResourceObject.CreateResource(testResourceName);
        yield return new RecompileScripts();

        //? initialize resource - load its asset
        createResourceObject.InitializeResource(testResourceName);
        ResourceSO testResource = AssetDatabase.LoadAssetAtPath<ResourceSO>($"{resourcesDirectory}/{testResourceName}/{testResourceName}.asset");

        //? verify the resource was found
        Assert.IsNotNull(testResource);
    }
    [Test]
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
        Assert.AreEqual(expected:testCardEffect, actual: testCard.CardEffect);
    }  
}
