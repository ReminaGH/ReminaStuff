using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Xml.Linq;
using System.IO;
using UnityEditor;
using static UnityEditor.Progress;
using System.Text;
using UnityEditor.Timeline.Actions;

public class BaseCabinet : MonoBehaviour
{
    [SerializeField] private GameInputUI gameInputUI;
    [SerializeField] string filePathName;
    [SerializeField] string projectName;

    string fileContents;
    private int cabinetScoreCounter = 0;

    string test_1 = "Test";
    string test_2 = "My project";

    string test = @"\Test\My project_Data\StreamingAssets\Score_Log\Score" + ".txt";
    string test2;
    string test3;
 
    private void Update() {

        filePathName = gameInputUI.ReturnInputName1();
        projectName = gameInputUI.ReturnInputName2();

        test2 = @"\" + test_1 + @"\" + test_2 + @"_Data\StreamingAssets\Score_Log\Score" + ".txt";
        test3 = @"\" + filePathName + @"\" + projectName + @"_Data\StreamingAssets\Score_Log\Score" + ".txt";

        

        string readFromFileTest = Application.streamingAssetsPath + test2;
        fileContents = File.ReadAllText(readFromFileTest);

        UnityEngine.Debug.Log(filePathName);
        UnityEngine.Debug.Log(test_1);
        UnityEngine.Debug.Log(projectName);
        UnityEngine.Debug.Log(test_2);
        UnityEngine.Debug.Log(test2);
        UnityEngine.Debug.Log(test3);

    }

    public void Interact() {

        UnityEngine.Debug.Log("Interact!");

        RunFile();
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

    private static void RunFile() {
        Process.Start(Environment.CurrentDirectory + @"\Assets\StreamingAssets\Test\My project" + ".exe");
    }


}
