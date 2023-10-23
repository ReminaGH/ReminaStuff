using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingCharacterSelectUI : MonoBehaviour {



    [SerializeField] private Button readyCharacterSelect;



    private void Awake() {
            readyCharacterSelect.onClick.AddListener(() => {
                CharacterSelectReady.Instance.SetPlayerReady();
            });
        }
    }
