using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public abstract class EnemyBaseState 
{
    public abstract void EnterState(EnemyStateMAnager enemy);

    public abstract void UpdateState(EnemyStateMAnager enemy);

    public abstract void OnCollisionEnter(EnemyStateMAnager enemy, Collider other);
    
    public abstract void OnTriggerEnter(EnemyStateMAnager enemy, Collider other);

    public abstract void Attack(EnemyStateMAnager enemy);


}

