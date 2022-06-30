using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class TemplateIO  {
    public static void CopyTemplate(string templateName, string newFileName, string newFilePath) {
        StreamReader reader = new StreamReader(templateName);
        StreamWriter writer = new StreamWriter(newFilePath + "/" + newFileName);
        newFileName = newFileName.Split(".")[0];
        string line = "";
        while((line = reader.ReadLine()) != null) {
            // Debug.Log($"copying line: {line}");
            if(line.Contains("FILENAME")) {
                // Debug.Log("Replacing filler class name");    
                line = line.Replace("FILENAME",newFileName); 
            }
            writer.WriteLine(line);
        }
        reader.Close();
        writer.Close();
    }
}
