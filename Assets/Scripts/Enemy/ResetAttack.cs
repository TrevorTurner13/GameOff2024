using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAttack : MonoBehaviour
{
    private EnemyDamage enemyDamage;



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
        enemyDamage.ResetAttack();

    }
}
