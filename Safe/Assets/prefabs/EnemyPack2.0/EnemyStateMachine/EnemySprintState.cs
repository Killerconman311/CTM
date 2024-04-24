using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public class EnemySprintState : EnemyBaseState
{

    public override void EnterState(EnemyStateMAnager enemy)
    {
        Debug.Log("sprinting");

    }

    public override void UpdateState(EnemyStateMAnager enemy)
    {

    }

    public override void OnCollisionEnter(EnemyStateMAnager enemy, Collider other)
    {
       
    }
    public override void OnTriggerEnter(EnemyStateMAnager enemy, Collider other)
    {

    }
    public override void Attack(EnemyStateMAnager enemy)
    {

    }
}
