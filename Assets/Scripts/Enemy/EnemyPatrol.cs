using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public EnemyController enemyController;

    public AIChase aiChase;
    public GameObject pointA;
    public GameObject pointB;

    private Rigidbody2D rb;

    public Animator animator;

    private Transform currentPoint;

    public float speed;

    public bool isIdle;
    public bool isDead;
    public bool isFacingRight;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        animator.SetBool("IsRunning", true);

    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        isFacingRight = !isFacingRight;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && !aiChase.isChasing)
        {
            //animator.SetBool("IsAttacking", false);
            Patrol();
        }
        if (rb.velocity.x > 0 && !isFacingRight)
        {
            flip();
        }
        else if (rb.velocity.x < 0 && isFacingRight)
        {
            flip();
        }
    }

    IEnumerator WaitIdle()
    {
  

        animator.SetBool("IsRunning", false);

            rb.velocity = Vector2.zero;

            isIdle = true;
            

            yield return new WaitForSeconds(1);

            

            if (currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
            }
            else
            {
                currentPoint = pointA.transform;
            }

            animator.SetBool("IsRunning", true);

            isIdle = false;



    }

    public void Patrol()
    {
        enemyController.currentState = EnemyController.aiStates.Patrol;

        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint == pointB.transform && !isIdle)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else if (currentPoint == pointA.transform && !isIdle)
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform && !isIdle)
        {
            StartCoroutine(WaitIdle());

        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform && !isIdle)
        {
            StartCoroutine(WaitIdle());
        }
    }
}
