using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static LoadingScene Instance { get; private set; }
    [SerializeField] List<LevelSO> levelSOList = new List<LevelSO>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void Restart()
    {
        int index = DataRuntimeManager.Instance.DataRuntime.Level() - 1;
        SceneManager.UnloadSceneAsync(levelSOList[index].scene);
        StartCoroutine(LoadSceneAsyncAndSetActive(levelSOList[index].scene));
    }
    public void NextLevel()
    {
        int index = DataRuntimeManager.Instance.DataRuntime.Level() - 1;
        SceneManager.UnloadSceneAsync(levelSOList[index].scene);
        StartCoroutine(LoadSceneAsyncAndSetActive(levelSOList[index+1].scene));
        DataRuntimeManager.Instance.DataRuntime.SetLevel(index + 2);
    }
    private IEnumerator LoadSceneAsyncAndSetActive(string sceneToLoad)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        yield return asyncOperation;

        Scene newScene = SceneManager.GetSceneByName(sceneToLoad);
        if (newScene.IsValid())
        {
            SceneManager.SetActiveScene(newScene);
        }
        else
        {
            Debug.LogError($"Failed to load the scene {sceneToLoad}.");
        }
    }
    int count = 0,sum=0;
    private void Update()
    {
        count++;
        int k= Mathf.CeilToInt(1.0f / Time.deltaTime);
        sum += k;
        Debug.Log("fps: " + k);
        Debug.Log("fpsTB: " + Mathf.CeilToInt(sum / count));
    }
}
