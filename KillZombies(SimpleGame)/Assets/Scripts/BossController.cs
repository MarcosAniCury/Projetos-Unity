using System;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    //Private vars
    private Transform player;
    private NavMeshAgent agent;

    private void Start()
    {
        player = GameObject.FindWithTag(Constants.TAG_PLAYER).transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(player.position);
    }
}