using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float maxHorizontalVelocity;
    [SerializeField] private float maxVerticalVelocity;

    private PlayerRigidbodyHandler rbHandler;
    private Rigidbody2D rb;

    private float horizontal;
    private float vertical;
    private float moveSpeed;
    private float jumpForce;

    public float MoveSpeed { get { return moveSpeed; } set {  moveSpeed = value; } }
    public float JumpForce { get {  return jumpForce; } set {  jumpForce = value; } }

    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbHandler = GetComponent<PlayerRigidbodyHandler>();
        rbHandler.SwapGravity(rbHandler.AntiGravityOn);
    }

    private void Update()
    {
        IsGrounded();
        
        if (!rbHandler.AntiGravityOn || isGrounded)
        {
            rb.velocity = new Vector3(horizontal * moveSpeed, rb.velocity.y, 0);
        }
       
    }

    public void IsGrounded()
    {
        isGrounded = Physics2D.BoxCast(groundCheck.position, new Vector2(0.5f, 0.1f), 0, -groundCheck.up, 0.1f, groundMask);
    }

    public void Move(InputAction.CallbackContext context)
    {
       
        horizontal = context.ReadValue<Vector2>().x;
        
        if(rbHandler.AntiGravityOn && context.performed)
        {
            vertical = context.ReadValue<Vector2>().y;
            rb.AddForce(new Vector2(horizontal * (moveSpeed / 2), vertical * (moveSpeed / 2)), ForceMode2D.Impulse);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxHorizontalVelocity, maxHorizontalVelocity),
                                      Mathf.Clamp(rb.velocity.y, -maxVerticalVelocity, maxVerticalVelocity));
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && context.performed) 
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        
    }

    public void GravitySwap(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            rbHandler.SwapGravity(!rbHandler.AntiGravityOn);
        }
    }
}
