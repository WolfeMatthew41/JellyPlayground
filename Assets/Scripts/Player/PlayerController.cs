using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    [SerializeField] private bool onAir = false;
    [SerializeField] private bool movingRight = false;
    [SerializeField] private bool movingLeft = false;
    private bool isColliding = false;
    private bool isVertical = false;

    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float gravityValue = 500f;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Movement.performed += Move;
        playerInputActions.Player.Switch.performed += Switch;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        isColliding = false;
    }

    private void FixedUpdate() {
        if(!(onAir && isColliding)){
            Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
            rb.velocity = new Vector2(inputVector.x * moveSpeed, rb.velocity.y);
        }
        ApplyGravity();
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        rb.velocity = new Vector2(inputVector.x * moveSpeed, rb.velocity.y);
        if(rb.velocity.x > 0f){
            movingRight = true;
            movingLeft = false;
        } else if(rb.velocity.x < 0f){
            movingLeft = true;
            movingRight = false;
        }
    }

    public void Jump(InputAction.CallbackContext context){
        if(context.performed && rb.velocity.y == 0){
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            onAir = true;
        }
    }

    private void Switch(InputAction.CallbackContext context){
        if(isVertical){
            anim.SetTrigger("SwitchHorizontal");
            isVertical = false;
        }else{
            if(movingLeft){
                anim.SetTrigger("SwitchVerticalLeft");
            }else{
                anim.SetTrigger("SwitchVerticalRight");
            }
            isVertical = true;
        }
    }

    private void ApplyGravity(){
        onAir = rb.velocity.y != 0;
        if(onAir){
            rb.AddForce(new Vector2(0, -1f * gravityValue * Time.deltaTime), ForceMode2D.Force);
        }
    }
}
