using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {


    //Deletes objects if they're only plates, for the purpose of delivery
    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                //Only accepts plates

                DeliveryManager.Instance.DeliverRecipie(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }

}
