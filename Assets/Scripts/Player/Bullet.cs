using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 20f;
    public Rigidbody2D rb;

    private Vector2 shootDirection;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if ( playerMovement != null)
        {
            shootDirection = playerMovement.IsFacingRight ? transform.right : -transform.right;
        }

        rb.velocity = shootDirection * speed;

    }

    void Update()
    {
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {

        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision object has a Health component
        EnemyHealth targetHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (targetHealth != null)
        {
            // Deal damage to the object
            targetHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
