using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : IState
{
    private Transform transform;
    private NavMeshAgent navMeshAgent;
    private float detectionRadius;
    private bool IsRun = false;
    private LayerMask detectionLayer;
    private float time=0;
    private float timeMax = 1f;


    public Attack(NavMeshAgent navMeshAgent,Transform transform,float detectionRadius,LayerMask dectectionLayer)
    {
        this.navMeshAgent = navMeshAgent;
        this.transform = transform;
        this.detectionRadius = detectionRadius;
        this.detectionLayer = dectectionLayer;
    }
    public void OnEnter()
    {
    }
    public void OnExit() { }

    public void Tick() {

        if (time < timeMax) {
            time +=Time.deltaTime;
        }
        else
        {
            time = 0;
            Transform target = Position();
            FollowTarGet(target);
        }          
    }
    public Transform Position()
    {
        Collider ownCollider = transform.GetComponent<Collider>();
        if (ownCollider != null) ownCollider.enabled = false;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
        if (ownCollider != null) ownCollider.enabled = true;
        int objectCount = hitColliders.Length;
        if (objectCount > 2||objectCount==0) { 
            IsRun = true;
        }
        else
        {
            IsRun = false;
        }
        return objectCount>0 ? hitColliders[0].transform: null;
    }
    public void FollowTarGet(Transform target)
    {
        if (target != null)
        {
            Vector3 position =target.position;
            navMeshAgent.SetDestination(position);
        }
        else
        {
            IsRun = true;
        }
    }
    public void EnemyIsRun()
    {
        IsRun=true;
    }
    public bool GetIsRun()
    {
        return IsRun;
    }
}
