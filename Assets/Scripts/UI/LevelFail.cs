using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFail : MonoBehaviour
{
    [SerializeField] private GameObject UIGameFail;
    //private void Start()
    //{
    //    StartCoroutine(WaitForPlayerInstance());
    //}


    private void OnEnable()
    {
        Player.OnPlayerInitialized += Player_OnPlayerInitialized;
    }

    private void Player_OnPlayerInitialized(object sender, System.EventArgs e)
    {
        Player.Instance.OnLost += UI_OnLost;
    }

    private void OnDisable()
    {
        Player.OnPlayerInitialized -= Player_OnPlayerInitialized;
    }


    private IEnumerator WaitForPlayerInstance()
    {
        while (Player.Instance == null)
        {
            yield return null;
        }
        Debug.Log("Player Instance initialized.");
        Player.Instance.OnLost += UI_OnLost;
    }
    private void UI_OnLost(object sender, System.EventArgs e)
    {
        Debug.Log("UI_OnLost");
        UIGameFail.SetActive(true);
        GameController.Instance.GameControllerFinish();
    }
}
