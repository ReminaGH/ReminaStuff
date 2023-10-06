using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {

    [SerializeField] private KitchenObjSO kitchenObjSO;

    private IKitchenObjectParent kitcheObjectParent;
        
    public KitchenObjSO GetKitchenObjSO() {
        return kitchenObjSO;

    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (this.kitcheObjectParent != null) {
            this.kitcheObjectParent.ClearKitchenObject();

        }
        this.kitcheObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("kitchenObjectParent already has kitchenObject!");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitcheObjectParent;
    
    }

    public void DestroySelf() {
        kitcheObjectParent.ClearKitchenObject();

        Destroy(gameObject);
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

    public static KitchenObject SpawnKitchenObject(KitchenObjSO kitchenObjSO, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjSO.prefab);

        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

}
