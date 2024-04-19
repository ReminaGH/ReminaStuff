using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Linq;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUIText;
    [SerializeField] private BaseCabinet baseCabinet;

    string baseCabinetStringContent;


    //Returns the file contents of the .txt file using the specicifc file locations provided by the BaseCabinet
    private void Update() {


        baseCabinetStringContent = baseCabinet.GetCurrentScoreLogFile();
  
        scoreUIText.text = baseCabinetStringContent;
        
        

    }
}
