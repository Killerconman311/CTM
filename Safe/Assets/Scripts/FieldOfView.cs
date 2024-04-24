using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    //variable to set last known position for the FOV to ensure enemies dont lose track of the player
    public Vector3 lastKnownPlayerPosition;
    public bool canSeePlayer;

    //rb for enemy to turn
    private Rigidbody enemyRigidbody;
    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.1f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    lastKnownPlayerPosition = target.position;
                    RotateTowards(target.position);
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            RotateTowards(lastKnownPlayerPosition);
        }
    }

    void RotateTowards(Vector3 targetPosition)
    {
        // Check if the target position is significantly different from the current position to avoid unnecessary calculations
        if ((targetPosition - transform.position).sqrMagnitude < 0.01f)
            return;

        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(directionToTarget);
        enemyRigidbody.MoveRotation(rotation);
    }
}