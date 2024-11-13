using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float maxHorizontalVelocity;
    [SerializeField] private float maxVerticalVelocity;
    [SerializeField] private AudioClip[] jumpSFX;

    private PlayerRigidbodyHandler rbHandler;
    private Rigidbody2D rb;

    private Animator animator;

    public PlayerHealth playerHealth;

    public DialogueTrigger dialogueTrigger;

    private float horizontal;
    private float vertical;
    private float moveSpeed;
    private float jumpForce;

    public float MoveSpeed { get { return moveSpeed; } set {  moveSpeed = value; } }
    public float JumpForce { get {  return jumpForce; } set {  jumpForce = value; } }

    private bool isGrounded;

    public bool IsFacingRight { get; private set; }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rbHandler = GetComponent<PlayerRigidbodyHandler>();
        rbHandler.SwapGravity(rbHandler.AntiGravityOn);

        animator.SetBool("isGrounded", true);

    }

    private void Update()
    {
        if (!playerHealth.isDead && !dialogueTrigger.isTransmitting)
        {
            if (horizontal > 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                IsFacingRight = true;
            }
            else if (horizontal < 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                IsFacingRight = false;
            }

            IsGrounded();

            if (!rbHandler.AntiGravityOn || isGrounded)
            {


                rb.velocity = new Vector3(horizontal * moveSpeed, rb.velocity.y, 0);
            }

            if (!isGrounded)
            {
                animator.SetBool("isGrounded", false);
                animator.SetBool("isRunning", false);
            }
            if (isGrounded && rb.velocity.x != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else if (isGrounded && rb.velocity.x == 0)
            {
                animator.SetBool("isRunning", false);
            }
            if (isGrounded)
            {
                animator.SetBool("isGrounded", true);
                animator.SetBool("isFalling", false);
            }
            if (!isGrounded && rb.velocity.y < 0)
            {
                animator.SetBool("isFalling", true);
            }
        }

    }

    public void IsGrounded()
    {
        isGrounded = Physics2D.BoxCast(groundCheck.position, new Vector2(0.5f, 0.1f), 0, -groundCheck.up, 0.1f, groundMask);
    }

    public void Move(InputAction.CallbackContext context)
    {

        horizontal = context.ReadValue<Vector2>().x;

        if (!playerHealth.isDead && !dialogueTrigger.isTransmitting)
        {
            if (rbHandler.AntiGravityOn && context.performed)
            {
                vertical = context.ReadValue<Vector2>().y;
                rb.AddForce(new Vector2(horizontal * (moveSpeed / 3), vertical * (moveSpeed / 2)), ForceMode2D.Impulse);
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxHorizontalVelocity, maxHorizontalVelocity),
                                          Mathf.Clamp(rb.velocity.y, -maxVerticalVelocity, maxVerticalVelocity));
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!playerHealth.isDead && !dialogueTrigger.isTransmitting)
        {
            if (isGrounded && context.performed)
            {
                SFXManager.instance.PlayRandomSFXClip(jumpSFX, transform, 1f);

                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
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
