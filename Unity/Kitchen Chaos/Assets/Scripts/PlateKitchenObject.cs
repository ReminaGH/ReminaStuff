using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {


    public event EventHandler<OnIngredientAddEventArgs> OnIngredientAdd;
    public class OnIngredientAddEventArgs : EventArgs { 
        public KitchenObjSO kitchenObjSO;
    }

    [SerializeField] private List<KitchenObjSO> validKitchObjSOList; 

    private List<KitchenObjSO> kitchenObjSOList;

    protected override void Awake() {
        base.Awake();
        kitchenObjSOList = new List<KitchenObjSO>();
    }
    public bool TryAddIngredient(KitchenObjSO kitchenObjSO) {
        if (!validKitchObjSOList.Contains(kitchenObjSO)) {
            //Not a valid ingredient
            return false; 
        
        }
        if (kitchenObjSOList.Contains(kitchenObjSO)) { // Prevents adding duplicates, remove / change later for polish
            //Already has this type
            return false;
        }
        else {
            kitchenObjSOList.Add(kitchenObjSO);

            OnIngredientAdd?.Invoke(this, new OnIngredientAddEventArgs {
                kitchenObjSO = kitchenObjSO

            });
            return true;
        }
    }

    public List<KitchenObjSO> GetKitchenObjSOList() {
        return kitchenObjSOList;
    }

}
    