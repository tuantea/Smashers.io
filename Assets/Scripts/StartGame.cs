using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public static StartGame Instance { get; private set; }
    [SerializeField] private GameObject welcome;
    [SerializeField] private GameObject tapToStart;
    [SerializeField] private Image Fade;
    private bool isStartGame=false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }    
    }
    public void TapToStartGame()
    {
        //isStartGame = true;
        welcome.SetActive(false);
        StartCoroutine(DelayTime(1f));
    }
    public void ReLoadScene()
    {
        
       StartCoroutine(DelayShowTime(2f));

    }
    public void SetIsStartGame(bool status)
    {
        isStartGame = status;
    }
    public bool IsStartGame()
    {
        return isStartGame;
    }
    IEnumerator DelayTime(float t)
    {
        yield return new WaitForSeconds(t);
        isStartGame=true;
    }
    IEnumerator DelayShowTime(float t)
    {

        isStartGame = false;
        Fade.gameObject.SetActive(true);
        yield return new WaitForSeconds(t / 2);
        yield return Fade.DOFade(0f, t / 2f)
            .OnComplete(() =>
            {
                //Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 1f);
                Fade.gameObject.SetActive(false);
                Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 1f);
            });
        welcome.SetActive(true);
    }
}
