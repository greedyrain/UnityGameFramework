using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesManager : BaseManager<ResourcesManager>
{
    //Synchronous loading
    public T Load<T>(string name)where T : Object
    {
        T res = Resources.Load<T>(name);
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
    }

    //Asynchronous loading
    public void LoadAsyn<T>(string name,UnityAction<T> callback)where T:Object 
    {
        MonoManager.Instance.StartCoroutine(RealLoadAsyn(name,callback));
    }

    public IEnumerator RealLoadAsyn<T>(string name,UnityAction<T> callback) where T:Object
    {
        ResourceRequest rr = Resources.LoadAsync<T>(name);
        yield return rr;

        if (rr.asset is GameObject)
            callback(GameObject.Instantiate(rr.asset) as T);
        else
            callback(rr.asset as T);
    }
}
