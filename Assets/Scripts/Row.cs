using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Row : MonoBehaviour
{
    public event EventHandler<EventArgs> OnRowStopped;
    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;

    private void Start()
    {
        rowStopped = true;
        SlotMachine.SlotChanged += SlotMachine_SlotChanged;
    }

    private void SlotMachine_SlotChanged(object sender, System.EventArgs e)
    {
        stoppedSlot = "";
        StartCoroutine(Rotate());
    }
    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.01f;
        for (int i = 0; i < 40; i++)
        {
            if (IsApproximately(transform.position.y, -3.82f))
            {
                transform.position = new Vector3(transform.position.x, 0.86f, 100f);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.39f, 100f);
            yield return new WaitForSeconds(timeInterval);
        }
        randomValue = Random.Range(60, 100);
        switch (randomValue % 4)
        {
            case 1:
                randomValue += 3;
                break;
            case 2:
                randomValue += 2;
                break;
            case 3:
                randomValue += 1;
                break;
        }
        for (int i = 0; i < randomValue; i++)
        {
            if (IsApproximately(transform.position.y, -3.82f))
            {
                transform.position = new Vector3(transform.position.x, 0.86f, 100f);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.39f, 100f);
            if (i > Mathf.RoundToInt(randomValue * 0.25f))
            {
                timeInterval = 0.01f;
            }
            else if (i > Mathf.RoundToInt(randomValue * 0.5f))
            {
                timeInterval = 0.02f;
            }
            else if (i > Mathf.RoundToInt(randomValue * 0.75f))
            {
                timeInterval = 0.03f;
            }
            else if (i > Mathf.RoundToInt(randomValue * 0.95f))
            {
                timeInterval = 0.04f;
            }
            yield return new WaitForSeconds(timeInterval);
        }
        
        if (IsApproximately(transform.position.y, -0.7f))
        {
            stoppedSlot = "Skin";
        }
        else if (IsApproximately(transform.position.y, -2.26f))
        {
            stoppedSlot = "Money";
        }

        else if (IsApproximately(transform.position.y, -3.82f) || IsApproximately(transform.position.y, 0.86f))
        {
            stoppedSlot = "Weapon";
        }
        rowStopped = true;
        OnRowStopped?.Invoke(this,EventArgs.Empty);
    }
    private bool IsApproximately(float a, float b)
    {
        return Mathf.Abs(a - b) < 0.01;
    }
}
