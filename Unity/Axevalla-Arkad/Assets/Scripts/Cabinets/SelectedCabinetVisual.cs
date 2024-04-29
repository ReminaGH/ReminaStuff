using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectedCabinetVisual : MonoBehaviour
{

    [SerializeField] private BaseCabinet baseCabinet;
    [SerializeField] private GameObject visualGameObject;

    private void Start() {
        PlayerController.Instance.OnSelectedCabinetChanged += Player_OnSelectedCabinetChanged;
    
    }

    private void Player_OnSelectedCabinetChanged(object sender, PlayerController.OnSelectedCabinetChangedEventArgs e) {
        if (e.selectedCabinet == baseCabinet) {
            Show();
        }
        else {
            Hide();
        }
    }
    private void Show() { 
        visualGameObject.SetActive(true);
    
    }
    private void Hide() {
        visualGameObject.SetActive(false);

    }
}
