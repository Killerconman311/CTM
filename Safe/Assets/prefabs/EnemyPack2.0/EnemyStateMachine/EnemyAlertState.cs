using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlertState : EnemyBaseState
{
    public override void EnterState(EnemyStateMAnager enemy)
    {
        // Implement behavior for entering alert state
        enemy.SetAlertBehavior();
        // Implement behavior for entering alert state
    }

    public override void UpdateState(EnemyStateMAnager enemy)
    {
        // Implement behavior for updating alert state
    }

    public override void OnCollisionEnter(EnemyStateMAnager enemy, Collider other)
    {
        // Implement behavior for collision detection in alert state
    }

    public override void OnTriggerEnter(EnemyStateMAnager enemy, Collider other)
    {
        // Implement behavior for trigger detection in alert state
    }

    public override void Attack(EnemyStateMAnager enemy)
    {
        // Implement behavior for attacking in alert state
    }

    public override void EnterAlertState(EnemyStateMAnager enemy)
    {
        // Implement behavior for entering alert state
        enemy.SetAlertBehavior(); // For example, reduce speed
    }

    public override void CheckSurroundings(EnemyStateMAnager enemy)
    {
        // Implement behavior for checking surroundings in alert state
        if (enemy.CanSeePlayer())
        {
            enemy.SwitchState(enemy.fightState); // Transition to fight state if player is visible
        }
        else
        {
            enemy.SwitchState(enemy.roamState); // Transition back to roam state if player is not visible
        }
    }
}
