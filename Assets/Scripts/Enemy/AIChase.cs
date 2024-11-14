using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float rayDistance;
    public EnemyController enemyController;
    private Rigidbody2D rb;
    private Animator animator;
    private EnemyPatrol enemyPatrol;

    public GameObject player;
    public float chaseSpeed;

    private float distance;
    public float chaseDistance;

    [SerializeField] private LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            animator.SetBool("IsGrounded", true);
        }
        else
        {
            animator.SetBool("IsGrounded", false);
        }
        if (enemyController.currentState == EnemyController.aiStates.Chasing)
        {
            if(CheckWall())
            {  
                Jump();
            }
            
            if (CheckDistance())
            {
                animator.SetBool("IsRunning", true);
                if (rb.velocity.x > 0 && !enemyPatrol.IsFacingRight)
                {
                    Flip();
                }
                else if (rb.velocity.x < 0 && enemyPatrol.IsFacingRight)
                {
                    Flip();
                }
                Chase();
            }  
        }
    }

    public void Chase()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
            
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        enemyPatrol.IsFacingRight = !enemyPatrol.IsFacingRight;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            animator.SetTrigger("IsJumping");
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
            Debug.Log("Jump!");
        }
       
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast((Vector2)transform.position + (Vector2.down / 1.8f), new Vector2(0.5f, 0.05f), 0, -Vector2.up, 0.1f, groundMask);
    }

    public bool CheckWall()
    {
        RaycastHit2D hit;
        if (!enemyPatrol.IsFacingRight)
        {
            hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), rayDistance, groundMask);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), rayDistance, groundMask);
        }

        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckDistance()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < chaseDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + (Vector2.down / 1.8f), new Vector2(0.5f, 0.05f));
         
    }
}
