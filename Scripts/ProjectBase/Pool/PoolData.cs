using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
    //Relationship: Closet(Pool) - Drawer(ParentObj) - Clothes(obj in poolList)

    //The parent object for the object in the pool;
    public GameObject parentObj;
    //The list to hold object;
    public List<GameObject> poolList;
    //The constructor to the PoolData class;
    public PoolData(GameObject obj,GameObject poolObj)
    {
        //Instantiate the parentObj as object's parent Object, and set it's name as object's name;
        //Set poolObj as parentObj's parent object;
        parentObj = new GameObject(obj.name);
        parentObj.transform.SetParent(poolObj.transform);
        poolList = new List<GameObject>();
        PushObj(obj);
    }

    //Push Object into the pool, this method will be called in the PoolManager;
    public void PushObj(GameObject obj)
    {
        //Deactivate gameObject；
        //Push gameObject into the list;
        //Set parentObj as object's parent object;
        obj.SetActive(false);
        poolList.Add(obj);
        obj.transform.SetParent(parentObj.transform);
    }

    //The method which will be called in the PoolManager, import a “name” into the PoolManager's method which name is GetObj,
    //and it will check whether the key "name" in the Dictionary or not;
    //If so, return the PoolData of the key;
    //If is not, creat a new GameObject instance;
    public GameObject GetObj()
    {
        //Get the first object in list;
        //And then remove the object from this list;
        //Set the object active;
        //Set no parent for the object;
        //Return the object;
        GameObject obj = null;
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.SetParent(null);
        return obj;
    }
}
