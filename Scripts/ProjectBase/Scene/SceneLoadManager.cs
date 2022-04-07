using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoadManager : BaseManager<SceneLoadManager>
{
    //同步加载
    public void LoadScene(string sceneName,UnityAction func)
    {
        SceneManager.LoadScene(sceneName);
        func();
    }
    //异步加载
    public void LoadSceneAsyn(string sceneName,UnityAction func)
    {
        MonoManager.Instance.StartCoroutine(RealLoadSSceneAsyn(sceneName, func));
    }

    public IEnumerator RealLoadSSceneAsyn(string sceneName,UnityAction func)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        while(!ao.isDone)
        {
            EventCenter.Instance.EventTrigger("Progress", ao.progress);
            yield return ao.progress;
        }
        func();
    }
}
