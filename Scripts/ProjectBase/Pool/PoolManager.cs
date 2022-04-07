using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolManager : BaseManager<PoolManager>
{
    //Dictionary (string)-->  PoolData --> List --> GameObject

    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
    public GameObject poolObj;

    //Method to get object by name from the Dictionary;
    public void GetObj(string name,UnityAction<GameObject> callback)
    {
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
            callback(poolDic[name].GetObj());
        else
        {
            //obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            ResourcesManager.Instance.LoadAsyn<GameObject>(name, (obj) =>
            {
                obj.name = name;
                callback(obj);
            });
        }
    }


    //Method to push object into the Dictionary;
    public void PushObj(string name,GameObject obj)
    {
        if (poolObj == null)
            poolObj = new GameObject("Pool");

        if (poolDic.ContainsKey(name))
            poolDic[name].PushObj(obj);
        else
            poolDic.Add(name,new PoolData(obj, poolObj));
    }

    public void ClearPool()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
