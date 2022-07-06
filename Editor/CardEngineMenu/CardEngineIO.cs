using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SadSapphicGames.CardEngineEditor {
    public static class CardEngineIO {
        private static Directories directories;
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
            Debug.LogWarning("Failed to find CardEngine instal location, returning null for path");
            return null;
        }
        public static void UpdateDirectories() {
            directories = SettingsEditor.ReadSettings().Directories;
        }
    }
}
