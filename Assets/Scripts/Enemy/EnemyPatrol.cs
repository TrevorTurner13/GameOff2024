using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private AudioClip[] growlSFX;
    [SerializeField] private float growlCooldown;

    private EnemyController enemyController;
    private EnemyHealth enemyHealth;

    //public AIChase aiChase;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Animator animator;

    private Transform currentPoint;

    [SerializeField] private float speed;

    public bool IsIdle { get; private set; }
    //public bool isDead;
    public bool IsFacingRight;

    private float growlSFXCooldownTimer = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyController = GetComponent<EnemyController>();
        animator = GetComponentInChildren<Animator>();

        currentPoint = pointB.transform;
        animator.SetBool("IsRunning", true);

    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        IsFacingRight = !IsFacingRight;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.currentState == EnemyController.aiStates.PatrolIdle)
        {
            animator.SetBool("IsRunning", false);

            rb.velocity = Vector2.zero;
        }
        else if (enemyController.currentState == EnemyController.aiStates.PatrolRunning)
        {
            animator.SetBool("IsRunning", true);
            if (rb.velocity.x > 0 && !IsFacingRight)
            {
                Flip();
            }
            else if (rb.velocity.x < 0 && IsFacingRight)
            {
                Flip();
            }
            Patrol();
        }
        growlSFXCooldownTimer += Time.deltaTime;
        if (growlSFXCooldownTimer >= growlCooldown)
        {
            growlSFXCooldownTimer = 0;
            SFXManager.instance.PlayRandomSFXClip(growlSFX, transform, 0.1f);
        } 
    }


    IEnumerator WaitIdle()
    {
        yield return new WaitForSeconds(2);

        if (currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
        else
        {
            currentPoint = pointA.transform;
        }

        IsIdle = false;
    }

    public void Patrol()
    {
        if (!enemyHealth.isDead)
        {
            animator.SetBool("IsRunning", true);
            Vector2 direction = currentPoint.position - transform.position;
            direction.Normalize();
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
            }
            else if (currentPoint == pointA.transform)
            {
                rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                IsIdle = true;
                StartCoroutine(WaitIdle());
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                IsIdle = true;
                StartCoroutine(WaitIdle());
            }
        }
    }
}
