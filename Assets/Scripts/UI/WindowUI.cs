using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowUI : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image[] imagesToggle;
    private bool[] checks;

    private void Start()
    {
        checks = new bool[2];
        for (int i = 0; i < checks.Length; i++)
        {
            checks[i] = true;
        }
    }
    public void PanelSound()
    {
        if (checks[0])
        {
            imagesToggle[0].sprite = sprites[1];
            checks[0] = false;
        }
        else
        {
            imagesToggle[0].sprite = sprites[0];
            checks[0] = true;
        }
    }
    public void PanelVibration()
    {
        if (checks[1])
        {
            imagesToggle[1].sprite = sprites[1];
            checks[1] = false;
        }
        else
        {
            imagesToggle[1].sprite = sprites[0];
            checks[1] = true;
        }
    }
}
