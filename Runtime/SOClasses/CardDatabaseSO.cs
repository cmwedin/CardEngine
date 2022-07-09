using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using SadSapphicGames.CardEngine;

namespace SadSapphicGames.CardEngineEditor
{
    
    [CreateAssetMenu(fileName = "CardDatabase", menuName = "SadSapphicGames/CardEngine/Databases/CardDatabase")]
    public class CardDatabaseSO : DatabaseSO<CardSO> {
        
    }
}