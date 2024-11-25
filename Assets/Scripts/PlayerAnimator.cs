using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static PlayerAnimator Instance { get; private set; }
    [SerializeField] private Animator animator;
    private bool cancelAttack = false;
    [SerializeField] private Skin skin;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }         
    }
    private void Start()
    {
        if (skin != null)
        {
            animator.runtimeAnimatorController = skin.GetController();
        }
    }

    public bool IsAttack()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("Attack");
    }

    public void PlayerRun(bool state)
    {
        if (IsAttack())
        {
            animator.Play("Idle");
            cancelAttack = true;
        }
        //animator.
        animator.SetBool("Run",state);
    }

    public void PlayerAttack()
    {
        cancelAttack = false;
        animator.SetTrigger("Attack");
    }
    public void PlayerAttack(float speed)
    {
        animator.speed = speed ;
        animator.SetTrigger("Attack");
    }
    public void PlayerVictory(bool state)
    {
        animator.SetBool("Victory",true);
    }
    public void PlayerDie()
    {
        Debug.Log("PlayerDie");
        animator.SetTrigger("Die");
    }
    public void ResetSpeed()
    {
        animator.speed = 1;
    }
    public bool IsCancelAttack()
    {
        return cancelAttack;
    }
}
