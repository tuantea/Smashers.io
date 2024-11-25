using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using Unity.VisualScripting;
public class CoinCount : MonoBehaviour
{
    
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private Transform coinParent;

    [SerializeField] private Transform spawnLocation;

    [SerializeField] private Transform endPosition;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private GameObject _canvasWin;
    [SerializeField] private float duration;
    [SerializeField] private int coinAmount;

    [SerializeField] private float minX;

    [SerializeField] private float maxX;

    [SerializeField] private float minY;

    [SerializeField] private float maxY;

    List<GameObject> coins = new List<GameObject>();

    private Tween coinReactionTween;

    private int coin;
    void Start()
    {
        _coinText.text = DataRuntimeManager.Instance.DataRuntime.Gold().ToString();
    }

    public async void NextLevel()
    {
        await CollectCoins();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        _canvasWin.SetActive(false);
        StartGame.Instance.ReLoadScene();
        LoadingScene.Instance.NextLevel();
    }
    public async void CollectCoins1()
    {
        await CollectCoins();
    }
    public async UniTask CollectCoins()
    {
        for (int i = 0; i < coins.Count; i++)
        {
            Destroy(coins[i]);
        }
        coins.Clear();
        List<UniTask> spawnCoinTaskList = new List<UniTask>();
        for (int i = 0; i < coinAmount; i++)
        {
            GameObject coinInstance = Instantiate(coinPrefab, coinParent);
            float xPosition = spawnLocation.position.x + Random.Range(minX, maxX);
            float yPosition = spawnLocation.position.y + Random.Range(minY, maxY);

            coinInstance.transform.position = new Vector3(xPosition, yPosition,100f);
            spawnCoinTaskList.Add(coinInstance.transform.DOPunchPosition(new Vector3(0, 30, 0), Random.Range(0, 1f)).SetEase(Ease.InOutElastic)
                .ToUniTask());
            coins.Add(coinInstance);
            await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
        }

        await UniTask.WhenAll(spawnCoinTaskList);
        // Move all the coins to the coin label
        await MoveCoinsTask();
        // Animation the reaction when collecting coin
    }

    private void SetCoin(int value)
    {
        coin = value;
        _coinText.text = coin.ToString();
    }
    private async UniTask MoveCoinsTask()
    {
        List<UniTask> moveCoinTask = new List<UniTask>();
        for (int i = coins.Count - 1; i >= 0; i--)
        {
            moveCoinTask.Add(MoveCoinTask(coins[i]));
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
        }
        await UniTask.WhenAll(moveCoinTask);
    }

    private async UniTask MoveCoinTask(GameObject coinInstance)
    {
        await coinInstance.transform.DOMove(endPosition.position, duration).SetEase(Ease.InBack).ToUniTask();

        GameObject temp = coinInstance;
        coins.Remove(coinInstance);
        Destroy(temp);

        await ReactToCollectionCoin();
        SetCoin(coin + 1);
    }

    private async UniTask ReactToCollectionCoin()
    {
        if (coinReactionTween == null)
        {
            coinReactionTween = endPosition.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.1f).SetEase(Ease.InOutElastic);
            await coinReactionTween.ToUniTask();
            coinReactionTween = null;
        }

    }

}