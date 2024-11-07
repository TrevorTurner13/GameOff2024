using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public EnemyDamage enemyDamage;

    public GameObject player;
    public float chaseSpeed;

    private float distance;
    public float distanceBetween;

    public bool isChasing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (isChasing && !enemyDamage.isAttacking)
        {
            enemyDamage.isAttacking = false;

            if (transform.position.x > player.transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += Vector3.left * chaseSpeed * Time.deltaTime;
            }
            if (transform.position.x < player.transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position += Vector3.right * chaseSpeed * Time.deltaTime;
            }

        }
        else
        {
            if (distance < distanceBetween)
            {
                isChasing = true;
            }
            if (distance > distanceBetween)
            {
                isChasing = false;

            }

        }


    }
}