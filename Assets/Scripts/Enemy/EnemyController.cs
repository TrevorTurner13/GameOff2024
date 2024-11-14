using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    private EnemyPatrol enemyPatrol;
    private EnemyDamage enemyDamage;
    private EnemyHealth enemyHealth;
    private AIChase enemyChase;
    
    //public AIChase aiChase;


    public enum aiStates
    {
        PatrolIdle,
        PatrolRunning,
        Attacking,
        Chasing,
        Death
    }

    public aiStates currentState = aiStates.PatrolIdle;

    // Start is called before the first frame update
    void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
        enemyDamage = GetComponent<EnemyDamage>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyChase = GetComponent<AIChase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != aiStates.Death)
        {
            if (enemyHealth.isDead && enemyChase.IsGrounded())
            {
                currentState = aiStates.Death;
            }
            else if (enemyDamage.PlayerInRange() && enemyChase.IsGrounded() && currentState != aiStates.Attacking)
            {
                currentState = aiStates.Attacking;
            }
            else if (!enemyDamage.PlayerInRange() && enemyChase.CheckDistance() && !enemyDamage.isAttacking && currentState != aiStates.Chasing)
            {
                currentState = aiStates.Chasing;
            }
            else if (!enemyDamage.PlayerInRange() && !enemyChase.CheckDistance() && !enemyDamage.isAttacking && !enemyPatrol.IsIdle && currentState != aiStates.PatrolRunning)
            {
                currentState = aiStates.PatrolRunning;
            }
            else if (!enemyDamage.PlayerInRange() && !enemyChase.CheckDistance() && !enemyDamage.isAttacking && enemyPatrol.IsIdle && currentState != aiStates.PatrolIdle)
            {
                currentState = aiStates.PatrolIdle;
            }
           
            //Debug.Log(currentState);
        }
    }
}
