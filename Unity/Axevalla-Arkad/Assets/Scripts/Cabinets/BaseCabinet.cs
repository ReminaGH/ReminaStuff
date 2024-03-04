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
    [SerializeField] private GameInputUI gameInputUI;
    [SerializeField] string filePathName = "";
    [SerializeField] string projectName = "";
    
    string fileContents;
    private int cabinetScoreCounter = 0;

    private const string FILE_PATH_NAME_ORIGIN = "/Assets/StreamingAssets/";
    private const string FILE_PATH_SCORE = "/StreamingAssets/Score_Log/Score.txt";
    private const string PROJECT_DATA_PATH = "_Data";

    private void Update() {

        filePathName = gameInputUI.ReturnInputName1();
        projectName = gameInputUI.ReturnInputName2();

        string readFromFileTest = Application.streamingAssetsPath + "/" + filePathName + "/" + projectName + PROJECT_DATA_PATH + FILE_PATH_SCORE;

        fileContents = File.ReadAllText(readFromFileTest);

    }

    public void Interact() {

        UnityEngine.Debug.Log("Interact!");

        RunFile(filePathName, FILE_PATH_NAME_ORIGIN, projectName);
    }

    public void InteractAlt() {

        UnityEngine.Debug.Log("Interact Alt!");

        gameInputUI.Show();
    }
    public string GetCurrentScoreLogFile() {
        string fileText = fileContents;

        return fileContents;
    }

    public int GetCurrentScore() {
        return cabinetScoreCounter;
    }

    public string GetGameName() {
        string fileName = filePathName;

        return fileName;
    }

    private static void RunFile(string filePathName, string FILE_PATH_NAME_ORIGIN, string projectName) {
        Process.Start(Environment.CurrentDirectory + @FILE_PATH_NAME_ORIGIN + @filePathName + "/" + @projectName + @".exe");
    }

    
}
