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

    private bool onAir = false;
    private bool movingRight = false;
    private bool movingLeft = false;
    private bool isColliding = false;
    private bool isVertical = false;
    private bool isDashing = false;
    private bool canDash = true;
    private string currentAnim;

    [SerializeField] private List<GameObject> detectors;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float gravityValue = 500f;
    [SerializeField] private float dashIntensity = 5f;
    [SerializeField] private float extraJumpIntensity = 2f;
    [SerializeField] private float dashCooldown = 1f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Movement.performed += Move;
        playerInputActions.Player.Switch.performed += Switch;
        playerInputActions.Player.Dash.performed += Dash;
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
            ApplyMovement(inputVector);
        }
        if(isDashing){
            StartCoroutine(ApplyDash());
        }
        
        ApplyGravity();
    }

    private void Move(InputAction.CallbackContext context)
    {
        if(!(onAir && isColliding)){
            Vector2 inputVector = context.ReadValue<Vector2>();
            ApplyMovement(inputVector);
        }

        if(rb.velocity.x > 0f){
            movingRight = true;
            movingLeft = false;
        } else if(rb.velocity.x < 0f){
            movingLeft = true;
            movingRight = false;
        }
    }

    private void ApplyMovement(Vector2 inputVector){
        rb.velocity = new Vector2(inputVector.x * moveSpeed, rb.velocity.y);
    }

    public void Jump(InputAction.CallbackContext context){
        if(context.performed && rb.velocity.y == 0){
            if(isVertical){
                rb.AddForce(Vector2.up * jumpHeight * extraJumpIntensity, ForceMode2D.Impulse);
            } else{
                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            }
            onAir = true;
        }
    }

    private void Switch(InputAction.CallbackContext context){
        if(!CanSwitch()) 
            return;

        if(isVertical){
            anim.SetTrigger("SwitchHorizontal");
            isVertical = false;
        }else{
            if(movingLeft){
                anim.SetTrigger("SwitchVerticalLeft");
                currentAnim = "SwitchVerticalLeft";
            }else{
                anim.SetTrigger("SwitchVerticalRight");
                currentAnim = "SwitchVerticalRight";
            }
            isVertical = true;
        }
    }

    private bool CanSwitch(){
        return true;
    }

    private void Dash(InputAction.CallbackContext context){
        if(!isVertical && canDash){
            isDashing = true;
        }
    }

    private IEnumerator ApplyDash(){
        canDash = false;
        if(movingLeft){
            rb.AddForce(Vector2.left * dashIntensity, ForceMode2D.Impulse);
        } else{
            rb.AddForce(Vector2.right * dashIntensity, ForceMode2D.Impulse);
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void ApplyGravity(){
        onAir = rb.velocity.y != 0;
        if(onAir){
            rb.AddForce(new Vector2(0, -1f * gravityValue * Time.deltaTime), ForceMode2D.Force);
        }
    }
}
