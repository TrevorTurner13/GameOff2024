using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //EnemyPatrol enemyPatrol;

    public float health = 100f;  // Health of the object

    public Animator animator;

    public EnemyController enemyController;
    private CircleCollider2D col;
    private Rigidbody2D rb;

    public bool isDead;

    private void Start()
    {
        col = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        //enemyPatrol = GetComponent<EnemyPatrol>();
    }
    // Method to apply damage to this object
    public void TakeDamage(float damage)
    {
        health -= damage;

        // Check if health is below zero and destroy the object if it's dead
        if (health <= 0f)
        {
            //enemyPatrol.isDead = true;
            animator.SetBool("IsDead", true);
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        rb.bodyType = RigidbodyType2D.Static;
        col.isTrigger = true;
        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
