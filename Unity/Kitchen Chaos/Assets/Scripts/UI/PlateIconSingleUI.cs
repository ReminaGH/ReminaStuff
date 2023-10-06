using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour {


    [SerializeField] private Image image;

    //Handles everything related to the single UI canvas
    public void SetKitchenObjSO(KitchenObjSO kitchenObjSO) {
        image.sprite = kitchenObjSO.sprite;
    
    }
}
