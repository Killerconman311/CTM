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
    public float timeBetweenShots = 1f;
    public float bulletSpeed = 5f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        currentState = roamState;
        currentState.EnterState(this);
        //movement below
        m_Agent = GetComponent<NavMeshAgent>();
        CurrentDestination = RandomDestination();
        //getting script
        fov = GetComponent<FieldOfView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    // Update is called once per frame
    void Update()
    {
        bool playerVisible = fov.canSeePlayer;
        currentState.UpdateState(this);

        // If the current state is set to roam and not currently waiting
        if (currentState == roamState && !isWaiting)
        {
            float dist = Vector3.Distance(transform.position, CurrentDestination.position);
            if (dist < 1.2)
            {
                // Start the coroutine for the 2-second pause
                StartCoroutine(Wait());
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

        m_Agent.destination = CurrentDestination.position;

        
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

        // Wait for 2 seconds
        yield return new WaitForSeconds(1);

        // Set a new random destination after time is passed
        CurrentDestination = RandomDestination();

        // Reset the bool to allow the coroutine to start again
        isWaiting = false;
    }

    public void SwitchState(EnemyBaseState state)
    {
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
        Debug.Log("shooting");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        //pew pew
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        bulletRb.velocity = directionToPlayer * bulletSpeed;

    }

}

