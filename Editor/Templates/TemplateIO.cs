using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SadSapphicGames.CardEngineEditor {
    public static class TemplateIO  {
        static string templatePath = CardEngineIO.GetPackagePath() + "/Editor/Templates/";
        public static void CopyTemplate(string templateName, string newFileName, string parentDirectory) {
            if(!Directory.Exists(parentDirectory)) {
                throw new Exception($"parent directory {parentDirectory} for file {newFileName} not found");
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

        // internal static void GenerateSettings() {
        //     if(!Directory.Exists("Assets/CardEngine")) {
        //         AssetDatabase.CreateFolder("Assets","CardEngine");
        //     }
        //     CopyTemplate("DefaultSettings.json","settings.json","Assets/CardEngine");
        // }
    }
}