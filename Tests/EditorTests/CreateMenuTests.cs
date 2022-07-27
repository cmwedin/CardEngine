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
    string typesDirectory = CardEngineIO.directories.CardTypes;
    CreateCardTypeObject createCardTypeObject = new CreateCardTypeObject();
    string testTypeName = "TestType";

    public void Cleanup()
    {
        AssetDatabase.DeleteAsset($"{typesDirectory}/{testTypeName}");
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
}
