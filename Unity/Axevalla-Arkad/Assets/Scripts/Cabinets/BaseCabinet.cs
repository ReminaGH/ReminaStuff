using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Xml.Linq;
using System.IO;

public class BaseCabinet : MonoBehaviour
{

    [SerializeField] String filePath;
    string fileContents;

    private int scoreCounter = 1;

    private void Update() {
        string readFromFileTest = Application.streamingAssetsPath + "/Score_Log/" + "Score" + ".txt";

        fileContents = File.ReadAllText(readFromFileTest);

    }
    public void Interact() {

        UnityEngine.Debug.Log("Interact!");
        
        //RunFile();
    }
    public string GetCurrentScore() {
        string fileText = fileContents;

        return fileContents;
    }

    private static void RunFile() {
        Process.Start(Environment.CurrentDirectory + @"\Assets\StreamingAssets\Test\My project.exe");
    }

    
}
