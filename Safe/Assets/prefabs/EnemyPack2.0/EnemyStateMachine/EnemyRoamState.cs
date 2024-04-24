
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Xml;

public class EnemyRoamState : EnemyBaseState
{
    public GameObject enemyRigidbodyObject;

    public override void EnterState(EnemyStateMAnager enemy)
    {
        Debug.Log("roaming");
        enemyRigidbodyObject = GameObject.FindGameObjectWithTag("enemy");
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




