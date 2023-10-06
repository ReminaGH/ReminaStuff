using System;
using System.Collections;
using System.Collections.Generic;
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
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0f;

            if (spawnPlateCount < spawnPlateCountMax) {
                spawnPlateCount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            //Player is empty handed
            if (spawnPlateCount > 0) {
                //There is at least one plate here
                spawnPlateCount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
