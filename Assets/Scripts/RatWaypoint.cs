using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatWaypoint : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
       
    [SerializeField] private Vector3 walkPoint = new Vector3(-2.5f, 0.2f, 22.7f);
        

    //int m_CurrentWaypointIndex;

    void Start ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination (walkPoint);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "Hole")
        {
            Destroy(this.gameObject);
        }

    }


    //void Update()
    //{
    //    if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
    //    {
    //        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % walkPoint.Length;
    //        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    //    }
    //}
}
