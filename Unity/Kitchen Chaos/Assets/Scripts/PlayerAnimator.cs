using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimator : NetworkBehaviour {
    //Takes the string and makes it a const that wont change, added saftey
    private const string IS_WALKING = "IsWalking";

    //Inputs the player_controller to the walking state of the player, IsWalking
    [SerializeField] private Player player;

    //Accesses the animator component
    private Animator animator;

    //Grabs the compontenton start
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    //Ask for the bool vale on every frame from the IsWalking from the Player_controller script
    private void Update() {
        if (!IsOwner) { 
            return; 
        }
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}


