using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR.Haptics;
using Unity.VisualScripting;

public class EnemyStateMAnager : MonoBehaviour
{
    public EnemyBaseState currentState;
    public EnemySprintState SprintState = new EnemySprintState();
    public EnemyRoamState roamState = new EnemyRoamState();
    public EnemyInvisibleState invisibleState = new EnemyInvisibleState();
    public EnemyFightState fightState = new EnemyFightState();

    public EnemyAlertState alertState = new EnemyAlertState();

    //next block pertains to movement
    [SerializeField] public List<Transform> movePositions = new List<Transform>();
    private NavMeshAgent m_Agent;
    private Transform CurrentDestination;

    // Flag to control coroutine execution
    private bool isWaiting = false;

    //gettintg FOV bool for whether or not player is visible
    private FieldOfView fov;

    //info for shooting
    public bool playerVisible;
    public GameObject bulletPrefab;
    public float shootTimer = 0f;
    public float shootHeight = 1f;
    public float timeBetweenShots = 1f;
    public float bulletSpeed = 5f;
    public GameObject player;

    //timer for enemy restarting search    
    private float waypointSearchTimer = 0f;
    private float waypointSearchLimit = 7f; // The time limit for reaching a waypoint

    //audio
    public AudioSource audioSource; // Reference to AudioSource component
    public AudioClip hitSound; // Sound that plays when the enemy is hit
    public AudioClip castSpellSound; // Sound that plays when they cast a spell
    public AudioClip deathSound; // Sound that plays when they die

    //ref to health script
    public EnemyStats enemyStats;

    //float to manage if enemy investigates last known position
    public float investigateDist = 5f;

       private IEnumerator LogEnemyState()
    {
        while (true)
        {
            Debug.Log($"Current state: {currentState.GetType().Name}");
            yield return new WaitForSeconds(5f); // Wait for 5 seconds
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LogEnemyState());
        player = GameObject.FindGameObjectWithTag("Player");

        currentState = roamState;
        currentState.EnterState(this);
        //movement below
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.updateRotation = false; // Disable NavMeshAgent's automatic rotation
        m_Agent.stoppingDistance = 0.1f; // Set the stopping distance
        CurrentDestination = RandomDestination();
        //getting script
        fov = GetComponent<FieldOfView>();
        //Linking enemy stats
        enemyStats = GetComponent<EnemyStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pSpell"))
        {
            // Play the hit sound
            audioSource.PlayOneShot(hitSound);
            //take health
            enemyStats.TakeDamage(1);
        }
    }

    // Update is called once per frame

    void Update()
    {
        playerVisible = fov.canSeePlayer;
        currentState.UpdateState(this);
        // Call the CheckSurroundings method of the current state
        currentState.CheckSurroundings(this);

        // If the current state is set to roam and not currently waiting
        if (currentState == roamState && !isWaiting)
        {
            float dist = Vector3.Distance(transform.position, CurrentDestination.position);
            if (dist < 1 && !isWaiting) // Check if the Wait() coroutine is not already running
            {
                // Start the coroutine for the 2-second pause
                StartCoroutine(Wait());
            }
            else
            {
                // Increase the waypoint search timer
                waypointSearchTimer += Time.deltaTime;

                // If the waypoint search timer exceeds the limit
                if (waypointSearchTimer >= waypointSearchLimit)
                {
                    // Reset the waypoint search timer
                    waypointSearchTimer = 0f;

                    // Start the coroutine for the 2-second pause
                    StartCoroutine(Wait());
                }
            }

        }

        if (currentState == SprintState)
        {
            Debug.Log("teleported");
            int randomIndex = Random.Range(0, movePositions.Count);

            // Get the selected random Transform
            Transform randomTransform = movePositions[randomIndex];

            // Set the position of the target Transform to the selected random Transform's position
            transform.position = randomTransform.position;

            SwitchState(invisibleState);
        }

        if (CurrentDestination != null && m_Agent.destination != CurrentDestination.position)
        {
            m_Agent.destination = CurrentDestination.position;
        }

        // for shooting

        shootTimer += Time.deltaTime;

        if (playerVisible == true && shootTimer >= timeBetweenShots)
        {
            ShootBullet();

            shootTimer = 0f;
        }
    }

    IEnumerator Wait()
    {
        //coroutine is running
        isWaiting = true;
        yield return new WaitForSeconds(2);
        // Set a new random destination after time is passed
        CurrentDestination = RandomDestination();


        // Reset the bool to allow the coroutine to start again
        isWaiting = false;
    }

    public void SwitchState(EnemyBaseState state)
    {
    if (state == alertState)
    {
        // Additional conditions to transition to alert state
        if (currentState != alertState) // Only switch if not already in alert state
        {
            currentState = state;
            state.EnterState(this);
            currentState.EnterAlertState(this);
        }
    }
   
        
            currentState = state;
            state.EnterState(this);
        
    }
    private Transform RandomDestination()
    {

        if (movePositions.Count > 0)
        {
            int rd = Random.Range(0, movePositions.Count);
            return movePositions[rd];
        }
        return null;
    }
    void ShootBullet()
    {
        audioSource.PlayOneShot(castSpellSound); // Play the cast spell sound (if any   
        // Calculate spawn position just outside the enemy collider
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Vector3 spawnPosition = transform.position + directionToPlayer * (GetComponent<Collider>().bounds.size.magnitude / 2 + bulletPrefab.GetComponent<Collider>().bounds.size.magnitude / 2);

        // Adjust the y-coordinate of the spawn position
        spawnPosition.y += shootHeight; // Adjust this value as needed

        GameObject bullet = objPoolManager.SpawnObject(bulletPrefab, spawnPosition, Quaternion.identity, objPoolManager.PoolType.bullets);

        Debug.Log("shooting");
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = directionToPlayer * bulletSpeed;
    }
    public bool CanSeePlayer()
    {
        if (fov.canSeePlayer)
        {
            // Implement logic when player is visible
            return true;
        }
        else
        {
            if (Vector3.Distance(transform.position, fov.lastKnownPlayerPosition) > investigateDist)
            {
                m_Agent.SetDestination(fov.lastKnownPlayerPosition);
            }
            else
            {
                // Switch to roam state after a delay if player not visible
                StartCoroutine(ReturnToRoamStateAfterDelay());
            }
            return false;
        }
    }

    IEnumerator ReturnToRoamStateAfterDelay()
    { 
        yield return new WaitForSeconds(5f); // Adjust delay as needed
        SwitchState(roamState);
    }

    public void SetAlertBehavior()
    {
        // Implement behavior for setting the enemy's alert state
        // For example, you might reduce the enemy's speed or change animations

    }


}

