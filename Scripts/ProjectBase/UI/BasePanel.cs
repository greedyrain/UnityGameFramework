using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BasePanel : MonoBehaviour
{
    public Dictionary<string, List<UIBehaviour>> controllerDic = new Dictionary<string, List<UIBehaviour>>();

    private void Awake()
    {
        FindChildrenController<Button>();
        FindChildrenController<Image>();
        FindChildrenController<TMP_Text>();
        FindChildrenController<Slider>();
        FindChildrenController<ScrollRect>();
        FindChildrenController<Toggle>();
        FindChildrenController<TMP_Dropdown>();
        FindChildrenController<TMP_InputField>();
        FindChildrenController<Scrollbar>();
    }

    public virtual void ShowMe()
    {

    }

    public virtual void HideMe()
    {

    }

    protected virtual void OnClick(string buttonName)
    {

    }

    protected virtual void OnValueChanged(string toggleName, bool value)
    {

    }

    protected virtual void OnValueChanged(string toggleName, string text)
    {

    }

    protected virtual void OnValueChanged(string toggleName, int index)
    {

    }

    protected virtual void OnValueChanged(string toggleName, float value)
    {

    }

    protected virtual void OnValueChanged(string toggleName, Vector2 pos)
    {

    }

    protected T GetController<T>(string name) where T : UIBehaviour
    {
        if (controllerDic.ContainsKey(name))
        {
            for (int i = 0; i < controllerDic[name].Count; i++)
            {
                if (controllerDic[name][i] is T)
                    return controllerDic[name][i] as T;
            }
        }
        return null;
    }

    public void FindChildrenController<T>() where T : UIBehaviour
    {
        T[] controllers = GetComponentsInChildren<T>();
        for (int i = 0; i < controllers.Length; i++)
        {
            string objName = controllers[i].gameObject.name;
            if (controllerDic.ContainsKey(objName))
                controllerDic[objName].Add(controllers[i]);
            else
                controllerDic.Add(objName, new List<UIBehaviour>() { controllers[i] });

            if (controllers[i] is Button)
            {
                (controllers[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                });
            }

            else if (controllers[i] is Toggle)
            {
                (controllers[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }

            else if (controllers[i] is InputField)
            {
                (controllers[i] as InputField).onValueChanged.AddListener((text) =>
                {
                    OnValueChanged(objName, text);
                });
            }

            else if (controllers[i] is TMP_Dropdown)
            {
                (controllers[i] as TMP_Dropdown).onValueChanged.AddListener((index) =>
                {
                    OnValueChanged(objName, index);
                });
            }

            else if (controllers[i] is Slider)
            {
                (controllers[i] as Slider).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }

            else if (controllers[i] is ScrollRect)
            {
                (controllers[i] as ScrollRect).onValueChanged.AddListener((pos) =>
                {
                    OnValueChanged(objName, pos);
                });
            }

            else if (controllers[i] is Scrollbar)
            {
                (controllers[i] as Scrollbar).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
        }
    }
}
