using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Flee : IState
{
    private NavMeshAgent navMeshAgent;
    private PlayerAnimator playerAnimator;
    private Transform transform;
    private float detectionRadius;
    private bool IsAttack = false;
    private LayerMask layerMask;
    private float time = 0;
    private float timeMax = 1f;
    private bool isMove = false;

    public Flee(NavMeshAgent navMeshAgent,PlayerAnimator playerAnimator,Transform transform,float detectionRadius,LayerMask layerMask)
    {
        this.navMeshAgent = navMeshAgent;
        this.playerAnimator = playerAnimator;
        this.transform = transform;
        this.detectionRadius = detectionRadius;
        this.layerMask = layerMask;
    }
    public void OnEnter()
    {
        isMove = false;
        //Vector3 position=Position();
        navMeshAgent.isStopped =false;

        //navMeshAgent.SetDestination(position);
    }
    public void OnExit() { }

    public void Tick()
    {
        //if (time < timeMax)
        //{
        //    time += Time.deltaTime;
        //}
        //else
        //{
        //    time = 0;
        //    Position();
        //}
        if (!isMove)
        {
            navMeshAgent.SetDestination(Position());
            isMove = true;
            playerAnimator.PlayerRun(true);
        }
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && isMove)
        {
            isMove = false;
        }

    }
    public Vector3 Position()
    {
        Collider ownCollider = transform.GetComponent<Collider>();
        if (ownCollider != null) ownCollider.enabled = false;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, layerMask);
        if (ownCollider != null) ownCollider.enabled = true;
        int objectCount = hitColliders.Length;
        Debug.Log("objectCount " + objectCount);
        if (objectCount > 2||objectCount==0)
        {
            IsAttack = false;
        }
        else
        {
            IsAttack = true;
        }
        bool isInNavMesh = false;
        if (objectCount > 0) {
            NavMeshHit hit;
            isInNavMesh = NavMesh.SamplePosition(2 * transform.position - hitColliders[0].transform.position,
                out hit, 0f, NavMesh.AllAreas);
        }
            return isInNavMesh?
            2 * transform.position - hitColliders[0].transform.position: RandomPosition();
    }
    private Vector3 RandomPosition()
    {
        float radius = 14f;
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        Vector3 randomPosition = new Vector3(randomPoint.x, 0, randomPoint.y);
        return randomPosition;
    }

    public bool GetIsAttack()
    {
        //if (isMove && !IsAttack)
        //{
        //    isMove = false;
        //}
        return IsAttack;
    }
}
