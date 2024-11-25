using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance {  get; private set; }
    [SerializeField] private Canvas joystick;
    private void Awake()
    {
        Instance = this;
    }
    public FloatingJoystick FloatingJoystick()
    {
        return joystick.transform.GetChild(0).GetComponent<FloatingJoystick>();
    }
    private void ActiveJoystick()
    {
        joystick.gameObject.SetActive(true);
    }
    public void GameControllerFinish()
    {
        joystick.transform.GetChild(0).GetComponent<FloatingJoystick>().gameObject.SetActive(false);
    }
    private void Update()
    {
        if (StartGame.Instance.IsStartGame())
        {
            ActiveJoystick();
        }
    }
}
