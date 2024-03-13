using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShootingTarget : MonoBehaviour
{
    public float orbitSpeed = 10f;  // Speed of orbiting around the player
    public float upDownSpeed = 2f;  // Speed of moving up and down
    public float upDownRange = 1f;  // Range of movement in the up and down direction
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Orbit around the player
        OrbitAroundPlayer();

        // Simulate variance in accuracy by moving up and down
        MoveUpDown();
    }

    private void OrbitAroundPlayer()
    {
        // Get the player's position
        Vector3 playerPosition = transform.parent.position;

        // Calculate the new position for the shooting target using polar coordinates
        float angle = Time.time * orbitSpeed;
        float x = playerPosition.x + Mathf.Cos(angle) * upDownRange;
        float y = playerPosition.y + Mathf.Sin(angle) * upDownRange;

        transform.position = new Vector3(x, y, playerPosition.z);
    }

    private void MoveUpDown()
    {
        // Move up and down within a specified range
        float newY = startPosition.y + Mathf.PingPong(Time.time * upDownSpeed, upDownRange * 2) - upDownRange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}