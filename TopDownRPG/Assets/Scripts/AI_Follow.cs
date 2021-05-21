using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Follow : MonoBehaviour
{
    public float range;
    Transform target;
    NavMeshAgent agent;

    private void Awake()
    {
        target = GameObject.Find("Player").transform;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= range)
        {
            agent.SetDestination(target.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
