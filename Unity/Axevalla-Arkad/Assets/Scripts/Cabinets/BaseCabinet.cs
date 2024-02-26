using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Xml.Linq;
using System.IO;
using UnityEditor;

public class BaseCabinet : MonoBehaviour
{

    [SerializeField] private string filePathName = "";

    [SerializeField] private string testName = "Test";
    string fileContents;

    private int scoreCounter = 1;

    private const string FILE_PATH_NAME_ORIGIN = "/Assets/StreamingAssets/";
    private const string FILE_PATH_SCORE = "My poject_Data/StreamingAssets/Score_Log";
    string fileEndPath;

    


    //\Assets\StreamingAssets\Test\My project_Data\StreamingAssets\Score_Log\Score.txt


    private void Update() {
        //string readFromFileTest = Application.streamingAssetsPath + "/Score_Log/" + "Score_" + filePathName + ".txt";
        string readFromFileTest = Application.streamingAssetsPath + "/" + testName + FILE_PATH_SCORE + "Score" + ".txt";

        fileContents = File.ReadAllText(readFromFileTest);

        fileEndPath = FILE_PATH_NAME_ORIGIN + testName + FILE_PATH_SCORE;

    }
    public void Interact() {

        UnityEngine.Debug.Log("Interact!");

        RunFile();

        UnityEngine.Debug.Log(@"\Assets\StreamingAssets\Test\My project_Data\StreamingAssets\Score_Log\Score.txt");
    }
    public string GetCurrentScore() {
        string fileText = fileContents;

        return fileContents;
    }

    public string GetGameName() {
        string fileName = filePathName;

        return fileName;
    }

    private static void RunFile() {
        Process.Start(Environment.CurrentDirectory + "S");
        //Process.Start(Environment.CurrentDirectory + @"\Assets\StreamingAssets\Test\My project.exe");
    }

    
}
