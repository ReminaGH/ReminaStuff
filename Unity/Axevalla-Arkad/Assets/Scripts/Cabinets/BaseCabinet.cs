using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Xml.Linq;
using System.IO;
using UnityEditor;
using System.Text;
using Mono.CSharp;

public class BaseCabinet : MonoBehaviour
{
    [SerializeField] private GameInputUI gameInputUI;
    [SerializeField] string filePathName;
    [SerializeField] string projectName;

    string fileContents;
    private int cabinetScoreCounter = 0;
    string fullFilePath;
    string projectNameCorrected;
    string filePathNameCorrected;
 
    private void Update() {

        filePathName = gameInputUI.ReturnInputName1();
        projectName = gameInputUI.ReturnInputName2();

        filePathNameCorrected = filePathName.Substring(0, filePathName.Length - 1);
        projectNameCorrected = projectName.Substring(0, projectName.Length - 1);

        fullFilePath = "/" + filePathNameCorrected + "/" + projectNameCorrected + "_Data/StreamingAssets/Score_Log/Score" + ".txt";


        /*try {
            string readFromFile = Application.streamingAssetsPath + fullFilePath;
            fileContents = File.ReadAllText(readFromFile);
        } catch {

            UnityEngine.Debug.Log("Cabinet number :" + gameObject.name + " has no correct path");
        }*/
        
    }
    private void Start() { // Not right one for intialiting score
        string readFromFile = Application.streamingAssetsPath + fullFilePath;
        fileContents = File.ReadAllText(readFromFile);
    }

    public void Interact() {

        UnityEngine.Debug.Log("Interact!");
        string readFromFile = Application.streamingAssetsPath + fullFilePath;
        fileContents = File.ReadAllText(readFromFile);
        RunFile(filePathNameCorrected, projectNameCorrected);
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

    private static void RunFile(string filePathNameCorrected, string projectNameCorrected) {
        Process.Start(Environment.CurrentDirectory + "/Assets/StreamingAssets/"+ filePathNameCorrected +"/" + projectNameCorrected + ".exe");
    }

}
