using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class MonoManager : BaseManager<MonoManager>
{
    private MonoController monoController;
    public MonoManager()
    {
        GameObject obj = new GameObject("MonoController");
        monoController = obj.AddComponent<MonoController>();
    }

    public void AddUpdateEvent(UnityAction func)
    {
        monoController.AddUpdateEvent(func);
    }

    public void RemoveUpdateEvent(UnityAction func)
    {
        monoController.RemoveUpdateEvent(func);
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return monoController.StartCoroutine(routine);
    }

    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return monoController.StartCoroutine(methodName, value);
    }

    public Coroutine StartCoroutine(string methodName)
    {
        return monoController.StartCoroutine(methodName);
    }
}
