using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //EnemyPatrol enemyPatrol;

    public float health = 100f;  // Health of the object

    public Animator animator;

    public EnemyController enemyController;

    public bool isDead;

    private void Start()
    {
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
        enemyController.currentState = EnemyController.aiStates.Death;
        StartCoroutine(WaitForAnimation());
        isDead = true;
    }

    IEnumerator WaitForAnimation()
    {

        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }
}
