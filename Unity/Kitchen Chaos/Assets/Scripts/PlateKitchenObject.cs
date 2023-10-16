using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
            AddIngredientServerRpc(
                KitchenGameMultiplayer.Instance.GetKitchenObjectSOIndex(kitchenObjSO)
                );
            
            return true;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void AddIngredientServerRpc(int kitchenObjectSOIndex) {
        AddIngredientClientRpc(kitchenObjectSOIndex);
    }

    [ClientRpc]
    private void AddIngredientClientRpc(int kitchenObjectSOIndex) {
        KitchenObjSO kitchenObjSO = KitchenGameMultiplayer.Instance.GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);
        kitchenObjSOList.Add(kitchenObjSO);

        OnIngredientAdd?.Invoke(this, new OnIngredientAddEventArgs {
            kitchenObjSO = kitchenObjSO

        });
    }

    public List<KitchenObjSO> GetKitchenObjSOList() {
        return kitchenObjSOList;
    }

}
    