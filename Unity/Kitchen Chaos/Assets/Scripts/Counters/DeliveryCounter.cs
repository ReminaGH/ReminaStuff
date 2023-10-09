using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {


    //Singleton method for a single instance where we use the positon for the object to play a sound
    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
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
