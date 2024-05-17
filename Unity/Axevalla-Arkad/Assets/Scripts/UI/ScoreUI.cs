using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Score { get; private set; }
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private TextMeshProUGUI scoreUIText;

    int testScore;


    private void Awake() {
        Score = this;
    }
    public void Update() {
        
        
    }
}
