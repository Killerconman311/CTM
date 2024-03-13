using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;



public class EnemyInvisibleState : EnemyBaseState
{
    private MeshRenderer enemyRenderer;
    private float visTime = 4.0f;

    public override void EnterState(EnemyStateMAnager enemy)
    {
        Debug.Log("invis");

        // Get the Renderer component of the enemy
        enemyRenderer = enemy.GetComponent<MeshRenderer>();

        // Check if the enemy has a Renderer component
        if (enemyRenderer != null)
        {
            Debug.Log("I am invisible");
            // Set the renderer to be invisible (disabled)
            enemyRenderer.enabled = false;
            
        }
        else
        {
            Debug.LogWarning("Renderer not found.");
        }
    }
    public override void UpdateState(EnemyStateMAnager enemy)
    {
        visTime -= Time.deltaTime;
        if (visTime <= 0f)
        {
            enemyRenderer.enabled = true;
            enemy.SwitchState(enemy.roamState);
        }
     

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
