using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{

}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

//Observer Mode
public class EventCenter : BaseManager<EventCenter>
{
    //Decare a dictionary to hold event;
    public Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    //Add event into the dictionary;
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions += action;

        else
            eventDic.Add(name, new EventInfo<T>(action));
    }

    //Remove events in the dictionary;
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions -= action;
    }

    //Trigger events in the dictionary;
    public void EventTrigger<T>(string name, T info)
    {
        if (eventDic.ContainsKey(name))
            //eventDic[name](info);
            if ((eventDic[name] as EventInfo<T>).actions != null)
                (eventDic[name] as EventInfo<T>).actions.Invoke(info);
    }


    public void AddEventListene(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions += action;

        else
            eventDic.Add(name, new EventInfo(action));
    }

    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions -= action;
    }

    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
            //eventDic[name](info);
            if ((eventDic[name] as EventInfo).actions != null)
                (eventDic[name] as EventInfo).actions.Invoke();
    }

    //Clear the dictionary;
    public void ClearEventDic()
    {
        eventDic.Clear();
    }
}
