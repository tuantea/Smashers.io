using System;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public static event EventHandler<EventArgs> SlotChanged;
    [SerializeField] private CoinCount coinCount;
    [SerializeField] private Row[] rows;
    [SerializeField] private Button handle;
    [SerializeField] private Button _back;
    private int rowsStoppedCount;
    private bool resultsChecked =false;
    [SerializeField] private Animator animator;
    private static readonly int ActionTrigger = Animator.StringToHash("Action");
    private void Start()
    {
        foreach (var row in rows)
        {
            row.OnRowStopped += HandleRowStopped;
        }
    }


    private void HandleRowStopped(object sender, EventArgs e)
    {
        rowsStoppedCount++;

        if (rowsStoppedCount == rows.Length)
        {
            CheckResult();
        }
    }
    public void HandButton()
    {
        SlotChanged?.Invoke(this, EventArgs.Empty);
        handle.interactable = false;
        _back.interactable=false;
        animator.SetTrigger(ActionTrigger);
    }
    private void CheckResult()
    {
        if (rows[0].stoppedSlot == rows[1].stoppedSlot &&
             rows[1].stoppedSlot == rows[2].stoppedSlot && rows[2].stoppedSlot == "Skin")
        {
            Debug.Log("kkk");
        }
        else if (rows[0].stoppedSlot == rows[1].stoppedSlot &&
             rows[1].stoppedSlot == rows[2].stoppedSlot && rows[2].stoppedSlot == "Weapon")
        {
            Debug.Log("BBB");
        }
        else if (rows[0].stoppedSlot == rows[1].stoppedSlot &&
             rows[1].stoppedSlot == rows[2].stoppedSlot && rows[2].stoppedSlot == "Money")
        {
            Debug.Log("Money");
            coinCount.CollectCoins1();
        }
        resultsChecked = true;
        rowsStoppedCount = 0;
        handle.interactable = true;
        _back.interactable=true;
    }


}