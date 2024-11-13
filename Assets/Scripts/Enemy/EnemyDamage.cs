using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private EnemyPatrol enemyPatrol;
    [SerializeField] private AudioClip enemyAttackSFX;

    private float cooldownTimer = Mathf.Infinity;

    //public int damage;

    public Animator animator;

    public Rigidbody2D rb;

    public bool isAttacking;

    public EnemyController enemyController;

    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInRange())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                enemyController.currentState = EnemyController.aiStates.Attacking;
                SFXManager.instance.PlaySFXClip(enemyAttackSFX, transform, 0.1f);

                Attack();
            }
        }
        else
        {
            isAttacking = false;
            enemyController.currentState = EnemyController.aiStates.Patrol;


        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInRange();
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    if(collision.gameObject.tag == "Player")

    //    {
    //        enemyController.currentState = EnemyController.aiStates.Attacking;
    //        Attack();
    //        rb.velocity = Vector2.zero;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")

    //    {
    //        enemyController.currentState = EnemyController.aiStates.Chasing;

    //        animator.SetBool("IsRunning", true);

    //        animator.SetBool("IsAttacking", false);
            
    //        isAttacking = false;
    //    }
    //}

    public void Attack()
    {
        rb.velocity = Vector2.zero;

        animator.SetBool("IsRunning", false);

        animator.SetTrigger("IsAttacking");

        isAttacking = true;

        //player take damage

        playerHealth.TakeDamage();
    }

    private bool PlayerInRange()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        //enemyController.currentState = EnemyController.aiStates.Attacking;

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void ResetAttack()
    {
        isAttacking = false;
    }
}
