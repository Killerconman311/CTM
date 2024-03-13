using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyFightState : EnemyBaseState
{
    float attackCoolDown;
   
    public override void EnterState(EnemyStateMAnager enemy)
    {

        Debug.Log("fighting");
        attackCoolDown = 4.0f;

    }

    public override void UpdateState(EnemyStateMAnager enemy)
    {
        if (attackCoolDown >= 0 )
        {
            //Countdown for how long fight state stays active
            attackCoolDown -= Time.deltaTime;
            //stops enemy for duration
            enemy.transform.position = enemy.transform.position;

        }
        else
        {
            Debug.Log("fight over");
            enemy.SwitchState(enemy.roamState);
            
        }
    }

    public override void OnCollisionEnter(EnemyStateMAnager enemy, Collider other)
    {
     
    }
    public override void OnTriggerEnter(EnemyStateMAnager enemy, Collider other)
    {
        if (other.CompareTag("spell"))
        {
            enemy.SwitchState(enemy.SprintState);
        }
    }
    public override void Attack(EnemyStateMAnager enemy)
    {

    }
    
}
