using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    public bool onWin = false;
    public static event EventHandler<EventArgs> OnEnemyManagerInitialized;
    public event EventHandler<EventArgs> OnWin;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        OnEnemyManagerInitialized?.Invoke(this,EventArgs.Empty);
    }
    private void Update()
    {
        if (!onWin && transform.childCount == 0)
        {
            onWin = true;
            OnWin?.Invoke(this, EventArgs.Empty);
        }
    }
}
