using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    [SerializeField] private AudioClip[] hurtSFX;

    public int currentHealth;

    public Animator animator;

    public Healthbar healthBar;

    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = playerHealth;
        healthBar.SetMaxHealth(playerHealth);
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
        if (playerHealth > 0)
        {
            playerHealth -= 10;
            currentHealth = playerHealth;
            animator.SetTrigger("isHurt");
            SFXManager.instance.PlayRandomSFXClip(hurtSFX, transform, 0.1f);
            healthBar.SetHealth(currentHealth);
        }

    }

    public void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isFalling", false);
    }
}
