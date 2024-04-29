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
        
        //Reads the files where the score is provided, this is used to provide an accurate and realtime score of whatever game is playing.
        try {
            fileContents = File.ReadAllText(readFromFile);
        } catch (Exception e) {
        }

    }
    private void Start() {
        UpdateFilePath();
        
    }

    //This function is executed by the playerController whenever they press "e" on the cabinet, this uses an event system to trigger it.
    public void Interact() {

        UpdateFilePath();

        //Method to run the appropriate files, names and locations are provided and corrected by another method
        RunFile(filePathNameCorrected, projectNameCorrected);
    }

    //This method opens up the alternate Ui where you as the user can input the appropriate location paths to your game
    public void InteractAlt() {
        UpdateFilePath();

        UnityEngine.Debug.Log("Interact Alt!");

        gameInputUI.Show();
    }

    //Returns the filelocation of the txt file that contains the current score
    public string GetCurrentScoreLogFile() {
        return fileContents;
    }

    public int GetCurrentScore() {
        return cabinetScoreCounter;
    }

    //This runs the selected program, and uses the corrected path to launch the correct .exe file
    private static void RunFile(string filePathNameCorrected, string projectNameCorrected) {
        try {

            Process.Start(Application.streamingAssetsPath + "/" + filePathNameCorrected + "/"+ projectNameCorrected + ".exe");

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
        readFromFile = Application.streamingAssetsPath + fullFilePath;

        return fullFilePath;

    }

}
