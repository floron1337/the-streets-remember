using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomWalker : MonoBehaviour
{
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float minWaitTime = 2f;
    [SerializeField] private float maxWaitTime = 4f;

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewDestination();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        Vector3 randomPos = Random.insideUnitSphere * wanderRadius + transform.position;
        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        timer = Random.Range(minWaitTime, maxWaitTime);
    }
}
