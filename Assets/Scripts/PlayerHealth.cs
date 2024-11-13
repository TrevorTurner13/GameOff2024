using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHealth;


    public Animator animator;

    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage()
    {
        playerHealth -= 10;
        animator.SetTrigger("isHurt");
    }

    public void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isFalling", false);
    }
}
