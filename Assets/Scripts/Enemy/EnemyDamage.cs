using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;

    public Animator animator;

    public Rigidbody2D rb;

    public bool isAttacking;

    public EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Player")

        {
            enemyController.currentState = EnemyController.aiStates.Attacking;
            Attack();
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")

        {
            enemyController.currentState = EnemyController.aiStates.Chasing;

            animator.SetBool("IsRunning", true);

            animator.SetBool("IsAttacking", false);
            
            isAttacking = false;
        }
    }

    public void Attack()
    {
        //rb.velocity = Vector2.zero;

        animator.SetBool("IsRunning", false);

        animator.SetBool("IsAttacking", true);

        isAttacking = true;

        //player take damage

        //playerHealth.TakeDamage();
    }


}
