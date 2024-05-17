using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private TextMeshProUGUI scoreUIText;

    int testScore;

    public void Update() {
        
        scoreUIText.text = BaseCabinet.Score.GetCurrentScoreLogFile();
    }
}
