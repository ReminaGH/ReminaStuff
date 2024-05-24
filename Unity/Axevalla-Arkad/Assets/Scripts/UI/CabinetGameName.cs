using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CabinetGameName : MonoBehaviour
{

    [SerializeField] BaseCabinet baseCabinet;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    
    
    private void Update() {
        textMeshProUGUI.text = baseCabinet.ReturnCabinetGameName();     
    }
}
