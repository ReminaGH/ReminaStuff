using Mono.CSharp;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KitchenObject : NetworkBehaviour {

    [SerializeField] private KitchenObjSO kitchenObjSO;

    private IKitchenObjectParent kitcheObjectParent;
    private FollowTransform followTransform;

    protected virtual void Awake() {
        followTransform = GetComponent<FollowTransform>();
    }

    public KitchenObjSO GetKitchenObjSO() {
        return kitchenObjSO;

    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        SetKitchenObjectParentServerRpc(kitchenObjectParent.GetNetworkObject());
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetKitchenObjectParentServerRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference) {
        SetKitchenObjectParentClientRpc(kitchenObjectParentNetworkObjectReference);
    }

    [ClientRpc]
    private void SetKitchenObjectParentClientRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference) {
        kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
        IKitchenObjectParent kitchenObjectParent = kitchenObjectParentNetworkObject.GetComponent<IKitchenObjectParent>();

        if (this.kitcheObjectParent != null) {
            this.kitcheObjectParent.ClearKitchenObject();

        }
        this.kitcheObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("kitchenObjectParent already has kitchenObject!");
        }
        kitchenObjectParent.SetKitchenObject(this);

        followTransform.SetTargetTransform(kitchenObjectParent.GetKitchenObjectFollowTransform());
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitcheObjectParent;
    
    }

    public void DestroySelf() {
       Destroy(gameObject);
    }

    public void ClearKitchenObjectOnParent() {
        kitcheObjectParent.ClearKitchenObject();
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if (this is PlateKitchenObject) { 
            plateKitchenObject = this as PlateKitchenObject;
            return true;

        } else {
            plateKitchenObject = null;  
            return false; 
        }
    
    }

    public static void SpawnKitchenObject(KitchenObjSO kitchenObjSO, IKitchenObjectParent kitchenObjectParent) {
        KitchenGameMultiplayer.Instance.SpawnKitchenObject(kitchenObjSO, kitchenObjectParent);
    }

    public static void DestroyKitchenObject(KitchenObject kitchenObject) {
        KitchenGameMultiplayer.Instance.DestroyKitchenObject(kitchenObject);
    }
}



/* SAVED CODE 
 
Not used in multiplayer
//transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
//transform.localPosition = Vector3.zero;  
 

/*if (this.kitcheObjectParent != null) {
            this.kitcheObjectParent.ClearKitchenObject();

        }
        this.kitcheObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("kitchenObjectParent already has kitchenObject!");
        }
        kitchenObjectParent.SetKitchenObject(this);

        followTransform.SetTargetTransform(kitchenObjectParent.GetKitchenObjectFollowTransform());*/

