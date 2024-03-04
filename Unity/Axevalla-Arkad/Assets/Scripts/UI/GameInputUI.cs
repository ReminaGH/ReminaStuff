using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameInputUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI inputField1;
    [SerializeField] TextMeshProUGUI inputField2;
    [SerializeField] private Button exitButton;
    [SerializeField] private BaseCabinet baseCabinet;

    string savedInputField1;
    string savedInputField2;


    private void Start() {
        Hide(); 
    }
    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide() {
        gameObject.SetActive(false);

        savedInputField1 = inputField1.text;
        savedInputField2 = inputField2.text;
    }

    public string ReturnInputName1() {
        return savedInputField1;
    }

    public string ReturnInputName2() {
        return savedInputField2;
    }
}
