using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SadSapphicGames.CardEngineEditor {
    public static class CardEngineIO {
        public static Directories directories { get => SettingsEditor.ReadSettings().Directories;}
        public static string GetPackagePath() {
            // ? standard package location
            string packagePath = "Packages/com.sadsapphicgames.cardengine";
            if(Directory.Exists(packagePath)) {
                return packagePath;
            } 
            // ? location for package development
            packagePath = "Assets/Packages/CardEngine";
            if (Directory.Exists(packagePath)) {
                return packagePath;
            }
            // ? failed to find
            Debug.LogWarning("Failed to find CardEngine install location, returning null for path");
            return null;
        }

    }
}
