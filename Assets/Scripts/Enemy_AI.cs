using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    private StateMachine stateMachine;
    private PlayerAnimator playerAnimator;
    public EnemyAttack enemyAttack;
    public Enemy enemy;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float agentSpeed;
    [SerializeField] private float speedAnimator;

    [SerializeField] private LayerMask detectionLayer;


    private void Start()
    {
        enemyAttack.OnAttack += EnemyAttack_OnAttack;
        enemyAttack.FinishOnAttack += EnemyAttack_FinishOnAttack;
        enemy.OnDie += Enemy_OnDie;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<PlayerAnimator>();
        stateMachine = new StateMachine();
        navMeshAgent.speed = agentSpeed;
        void At(IState from,IState to,Func<bool> condition)=>
            stateMachine.AddTransition(from,to,condition);
        void Any(IState to, Func<bool> condition) =>
            stateMachine.AddAnyTransition(to, condition);
        Flee flee= new Flee(navMeshAgent,playerAnimator,transform, detectionRadius, detectionLayer);
        Attack attack = new Attack(navMeshAgent,transform,detectionRadius,detectionLayer);
        At(flee, attack, IsAttack());
        At(attack,flee, IsRun());
        stateMachine.SetState(flee);
        Func<bool> IsAttack() => () => flee.GetIsAttack();
        Func<bool> IsRun() => () => attack.GetIsRun();
    }

    private void Enemy_OnDie(object sender, EventArgs e)
    {
        if (navMeshAgent.hasPath)
        {
            navMeshAgent.isStopped = true;
        }
    }

    private void EnemyAttack_FinishOnAttack(object sender, EventArgs e)
    {
        StartCoroutine(DelayTime(1f));
        //navMeshAgent.isStopped = false;
        //playerAnimator.ResetSpeed();
        //playerAnimator.PlayerRun(true);
    }

    private void EnemyAttack_OnAttack(object sender, EventArgs e)
    {
        navMeshAgent.isStopped=true;
        playerAnimator.PlayerAttack(speedAnimator);
    }

    private void Update()
    {
        if (StartGame.Instance.IsStartGame())
        {
            stateMachine.Tick();
        }
    }
    IEnumerator DelayTime(float t)
    {
        yield return new WaitForSeconds(t);
        navMeshAgent.isStopped = false;
        playerAnimator.ResetSpeed();
        playerAnimator.PlayerRun(true);
    }
}
