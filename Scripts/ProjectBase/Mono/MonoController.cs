using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    public event UnityAction updateEvent;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (updateEvent != null)
        {
            updateEvent();
        }
    }

    public void AddUpdateEvent(UnityAction func)
    {
        updateEvent += func;
    }

    public void RemoveUpdateEvent(UnityAction func)
    {
        updateEvent -= func;
    }
}
