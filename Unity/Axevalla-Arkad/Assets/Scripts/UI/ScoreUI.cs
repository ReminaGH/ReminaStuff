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

    private void Update() {
       

        scoreUIText.text = baseCabinet.GetCurrentScoreLogFile();
        

    }
}
