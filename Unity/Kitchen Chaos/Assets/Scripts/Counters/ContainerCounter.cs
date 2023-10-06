using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {


    public event EventHandler OnPlayerGrabedObject;

    [SerializeField] private KitchenObjSO kitchenObjSO;


    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            //Player is not carrying an object
            KitchenObject.SpawnKitchenObject(kitchenObjSO, player);

            //Invokes animation of the containerCounter with an event
            OnPlayerGrabedObject?.Invoke(this, EventArgs.Empty);
        }

    }
    
}
