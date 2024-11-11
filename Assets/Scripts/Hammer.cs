using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public static Hammer Instance { get; private set; }
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private PlayerAttack playerAttack;

    private void Awake()
    {
        //Instance = this;
    }
    private void Start()
    {
        playerAttack.OnHit += Hammer_OnHit;
    }

    private void Hammer_OnHit(object sender, System.EventArgs e)
    {
        hitParticle.Play();
    }

    public void AnimatorAttack()
    {
        animator.Play("Base Layer.Attack");
    }
}
