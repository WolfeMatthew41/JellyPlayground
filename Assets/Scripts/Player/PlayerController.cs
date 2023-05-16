using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private Moving_Platform movingPlatform;

    private bool onAir = false;
    private bool movingRight = false;
    private bool movingLeft = false;
    private bool isColliding = false;
    private bool isVertical = false;
    private bool isDashing = false;
    private bool dashCooldownOver = true;
    private bool isRiding = false;
    private bool inWater = false;

    private string currentAnim;
    private int coinsCollected;


    [Header("References")]
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject verticalDetectorLeft;
    [SerializeField] private GameObject verticalDetectorRight;
    [SerializeField] private GameObject horizontalDetectorLeft;
    [SerializeField] private GameObject horizontalDetectorRight;
    [SerializeField] private GameObject dashDetectorLeft;
    [SerializeField] private GameObject dashDetectorRight;

    [Header("Adjustable gameplay values")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float gravityValue = 500f;
    [SerializeField] private float dashIntensity = 5f;
    [SerializeField] private float extraJumpIntensity = 2f;
    [SerializeField] private float dashCooldown = 1f;

    [SerializeField] private float jellyDashOffset = 0.5f;
    [SerializeField] private GameObject jellyDashRight;
    [SerializeField] private GameObject jellyDashLeft;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Movement.performed += Move;
        playerInputActions.Player.Movement.canceled += Idle;
        playerInputActions.Player.Switch.performed += Switch;
        playerInputActions.Player.Dash.performed += Dash;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        isColliding = true;

        if (other.transform.tag == "MovingPlatform")
        {
            movingPlatform = other.gameObject.GetComponent<Moving_Platform>();
        }

        // If player is riding a platform, and with another platform, and is vertical, go into horizontal mode.
        if (other.transform.tag == "Impassable" && isRiding && isVertical)
        {
            anim.SetTrigger("SwitchHorizontal");
            isVertical = false;
        }
        // If player is riding  a platform, collides with another platform, and isn't vertical, reverse direction
        // of the moving platform.
        else if (other.transform.tag == "Impassable" && isRiding && !isVertical)
        {
            movingPlatform.setMovingToEnd(false);
        }
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
        currentAnim = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }

    private void Move(InputAction.CallbackContext context)
    {
        anim.SetBool("Moving", true);
        if(!(onAir && isColliding)){
            Vector2 inputVector = context.ReadValue<Vector2>();
            ApplyMovement(inputVector);
        }
        
        if(rb.velocity.x > 0f){
            movingRight = true;
            movingLeft = false;
            if(!isVertical){
                anim.Play("MovingRight");
            }else{
                if(currentAnim.Equals("IdleVerticalLeft") || currentAnim.Equals("MovingVerticalLeft"))
                    anim.Play("MovingExtraRight");
                else
                    anim.Play("MovingVerticalRight");
                OffsetMovementRight();
            }
        } else if(rb.velocity.x < 0f){
            movingLeft = true;
            movingRight = false;
            if(!isVertical){
                anim.Play("MovingLeft");
            }else{
                if(currentAnim.Equals("IdleVerticalRight") || currentAnim.Equals("MovingVerticalRight"))
                    anim.Play("MovingExtraLeft");
                else
                    anim.Play("MovingVerticalLeft");
                OffsetMovementLeft();
            }
        }
    }

    private void OffsetMovementRight(){
        if(currentAnim.Equals("MovingVerticalLeft") || currentAnim.Equals("IdleVerticalLeft") || currentAnim.Equals("IdleExtraLeft") || currentAnim.Equals("MovingExtraLeft")){
            transform.position = new Vector2(transform.position.x - 3f, transform.position.y);
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x + 3f, playerCamera.transform.position.y, playerCamera.transform.position.z);
        }
    }

    private void OffsetMovementLeft(){
        if(currentAnim.Equals("MovingVerticalRight") || currentAnim.Equals("IdleVerticalRight") || currentAnim.Equals("IdleExtraRight") || currentAnim.Equals("MovingExtraRight")){
            transform.position = new Vector2(transform.position.x + 3f, transform.position.y);
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x - 3f, playerCamera.transform.position.y, playerCamera.transform.position.z);
        }
    }

    private void Idle(InputAction.CallbackContext context)
    {
        anim.SetBool("Moving", false);
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
        if(isVertical){
            if(currentAnim.Equals("IdleVerticalLeft") || currentAnim.Equals("MovingVerticalLeft")){
                if(!verticalDetectorLeft.GetComponent<Detector>().NotColliding()) return;
            } else if(currentAnim.Equals("IdleVerticalRight") || currentAnim.Equals("MovingVerticalRight"))
                if(!verticalDetectorRight.GetComponent<Detector>().NotColliding()) return;
            anim.SetTrigger("SwitchHorizontal");
            isVertical = false;
        }else{
            if(movingLeft){
                if(!horizontalDetectorLeft.GetComponent<Detector>().NotColliding()) return;
            }else{
                if(!horizontalDetectorRight.GetComponent<Detector>().NotColliding()) return;
            }
            anim.SetTrigger("SwitchVertical");
            isVertical = true;
        }
    }

    private void Dash(InputAction.CallbackContext context){
        if(!isVertical && dashCooldownOver){
            isDashing = true;
        }
    }

    private IEnumerator ApplyDash(){
        dashCooldownOver = false;
        if(movingLeft){
            if (dashDetectorLeft.GetComponent<Detector>().NotColliding())
            {
                JellyDashAnimation();
                rb.AddForce(Vector2.left * dashIntensity, ForceMode2D.Impulse);
            }
        } else{
            if (dashDetectorRight.GetComponent<Detector>().NotColliding())
            {
                JellyDashAnimation();
                rb.AddForce(Vector2.right * dashIntensity, ForceMode2D.Impulse);
            }
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        dashCooldownOver = true;
    }

    private void ApplyGravity(){
        onAir = rb.velocity.y != 0;
        if(onAir){
            rb.AddForce(new Vector2(0, -1f * gravityValue * Time.deltaTime), ForceMode2D.Force);
        }
    }

    public PlayerInputActions GetPlayerInputActions(){
        return playerInputActions;
    }

    public bool getVertical()
    {
        return isVertical;
    }

    public bool getRiding()
    {
        return isRiding;
    }

    public void setRiding(bool ride)
    {
        isRiding = ride;
    }

    public bool getInWater()
    {
        return inWater;
    }

    public void setInWater(bool inWater)
    {
        this.inWater = inWater;
    }

    public void setJumpHeight(float height)
    {
        jumpHeight = height;
    }

    public float GetDashIntensity(){
        return dashIntensity;
    }
  
    private void JellyDashAnimation() 
    {
        if (movingRight)
        {
            Vector3 playerPos = transform.position;
            Instantiate(jellyDashRight, playerPos, Quaternion.identity);
            playerPos.x += jellyDashOffset;
            Instantiate(jellyDashRight, playerPos, Quaternion.identity);
            playerPos.x += jellyDashOffset;
            Instantiate(jellyDashRight, playerPos, Quaternion.identity);
        }
        else 
        {
            Vector3 playerPos = transform.position;
            Instantiate(jellyDashLeft, playerPos, Quaternion.identity);
            playerPos.x -= jellyDashOffset;
            Instantiate(jellyDashLeft, playerPos, Quaternion.identity);
            playerPos.x -= jellyDashOffset;
            Instantiate(jellyDashLeft, playerPos, Quaternion.identity);
        }

    }

  
}
