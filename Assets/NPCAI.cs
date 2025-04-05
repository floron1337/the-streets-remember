using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    [Header("AI Settings")]
    public float chaseSpeed = 5f;
    public float patrolSpeed = 3f;
    public float chaseRange = 15f;
    public float stoppingDistance = 2f;

    private NavMeshAgent agent;
    private Transform player;
    private NPCVision vision;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponent<NPCVision>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.stoppingDistance = stoppingDistance;
    }

    void Update()
    {
        if (vision.CanSeePlayer() || Vector3.Distance(transform.position, player.position) < chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol(); // Implement your existing patrol logic here
        }
    }

    void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    void Patrol()
    {
        agent.speed = patrolSpeed;
        // Add your existing patrol logic here
    }
}