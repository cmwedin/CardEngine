using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SadSapphicGames.CardEngineEditor {
    /// <summary>
    /// Static class for copying templates when using the packages create tools
    /// </summary>
    public static class TemplateIO  {
        /// <summary>
        /// The path for the template directory
        /// </summary>
        static string templatePath = CardEngineIO.GetPackagePath() + "/Editor/Templates/";
        /// <summary>
        /// Copies a given template to a given directory with a new name
        /// </summary>
        /// <param name="templateName">the name of the template to copt</param>
        /// <param name="newFileName">The new name to give the template</param>
        /// <param name="parentDirectory">the parent directory to copy the template too</param>
        /// <exception cref="ArgumentException">Thrown if the parent directory cannot be found</exception>
        public static void CopyTemplate(string templateName, string newFileName, string parentDirectory) {
            if(!Directory.Exists(parentDirectory)) {
                throw new ArgumentException($"parent directory {parentDirectory} for file {newFileName} not found");
            }
            using StreamReader reader = new StreamReader(templatePath + templateName);
            using StreamWriter writer = new StreamWriter(parentDirectory + "/" + newFileName);
            newFileName = newFileName.Split(".")[0];
            templateName = templateName.Split(".")[0];
            string line = "";
            while((line = reader.ReadLine()) != null) {
                // Debug.Log($"copying line: {line}");
                if(line.Contains(templateName)) {
                    // Debug.Log("Replacing filler class name");    
                    line = line.Replace(templateName,newFileName); 
                }
                writer.WriteLine(line);
            }
            reader.Close();
            writer.Close();
        }
    }
}