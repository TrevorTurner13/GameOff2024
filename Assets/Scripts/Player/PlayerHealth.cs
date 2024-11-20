using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] private AudioClip[] hurtSFX;
    [SerializeField] private Vector3 respawnPosition;

    public int currentHealth;

    public Animator animator;

    public Healthbar healthBar;

    public bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        respawnPosition = transform.position;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            animator.SetTrigger("isHurt");
            SFXManager.instance.PlayRandomSFXClip(hurtSFX, transform, 0.1f);
            currentHealth -= 10;
            healthBar.SetHealth(currentHealth);
        }

    }

    public void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isFalling", false);

        if (isDead)
        {
            Respawn();
        }
    }

    public void Respawn()
    {

        StartCoroutine(WaitForDeath());
          
    }


    IEnumerator WaitForDeath()
    {
        currentHealth = maxHealth;

        yield return new WaitForSeconds(3);

        animator.SetBool("isDead", false);
        isDead = false;
        transform.position = respawnPosition;
        healthBar.SetMaxHealth(maxHealth);

    }

}
