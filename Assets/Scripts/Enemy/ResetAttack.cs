using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAttack : MonoBehaviour
{
    private EnemyDamage enemyDamage;
    public EnemyController enemyController;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemyDamage = GetComponentInParent<EnemyDamage>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetEnemyAttack()
    {
        animator.SetBool("IsRunning", false);

        enemyDamage.ResetAttack();
        enemyController.currentState = EnemyController.aiStates.Patrol;

    }
}
