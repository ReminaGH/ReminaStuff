using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectedCabinetVisual : MonoBehaviour
{

    [SerializeField] private BaseCabinet baseCabinet;

    private void Start() {
        PlayerController.Instance.OnSelectedCabinetChanged += Instance_OnSelectedCabinetChanged;
    
    }

    private void Instance_OnSelectedCabinetChanged(object sender, PlayerController.OnSelectedCabinetChangedEventArgs e) {
        throw new System.NotImplementedException();
    }
}
