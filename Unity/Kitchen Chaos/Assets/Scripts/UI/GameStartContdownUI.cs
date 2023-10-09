using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartContdownUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI countdownText;



    private void Start() {
        KitchenGameManager.Instance.OnStageChanged += KitchenGamemanager_OnStageChanged;

        Hide();
    }

    private void KitchenGamemanager_OnStageChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsCountdownToStartActive()) {
            Show();
        } else {
            Hide();
        }
    }

    private void Update() {
        countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountdownToStartTimer()).ToString();
    }
    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
