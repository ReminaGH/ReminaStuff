using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System;

public class WriteToFile : MonoBehaviour
{


    //Creates a directory called "streamingAssetsPath" in the appropriate directory of the game
    void Awake()
    {

        Directory.CreateDirectory(Application.streamingAssetsPath + "/Score_Log/");

    }

    private void Start() {

        CreateTextFile();
        
    }

    //Creates a .txt file to save the variables we want returned, such as score. This is where you'd return an int using
    //a method to get a score that the acrade-hall can read and save.
    public void CreateTextFile() {

        string txtDocumentName = Application.streamingAssetsPath + "/Score_Log/" + "Score" + ".txt";

        if (File.Exists(txtDocumentName)) {

            File.Delete(txtDocumentName);
        
        }
        File.WriteAllText(txtDocumentName, "");
    }
}
