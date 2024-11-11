using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance {  get; private set; }
    public event EventHandler<EventArgs> OnHit;
    public event EventHandler<EventArgs> OnFinishAttack;
    [SerializeField] private Hammer hammer;
    private void Awake()
    {
        //Instance = this;
    }

    public void StartHammerHit()
    {
        hammer.AnimatorAttack();
        
    }

    public void HammerLow()
    {
        Debug.Log("HammerLow");
    }
    public void HitCreated()
    {
        Debug.Log("HitCreated");
        OnHit?.Invoke(this,EventArgs.Empty);
    }
    public void AttackFinished()
    {
        Debug.Log("AttackFinish");
        OnFinishAttack?.Invoke(this,EventArgs.Empty);
    }
}
