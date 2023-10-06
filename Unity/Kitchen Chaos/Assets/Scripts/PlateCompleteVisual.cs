using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVisual : MonoBehaviour {

    [Serializable]
    public struct KitchenObjSO_GameObject {

        public KitchenObjSO kitchenObjSO;
        public GameObject gameObject; 
     
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjSO_GameObject> kitchenObjSO_GameObjSOList;

    private void Start() {
        plateKitchenObject.OnIngredientAdd += PlateKitchenObject_OnIngredientAdd;

        foreach (KitchenObjSO_GameObject kitchenObjSOGameObj in kitchenObjSO_GameObjSOList) {
                kitchenObjSOGameObj.gameObject.SetActive(false);

            }
    }

    //Reveals inactive visual objects and sets them active to show them
    private void PlateKitchenObject_OnIngredientAdd(object sender, PlateKitchenObject.OnIngredientAddEventArgs e) {
        foreach (KitchenObjSO_GameObject kitchenObjSOGameObj in kitchenObjSO_GameObjSOList) {
            if (kitchenObjSOGameObj.kitchenObjSO == e.kitchenObjSO) { 
                kitchenObjSOGameObj.gameObject.SetActive(true);

            }
        }
    }
}
