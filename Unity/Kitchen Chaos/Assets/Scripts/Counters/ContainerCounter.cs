using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ContainerCounter : BaseCounter {


    public event EventHandler OnPlayerGrabedObject;

    [SerializeField] private KitchenObjSO kitchenObjSO;


    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            //Player is not carrying an object
            KitchenObject.SpawnKitchenObject(kitchenObjSO, player);

            InteractLogiclServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractLogiclServerRpc() {
        InteractLogiclClientRpc();
    }

    [ClientRpc]
    private void InteractLogiclClientRpc() {
        OnPlayerGrabedObject?.Invoke(this, EventArgs.Empty);
    }
}
