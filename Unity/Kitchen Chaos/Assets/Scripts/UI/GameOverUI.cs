using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI recipesDeliveredText;


    private void Start() {
        KitchenGameManager.Instance.OnStageChanged += KitchenGamemanager_OnStageChanged;

        Hide();
    }

    private void KitchenGamemanager_OnStageChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsGamerOver()) {
            Show();

            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
        }
        else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
