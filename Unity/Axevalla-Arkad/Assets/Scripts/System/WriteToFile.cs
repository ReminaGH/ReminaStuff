using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System;


//This script should be placed in the main game object, either as itself or the code copied. If the game does not have a persistant gameObject, apply this code
//to where the game store it's score and use that to directly access the score.

//If the game does not have a Score system, create a simple timer function on your most persistent game object and do the same by exposing the int that is
//the timer to the gameInt
public class WriteToFile : MonoBehaviour
{

    int gameScore;
    string txtDocumentName = Application.streamingAssetsPath + "/Score_Log/" + "Score" + ".txt";


    //Creates a directory called "streamingAssetsPath" in the appropriate directory of the game
    void Awake()
    {

        Directory.CreateDirectory(Application.streamingAssetsPath + "/Score_Log/");

    }

    private void Start() {

        CreateTextFile();
        
    }

    //Your game needs to write its score to the gameScore through an exposed method inside your code.
    /*
     
    Public int ReturnScore() {
    
    return GameScore;
    
    }
      
    Then: testScore = _Location of socre_.ReturnScore();

    Example: testScore = baseGame.ReturnScore();
      
     */
    private void OnApplicationQuit() {
        gameScore = 0;
        WriteToTxtFile();
    }

    //Creates a .txt file to save the variables we want returned, such as score. This is where you'd return an int using
    //a method to get a score that the acrade-hall can read and save.
    public void CreateTextFile() {

        

        if (File.Exists(txtDocumentName)) {

            File.Delete(txtDocumentName);
        
        }
        File.WriteAllText(txtDocumentName, "");
    }

    public void WriteToTxtFile() {

        File.WriteAllText(txtDocumentName, gameScore.ToString());
    }
}
