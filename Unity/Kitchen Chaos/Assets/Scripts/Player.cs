using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Netcode;
using UnityEngine;


public class Player : NetworkBehaviour, IKitchenObjectParent {

    public static event EventHandler OnAnyPlayerSpawned;
    public static event EventHandler OnAnyPickedSomething;
    public static void ResetStaticData() {
        OnAnyPlayerSpawned = null;
    }

    public static Player LocalInstance { get; private set; }


    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    //Gives access to movespeed directly via Unity
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private LayerMask collisionsLayerMask;
    [SerializeField] private Transform KitchenObjectHoldPoint;
    [SerializeField] private List<Vector3> spawnPositionList;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    public override void OnNetworkSpawn() {
        if (IsOwner) { 
        LocalInstance = this;
        }

        transform.position = spawnPositionList[KitchenGameMultiplayer.Instance.GetPlayerDataIndexFromClientId(OwnerClientId)];

        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);

        if (IsServer) {
            NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        }
    }

    private void NetworkManager_OnClientConnectedCallback(ulong clientID) {
        if (clientID == OwnerClientId && HasKitchenObject()) {
            KitchenObject.DestroyKitchenObject(GetKitchenObject());
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        //If IsGamePlaying not true,cannot do any interactions
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        //If IsGamePlaying not true,cannot do any interactions
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        //Checks if it's local input
        if (!IsOwner) {
            return;
        }
        HandleMovement();
        HandleInteractions();
        
    }

    //returns the value of walking to another object, referenced in player_controller
    public bool IsWalking() {
        return isWalking;
    }


    //Bad code example, change later as it is used by the event OnInteractAction instead
    private void HandleInteractions() {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        //Was changed from Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); to current
        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);

        //Check last interact distance
        //Changed from moveDir != Vector3.zero to moveDir.sqrMagnitude > .01f in attempt to fix vieing vector is zero
        if (moveDir.sqrMagnitude > .01f) {
            lastInteractDir = moveDir;
        }

        //Direction check of the object infront of, using raycas
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {

                //clearCounter.Interact(); // Commented out to not work
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }

                } else {
                SetSelectedCounter(null);

                }
        } else {
            SetSelectedCounter(null);

        }
        
    }

    //Function for movement
    private void HandleMovement() {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        //Was changed from Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); to current
        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRaidus = .7f;
        //float playerHeight = 2f;
        bool canMove = !Physics.BoxCast(transform.position, Vector3.one * playerRaidus, moveDir, Quaternion.identity, moveDistance, collisionsLayerMask);

        //Physicis check
        if (!canMove) {
            //Cannot move towards moveDir

            //Attempt move x only
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.BoxCast(transform.position, Vector3.one * playerRaidus, moveDirX, Quaternion.identity, moveDistance, collisionsLayerMask);

            if (canMove) {
                //Can only move X
                moveDir = moveDirX;
            } else {
            //Cannot move on only the x

            //Attempt to move on the z
            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
               canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.BoxCast(transform.position, Vector3.one * playerRaidus, moveDirZ, Quaternion.identity, moveDistance, collisionsLayerMask);
                if (canMove) {
                    //Can only move z
                    moveDir = moveDirZ;
                } else {
                    //Cannot move in any direction
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        //Tests to see if the player is walking or not
        isWalking = moveDir != Vector3.zero;

        //Direction facing of the player with interplation
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedArgs {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform() {
        return KitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
            OnAnyPickedSomething?.Invoke(this, EventArgs.Empty);

        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
    public NetworkObject GetNetworkObject() {
        return NetworkObject;
    }

}




//_______________________SAVED CODE____________________________//

//Selected counter code, old

/*Vector2 inputVector = gameInput.GetMovementVectorNormalized();
Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

//Check last interact distance
if (moveDir != Vector3.zero) {
    lastInteractDir = moveDir;
}

//Direction check of the object infront of, using raycas
float interactDistance = 2f;
if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
    if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
        //Has clearCounter
        clearCounter.Interact();
    }
}*/