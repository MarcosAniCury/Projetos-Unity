using System;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    //COMPONENTs
    Transform player;
    NavMeshAgent agent;
    Status myStatus;
    AnimationCharacter myAnimation;
    MovementCharacter myMovement;

    private void Start()
    {
        player = GameObject.FindWithTag(Constants.TAG_PLAYER).transform;
        agent = GetComponent<NavMeshAgent>();
        myStatus = GetComponent<Status>();
        agent.speed = myStatus.Speed;
        myAnimation = GetComponent<AnimationCharacter>();
        myMovement = GetComponent<MovementCharacter>();
    }

    private void Update()
    {
        agent.SetDestination(player.position);
        myAnimation.Walk(agent.velocity.magnitude);

        if (agent.hasPath)
        {
            bool playerClose = agent.remainingDistance <= agent.stoppingDistance;
            myAnimation.Attack(playerClose);
            
            Vector3 direction = player.position - transform.position;
            myMovement.Rotation(direction);
        }
    }
}