using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjSO kitchenObjSO;
    
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // There is no KitchenObject here
            if (player.HasKitchenObject()) { 
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else { 
                //Player is not carrying something 
            }
        } else {
            // There is KitchenObject here
            if (player.HasKitchenObject()) {
                //Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //Player is holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjSO())) {
                        KitchenObject.DestroyKitchenObject(GetKitchenObject());
                        
                    }
                } else {
                    //Player is not carrying a plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        //Counter has a plate on it
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjSO())) {
                            KitchenObject.DestroyKitchenObject(player.GetKitchenObject());

                        }
                    }
                }
            } else {
                //Player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
        
    }

}
    