using System.Collections;
using System.Threading;
using UnityEngine;



/// <summary>
/// lighting bolt will check for collision with enemy and wall and return to pool if it hits either.
/// If no collision is detected, the bolt will return to the pool after a set amount of time.
/// </summary>

public class lightningDestroy : MonoBehaviour
{
    //sets a timer base that the object can set it's timer to when it is reactivated
    public float originalTimer = 5f;
    public float timer;
    private bool isReturned = false;

    public void OnEnableLightning()
    {
        timer = originalTimer;
        isReturned = false;
        StartCoroutine(ReturnToPoolAfterTime());
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        // Cast a ray forward from the current position
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            // Check if the ray hit an object tagged as "enemy"
            if (hit.collider.CompareTag("enemy"))
            {
                // Get the EnemyStats component from the collided enemy
                EnemyStats enemyStats = hit.collider.gameObject.GetComponent<EnemyStats>();

                // Check if the EnemyStats component exists
                if (enemyStats != null)
                {
                    // Apply damage to the enemy
                    enemyStats.TakeDamage(1);
                    Debug.Log("Enemy hit by lightning bolt!");
                }

                // Return the lightning bolt to the pool
                ReturnToPool();
            }
            else if (hit.collider.CompareTag("wall"))
            {
                // Return the lightning bolt to the pool if it hits a wall
                ReturnToPool();
            }
        }
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        yield return new WaitForSeconds(timer);
        if (!isReturned)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        isReturned = true;
        objPoolManager.ReturnObjectToPool(gameObject);
    }
}
