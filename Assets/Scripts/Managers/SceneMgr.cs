using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr : SingleTon<SceneMgr>
{
    public void LoadScene(string sceneName,UnityAction sceneLoadComplete)
    {
        SceneManager.LoadScene(sceneName);
        sceneLoadComplete();
    }

    public void LoadSceneAsync(string sceneName,UnityAction sceneLoadComplete)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName, sceneLoadComplete));
    }
    private IEnumerator LoadSceneAsyncCoroutine(string sceneName,UnityAction sceneLoadComplete)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        sceneLoadComplete();
    }
}
