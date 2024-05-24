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
using Unity.VisualScripting;
using System.Security.AccessControl;
using EditorAttributes;

public class BaseCabinet : MonoBehaviour
{
    [SerializeField] private GameInputUI gameInputUI;
    [SerializeField] private OtherGameRunningUI otherGameRunningUI;
    [SerializeField] string filePathName;
    [SerializeField] string projectName;
    [SerializeField] private bool ChangeName = false;

    string fileContents;
    private int cabinetScoreCounter = 3;
    string fullFilePath;
    string projectNameCorrected;
    string filePathNameCorrected;
    string readFromFile;

    private void Update() {

        
        
        //Reads the files where the score is provided, this is used to provide an accurate and realtime score of whatever game is playing.
        try {
            fileContents = File.ReadAllText(readFromFile);
        } catch (Exception e) {
        }
        
    }

    private void Awake() {

        UpdateFilePathOnAwake();
       

    }
    private void Start() {

        
        UpdateFilePathOnAwake();
        

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
    private void RunFile(string filePathNameCorrected, string projectNameCorrected) {
        try {
            
            Process.Start(Application.streamingAssetsPath + "/" + filePathNameCorrected + "/"+ projectNameCorrected + ".exe");
            PlayerController.Instance.gamePausedToggle();
            otherGameRunningUI.Show();

        } catch (Exception e) {
            UnityEngine.Debug.Log(e.ToString());
        }
    }

    public string UpdateFilePath() {

        if (filePathName == "" && projectName == "") { filePathName = gameInputUI.ReturnInputName1(); 
        

            try {
                filePathNameCorrected = filePathName.Substring(0, filePathName.Length - 1);
                projectNameCorrected = projectName.Substring(0, projectName.Length - 1);
            } catch (Exception e) {
                UnityEngine.Debug.Log("No file found, error message: " + e);
            }
        }

    fullFilePath = "/" + filePathNameCorrected + "/" + projectNameCorrected + "_Data/StreamingAssets/Score_Log/Score" + ".txt";
        readFromFile = Application.streamingAssetsPath + fullFilePath;

        return fullFilePath;

    }

    public string UpdateFilePathOnAwake() {


        try {
            filePathNameCorrected = filePathName;
            projectNameCorrected = projectName;
        } catch (Exception e) {
            UnityEngine.Debug.Log("No file found, error message: " + e);
        }


        fullFilePath = "/" + filePathNameCorrected + "/" + projectNameCorrected + "_Data/StreamingAssets/Score_Log/Score" + ".txt";
        readFromFile = Application.streamingAssetsPath + fullFilePath;

        return fullFilePath;

    }

    public void UpdateGame() {
        UpdateFilePath();
        UnityEngine.Debug.Log(GetCurrentScoreLogFile());
    }

    public void UpdateScore() {
        ScoreUI.Score.AddScore(int.Parse(fileContents));
        UnityEngine.Debug.Log("File contents, current score: " + fileContents);
    }
    public string ReturnCabinetGameName() {
        if (ChangeName == false) {
            return projectNameCorrected;
        }
        else {
            return filePathNameCorrected;
        }
    }

    public string ReturnProjectNameOnStart() {

        return projectNameCorrected;
    }

    public string ReturnFileNameOnStart() {

        return filePathNameCorrected;
    }

}
