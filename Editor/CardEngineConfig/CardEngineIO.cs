using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SadSapphicGames.CardEngineEditor {
    /// <summary>
    /// Static class for methods to get important directories
    /// </summary>
    public static class CardEngineIO {
        /// <summary>
        /// Reads the directories in the setting
        /// </summary>
        public static Directories directories { get => SettingsEditor.ReadSettings().Directories;}
        /// <summary>
        /// Gets the path for the package files from its possible installation locations
        /// </summary>
        /// <returns></returns>
        public static string GetPackagePath() {
            // ? standard package location if installed in the Packages folder
            string packagePath = "Packages/com.sadsapphicgames.cardengine";
            if(Directory.Exists(packagePath)) {
                return packagePath;
            } 
            // ? location for package if installed in the Assets folder or during development
            packagePath = "Assets/Packages/CardEngine";
            if (Directory.Exists(packagePath)) {
                return packagePath;
            }
            //? Alternate location for package if installed in the Assets folder
            packagePath = "Assets/CardEngine";
            if (Directory.Exists(packagePath)) {
                return packagePath;
            }
            // ? failed to find
            Debug.LogWarning("Failed to find CardEngine install location, returning null for path");
            return null;
        }

    }
}
