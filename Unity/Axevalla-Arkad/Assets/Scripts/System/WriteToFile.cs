using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System;

public class WriteToFile : MonoBehaviour
{

    [SerializeField] BaseCabinet baseCabinet;

    void Awake()
    {

        Directory.CreateDirectory(Application.streamingAssetsPath + "/Score_Log/");


    }

    private void Start() {

        CreateTextFile();
        
    }

    public void CreateTextFile() {

        string txtDocumentName = Application.streamingAssetsPath + "/Score_Log/" + "Score_" + baseCabinet.GetGameName() + ".txt";

        if (File.Exists(txtDocumentName)) {

            File.Delete(txtDocumentName);
        
        }

        File.WriteAllText(txtDocumentName, baseCabinet.GetCurrentScore());
    }
}
