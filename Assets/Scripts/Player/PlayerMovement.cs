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
    private CoyoteTimeHandler coyoteTimeHandler;

    private Animator animator;

    public PlayerHealth playerHealth;

    public DialogueTrigger dialogueTrigger;

    public Canvas pauseMenu;

    private float horizontal;
    private float vertical;
    private float moveSpeed;
    private float jumpForce;

    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }

    public bool IsFacingRight { get; private set; }

    //public static bool isPaused;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rbHandler = GetComponent<PlayerRigidbodyHandler>();
        rbHandler.SwapGravity(rbHandler.AntiGravityOn);
        IsFacingRight = true;
        animator.SetBool("isGrounded", true);
        coyoteTimeHandler = GetComponent<CoyoteTimeHandler>();
    }

    private void Update()
    {
        if (!playerHealth.isDead && !dialogueTrigger.isTransmitting)
        {
            if (horizontal > 0 && !IsFacingRight)
            {
                Flip();
            }
            else if (horizontal < 0 && IsFacingRight)
            {
                Flip();
            }

            if (!rbHandler.HasJetPack || IsGrounded())
            {
                rb.velocity = new Vector3(horizontal * moveSpeed, rb.velocity.y, 0);
            }

            if(rb.velocity.y < 0 && !coyoteTimeHandler.CoyoteTime)
            {
                rbHandler.FallGravity();
            }

            if (!IsGrounded())
            {
                animator.SetBool("isGrounded", false);
                animator.SetBool("isRunning", false);
            }

            if (IsGrounded() && rb.velocity.x != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else if (IsGrounded() && rb.velocity.x == 0)
            {
                animator.SetBool("isRunning", false);
            }

            if (IsGrounded())
            {
                rbHandler.ResetGravity();
                animator.SetBool("isGrounded", true);
                animator.SetBool("isFalling", false);
            }

            if (!IsGrounded() && rb.velocity.y < 0)
            {
                animator.SetBool("isFalling", true);
            }
            else if (!IsGrounded() && rb.velocity.y > 0)
            {
                animator.SetBool("isFalling", false);
            }
        }

    }

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(groundCheck.position, new Vector2(0.2f, 0.05f), 0, -groundCheck.up, 0.1f, groundMask);
    }

    public void Move(InputAction.CallbackContext context)
    {

        horizontal = context.ReadValue<Vector2>().x;

        if (!playerHealth.isDead && !dialogueTrigger.isTransmitting)
        {
            if (rbHandler.AntiGravityOn && context.performed && rbHandler.HasJetPack)
            {
                vertical = context.ReadValue<Vector2>().y;
                rb.AddForce(new Vector2(horizontal * (moveSpeed / 2), vertical * (moveSpeed / 2)), ForceMode2D.Impulse);
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxHorizontalVelocity, maxHorizontalVelocity),
                                          Mathf.Clamp(rb.velocity.y, -maxVerticalVelocity, maxVerticalVelocity));
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!playerHealth.isDead && !dialogueTrigger.isTransmitting)
        {
            if (IsGrounded() && context.performed || coyoteTimeHandler.CoyoteTime && context.performed)
            {
                SFXManager.instance.PlayRandomSFXClip(jumpSFX, transform, 1f);
                rb.velocity = new Vector2(rb.velocity.x, 0);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(0.2f, 0.05f));
    }

    public void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Pause Game");

        if (!PauseMenu.isPaused && context.performed)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            PauseMenu.isPaused = true;
            AudioListener.pause = true;
        }

        else if (PauseMenu.isPaused && context.performed)
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
            PauseMenu.isPaused = false;
            AudioListener.pause = false;

        }
    }
}
