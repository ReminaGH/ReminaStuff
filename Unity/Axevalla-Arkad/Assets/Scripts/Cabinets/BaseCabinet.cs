using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCabinet : MonoBehaviour
{

    private int scoreCounter = 3;

    public void Interact() {

        Debug.Log("Interact!");
        scoreCounter++;
    }

    public int GetCurrentScore() { 
        return scoreCounter;
    }
}
