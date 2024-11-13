using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyPatrol enemyPatrol;
    public EnemyDamage enemyDamage;
    public EnemyHealth enemyHealth;
    //public AIChase aiChase;


    public enum aiStates
    {
        Patrol, 
        Attacking,
        Chasing,
        Death
    }

    public aiStates currentState = aiStates.Patrol;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case aiStates.Patrol:
                enemyPatrol.Patrol();
                break;

            case aiStates.Attacking:
                //enemyDamage.Attack();

                break;

            case aiStates.Chasing:
                //aiChase.Chase();
                break;

            case aiStates.Death:

                break;
        }
    }
}
