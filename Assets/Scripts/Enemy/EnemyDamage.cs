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

    private Animator animator;

    private Rigidbody2D rb;

    public bool isAttacking;

    private EnemyController enemyController;

    private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyController = GetComponent<EnemyController>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (enemyController.currentState == EnemyController.aiStates.Attacking && !isAttacking)
        {
            if (PlayerInRange() && cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                isAttacking = true;
            }
            if (isAttacking)
            {
                SFXManager.instance.PlaySFXClip(enemyAttackSFX, transform, 0.1f);
                Attack();
            }
        }
    }
    public void Attack()
    {
        rb.velocity = Vector2.zero;

        animator.SetBool("IsRunning", false);

        animator.SetTrigger("IsAttacking");
    }

    public bool PlayerInRange()
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
