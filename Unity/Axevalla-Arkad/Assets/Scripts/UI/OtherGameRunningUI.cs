using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherGameRunningUI : MonoBehaviour
{

    public static OtherGameRunningUI isRunningUI { get; private set; }

    private void Awake() {
         isRunningUI = this;
    }
    private void Start() {
        Hide();
    }
    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide() {
        gameObject.SetActive(false);

    }
}
