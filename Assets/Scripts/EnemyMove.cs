using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool isMove = false;
    private bool test = true;
    Vector3 vectorTest = new Vector3(1, 0, 1);
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {

        //if (!isMove)
        //{
        //    agent.SetDestination(RandomPosition());
        //    isMove = true;

        //}
        //if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance&& isMove)
        //{
        //    isMove = false;
        //}
    }
    public void MovePosition(Vector3 position)
    {
        if (!isMove)
        {
            agent.SetDestination(position);
            isMove = true;
        }
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && isMove)
        {
            isMove = false;
        }
    }
    private Vector3 RandomPosition()
    {
        float radius = 14f;
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        Vector3 randomPosition = new Vector3(randomPoint.x, 0, randomPoint.y);
        return randomPosition;
    }
    public void ResetPath()
    {
        agent.ResetPath();
    }

}
