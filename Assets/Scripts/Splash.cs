using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class Splash : MonoBehaviour
{
    [SerializeField] private List<LevelSO> levelSOList;
    [SerializeField] private Image gameFill;
    //private List<AsyncOperation> screenToLoadList = new List<AsyncOperation>();
    private List<AsyncOperation> screenToLoadList;
    private int level = 0;
    [SerializeField] private GameObject saveManager;
    void Start()
    {
        Application.targetFrameRate = 60;
        screenToLoadList = new List<AsyncOperation>();
        level = DataRuntimeManager.Instance.DataRuntime.Level();
        screenToLoadList.Add(SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive));
        screenToLoadList.Add(SceneManager.LoadSceneAsync(levelSOList[level - 1].scene, LoadSceneMode.Additive));
        DontDestroyOnLoad(saveManager);
        StartCoroutine(LoadProgress());
        
     //   Audio.Instance.StartAudio();
    }

    private void UnloadScene()
    {
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("LoadingGame"));
        SceneManager.UnloadSceneAsync("Splash");
    }
    //public GameObject GetLevel()
    //{
    //    return levelSOList[level].gameLevel1;
    //}
    IEnumerator LoadProgress()
    {

        for (int i = 0; i < screenToLoadList.Count; i++)
        {
            while (!screenToLoadList[i].isDone)
            {
                yield return null;
            }
        }
        gameFill.fillAmount = 0f;
        gameFill.DOFillAmount(1f, 2f);
        yield return new WaitForSeconds(4f);
        yield return SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelSOList[level - 1].scene));
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Splash"));
        
    }
}
