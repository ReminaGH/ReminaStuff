using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System;


/*
!! AXEVALLA ARKAD SCORE SCRIPT - Step by Step guide to impliment !! 

This script creates a directory location called StreamingAsset in your game, then further creates a map called Score_Log with a single txt file called Score.txt
After being created, it is then used to write !!YOUR!! games score into the file itself so Axevalla Arkad can read the score. Each time the script is run,
It checks if the file exists and then overrides it. You can change the script but the final result should always contain the previous mentioned locations and files.

NOTE: IF YOUR GAME DOES NOT HAVE A SCORE, OR INTENDS TO HAVE A SCORE, CREATE SOMETHING TO RETURN AS A INT. THIS CAN BE A TIMER OR WHATEVER! Remember that the returned
score will be used as currency for the Arkad and should have some formula to return a reasonable number. Example: for every 1 min, give 1 score.

STEP BY STEP PROCESS!

1. Attatch the script / Impliment the code.

Depending on where you save your score, you should impliment this script, either the code itself, or by attatching the script to your main game object.

Notes: The current flow is that it saves the score when the object / script is attatched to is destoryed under the Private void OnDestroy. If your object keeping track of your score presists
for a long time, I.E until the game is closed, you can change Private void OnDestroy to OnApplicationExit.


2. Create a method to expose your score:

This method should be created inside of the script that keeps your score. Underneath is an example you can copy and use:

Public void ReturnScore() {

return yourScoreVariable;

}

Make sure to change the "yourScoreVariable" to actually be the score you want to return.


3. Exposing your method

Here you need to create an instance of your class to be able to expose it to the script and send it away. There are multiple ways of doing but this guide
assumes you've attatched the script to your object. This is also called the "singleton pattern" if you require more info about this way.

Inside of your object where the score is saved, do the following:

Add this to your variables, like how you'd add a new Int or bool

public static Score Instance { get; private set; }

then inside your awake, add the following:

private void Awake() {
    Instance = this;
}

If you already are using the private void Awake() just copy and paste Instance = this;


4. Asign the variable.

Assuming you've created a method to expose your score, now you need to asgin it to gameScore. Using the Singleton pattern, you can now access the method inside of THIS script.

 private void OnDestroy() {
       
 This part -->  gameScore = Score.Instance.ReturnScore();  <-- This part

        WriteToTxtFile();
    }

NOTE: If you happen to have your score saved as a string, you need to convert it to an int.


5. Checking if it's correct.

If everything is correct, check the following after launching and testing the game for errors:
1. Inside of your "your name"_data (example: my project_data) should be a folder called StreamingAsset
2. A folder called Score_Log should exist
3. Inside of the folder should exist a txt file called Score.txt
4. Opening the txt file should give you the socre you returned.

If you are not getting the correct score, try and debug to see if you are sendig the right information to the right place using Debug.Log. 
The final project should simply have your correct score being sent to the txt file.

Made by Christofer Persson 2024, Discord: .remina, mail: Christoferpworkmail@gmail.com
*/
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

   
    private void OnDestroy() {
        //CHANGE THE 0 TO YOUR SCORE VARIABLE
        gameScore = 0;
        //SEE STEP 4 FOR INFO
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
