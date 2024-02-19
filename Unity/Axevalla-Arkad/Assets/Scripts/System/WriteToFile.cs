using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WriteToFile : MonoBehaviour
{

    [SerializeField] BaseCabinet baseCabinet;

    void Start()
    {

        Directory.CreateDirectory(Application.streamingAssetsPath + "/Score_Log/");

    }

    private void OnApplicationQuit() {

        CreateTextFile();
    }

    public void CreateTextFile() {

        string txtDocumentName = Application.streamingAssetsPath + "/Score_Log/" + "Score" + ".txt";

        if (File.Exists(txtDocumentName)) {

            File.Delete(txtDocumentName);
        
        }

        File.WriteAllText(txtDocumentName, baseCabinet.GetCurrentScore().ToString());
    }
}
