using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float moveSpeed = 3f;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;

        playerInputActions.Player.Movement.performed += Move;
    }

    private void FixedUpdate() {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        rb.AddForce(inputVector * moveSpeed, ForceMode2D.Force);
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Debug.Log(inputVector);
        rb.AddForce(inputVector * moveSpeed, ForceMode2D.Force);
    }

    public void Jump(InputAction.CallbackContext context){
        if(context.performed){
            Debug.Log("JUMP");
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }
}
