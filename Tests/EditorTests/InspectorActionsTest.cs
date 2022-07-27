using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SadSapphicGames.CardEngineEditor;
using SadSapphicGames.CardEngine;
using UnityEditor;
public class InspectorActionsTest : IPrebuildSetup, IPostBuildCleanup{
    CardSO testCard;
    CompositeEffectSO testCompEffect;

    public void Setup() {
        //? generate the test card and its composite effect 
        //? as well as a test type and test unit effect
        //? to make sure both databases have at least one entry
        throw new System.NotImplementedException();
    }

    public void Cleanup() {
        throw new System.NotImplementedException();
    }
    [Test]
    public void RemoveTypeTest() {

        
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
        Assert.AreEqual(expected: testCard, actual: AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(testCard))[0]); 
    }
    [Test]
    public void AddEffectTest() {
        //? Load composite effect


        //?create Add effect object and the array argument
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
