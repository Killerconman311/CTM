using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private List <Transform> movePositions = new List<Transform>();
    private NavMeshAgent m_Agent;
    private Transform CurrentDestination;

  
  

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        CurrentDestination = RandomDestination();
       
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, CurrentDestination.position);
        if (dist < 1.2)
        {
            CurrentDestination = RandomDestination();
        }

        m_Agent.destination = CurrentDestination.position;

     

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


}
