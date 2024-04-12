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
using static UnityEngine.InputSystem.InputAction;

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
    string readFromFile;

    private void Update() {

        UpdateFilePath();
        

        try {
            fileContents = File.ReadAllText(readFromFile);
        } catch (Exception e) {
        }

        //UnityEngine.Debug.Log(fileContents);
        //UnityEngine.Debug.Log(readFromFile);
        //C:/Users/datahaxx/Documents/CPGit/CpStuff/Unity/Axevalla-Arkad/Assets/StreamingAssets//_Data/StreamingAssets/Score_Log/Score.txt
        //C:\Users\datahaxx\Documents\CPGit\CpStuff\Unity\Axevalla-Arkad/Assets/StreamingAssets/Test/My project.exe

        
       


    }
    private void Start() {
        UpdateFilePath();
        
    }

    public void Interact() {

        UpdateFilePath();
        //UnityEngine.Debug.Log("Sökvägen är för fullFilePath: " + fullFilePath);
        //UnityEngine.Debug.Log("Sökvägen är för Application.streamingAssetsPath: " + readFromFile);
        UnityEngine.Debug.Log("Sökvägen är för Enviroment: " + Environment.CurrentDirectory + "/Assets/StreamingAssets/" + filePathNameCorrected + "/" + projectNameCorrected + ".exe");
        RunFile(filePathNameCorrected, projectNameCorrected);
    }

    public void InteractAlt() {
        UpdateFilePath();

        UnityEngine.Debug.Log("Interact Alt!");

        gameInputUI.Show();
    }
    public string GetCurrentScoreLogFile() {
        return fileContents;
    }

    public int GetCurrentScore() {
        return cabinetScoreCounter;
    }

    private static void RunFile(string filePathNameCorrected, string projectNameCorrected) {
        try {
            Process.Start(Environment.CurrentDirectory + "/My project_Data/StreamingAssets/" + filePathNameCorrected + "/" + projectNameCorrected + ".exe");
            UnityEngine.Debug.Log(Environment.CurrentDirectory + "/My project_Data/StreamingAssets/" + filePathNameCorrected + "/" + projectNameCorrected + ".exe");
        } catch (Exception e) {
            UnityEngine.Debug.Log(e.ToString());
        }
    }

    public string UpdateFilePath() {

        filePathName = gameInputUI.ReturnInputName1();
        projectName = gameInputUI.ReturnInputName2();

        try {
            filePathNameCorrected = filePathName.Substring(0, filePathName.Length - 1);
            projectNameCorrected = projectName.Substring(0, projectName.Length - 1);
        } catch (Exception e) {
            UnityEngine.Debug.Log("No file found, error message: " + e);
        }

 
        fullFilePath = "/" + filePathNameCorrected + "/" + projectNameCorrected + "_Data/StreamingAssets/Score_Log/Score" + ".txt";
        //readFromFile = Application.streamingAssetsPath + fullFilePath;
        readFromFile = @"C:\\Users\\datahaxx\\Documents\\CPGit\\CpStuff\\Unity\\Compiled\\My project_Data\\StreamingAssets" + fullFilePath;
        return fullFilePath;
    }

}
