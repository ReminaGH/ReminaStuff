using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjSO plateKitchenObjSO;


    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int spawnPlateCount;
    private int spawnPlateCountMax = 4;

    private void Update() {
        if (!IsServer) {
            return;
        }
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0f;

            if (KitchenGameManager.Instance.IsGamePlaying() && spawnPlateCount < spawnPlateCountMax) {
                SpawnPlatesServerRpc();
            }
        }
    }

    [ServerRpc]
    private void SpawnPlatesServerRpc() {
        SpawnPlatesClientRpc();
    }
    [ClientRpc]
    private void SpawnPlatesClientRpc() {
        spawnPlateCount++;

        OnPlateSpawned?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            //Player is empty handed
            if (spawnPlateCount > 0) {
                //There is at least one plate here
                KitchenObject.SpawnKitchenObject(plateKitchenObjSO, player);

                InteractLogiclServerRpc();
            }
        }
    }


    [ServerRpc(RequireOwnership = false)]
    private void InteractLogiclServerRpc() {
        InteractLogiclClientRpc();
    }

    [ClientRpc]
    private void InteractLogiclClientRpc() {
        spawnPlateCount--;

        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
    }
}
