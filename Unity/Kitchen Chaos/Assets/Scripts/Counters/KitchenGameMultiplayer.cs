using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KitchenGameMultiplayer : NetworkBehaviour {
  

    public static KitchenGameMultiplayer Instance { get; private set; }

    [SerializeField] private KitchenObjectListSO kitchenObjectListSO;

    private void Awake() {
        Instance = this;
    }

    public void SpawnKitchenObject(KitchenObjSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        SpawnKitchenObjectServerRpc(GetKitchenObjectSOIndex(kitchenObjectSO), kitchenObjectParent.GetNetworkObject());
    }
    
        [ServerRpc(RequireOwnership = false)]
        private void SpawnKitchenObjectServerRpc(int kitchenObjectSOIndex, NetworkObjectReference kitchenObjectParentNetworkObjectReference) {
            KitchenObjSO kitchenObjSO = GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);

            Transform kitchenObjectTransform = Instantiate(kitchenObjSO.prefab);

            NetworkObject kitchenNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
            kitchenNetworkObject.Spawn(true);

            KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

            kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
            IKitchenObjectParent kitchenObjectParent = kitchenObjectParentNetworkObject.GetComponent<IKitchenObjectParent>();

            kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
    }

    private int GetKitchenObjectSOIndex(KitchenObjSO kitchenObjSO) { 
        return kitchenObjectListSO.kitchenObjectSOList.IndexOf(kitchenObjSO);
    }

    private KitchenObjSO GetKitchenObjectSOFromIndex(int kitchenObjectSOIndex) {
        return kitchenObjectListSO.kitchenObjectSOList[kitchenObjectSOIndex];
    }
    
   
}
