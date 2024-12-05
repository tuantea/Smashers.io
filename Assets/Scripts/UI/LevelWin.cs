using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWin : MonoBehaviour
{
    [SerializeField] private GameObject UIGameWin;
    [SerializeField] private Button _buttonWin;
    //private void Start()
    //{
    //    //StartCoroutine(WaitForPlayerInstance());
    //    //Player.Instance.OnLost += UI_OnLost;
    //}

    private IEnumerator WaitForPlayerInstance()
    {
        while (Player.Instance == null)
        {
            yield return null;
        }
        Debug.Log("Player Instance initialized.");
        EnemyManager.Instance.OnWin += UI_OnWin;
    }
    private void UI_OnWin(object sender, System.EventArgs e)
    {
        Debug.Log("UI_OnWin");
        UIGameWin.SetActive(true);
        GameController.Instance.GameControllerFinish();
    }
    private void OnEnable()
    {
        EnemyManager.OnEnemyManagerInitialized += EnemyManager_OnEnemyManagerInitialized;
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.OnWin += UI_OnWin;
        }
    }

    private void EnemyManager_OnEnemyManagerInitialized(object sender, System.EventArgs e)
    {
        EnemyManager.Instance.OnWin += UI_OnWin;
    }
    private void OnDisable()
    {
        EnemyManager.OnEnemyManagerInitialized -= EnemyManager_OnEnemyManagerInitialized;

        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.OnWin += UI_OnWin;
        }
    }

    public async void AwaitNextLevel()
    {
        _buttonWin.interactable = false;
        await UniTask.Delay(4000);
        _buttonWin.interactable = true;
    }
}
