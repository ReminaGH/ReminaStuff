using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance { get; private set; }

    public event EventHandler<OnSelectedCabinetChangedEventArgs> OnSelectedCabinetChanged;
    public class OnSelectedCabinetChangedEventArgs : EventArgs {
        public BaseCabinet selectedCabinet;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private bool gamePasued = false;
    private bool isWalking;
    private Vector3 lastInteractionDir;
    private BaseCabinet selectedCabinet;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than 1 player instance");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAltAction += GameInput_OnInteractAltAction;
    }

    private void GameInput_OnInteractAltAction(object sender, EventArgs e) {
        if (selectedCabinet != null && gamePasued == false) {
            selectedCabinet.InteractAlt();
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCabinet != null && gamePasued == false) {
            selectedCabinet.Interact();
        }
    }

    private void Update() {
        if (gamePasued == false) {
            HandleMovement();
            HandleInteractions();
        }
    }
    
    public bool IsWalking() { return isWalking; }

    private void HandleInteractions() { 
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
        lastInteractionDir = moveDir;
        }

        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactionDistance)) {
            if (raycastHit.transform.TryGetComponent(out BaseCabinet baseCabinet)) {
                //Has BaseCabinet
                if (baseCabinet != selectedCabinet) {
                    SetSelectedCabinet(baseCabinet);
                }
            }
            else {
                SetSelectedCabinet(null);
            }
        }
        else {
            SetSelectedCabinet(null);
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            //Cannot move towards moveDir

            //Attempt onl x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                //Can move only to the X
                moveDir = moveDirX;
            }
            else {
                //Cannot move only on the X

                //Attempt to move only Z
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    //Can move only to the Z
                    moveDir = moveDirZ;
                }
                else {
                    //Cannot move in any direction
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCabinet(BaseCabinet selectedCabinet) {
        this.selectedCabinet = selectedCabinet;

        OnSelectedCabinetChanged?.Invoke(this, new OnSelectedCabinetChangedEventArgs {
            selectedCabinet = selectedCabinet
        });
    }

    public void gamePausedToggle() {
        
        gamePasued = !gamePasued;
    } 

}

