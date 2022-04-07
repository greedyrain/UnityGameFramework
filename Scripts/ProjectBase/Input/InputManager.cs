using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseManager<InputManager>
{
    private bool isOn = false;
    public InputManager()
    {
        MonoManager.Instance.AddUpdateEvent(Update);
    }

    private void Update()
    {
        if (!isOn)
            return;

        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.D);
        CheckKeyCode(KeyCode.J);
        CheckKeyCode(KeyCode.K);
        CheckKeyCode(KeyCode.L);
        CheckKeyCode(KeyCode.I);
        CheckKeyCode(KeyCode.B);
    }

    public void CheckKeyCode(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
            EventCenter.Instance.EventTrigger("KeyDown", keyCode);
        if (Input.GetKeyUp(keyCode))
            EventCenter.Instance.EventTrigger("KeyUp", keyCode);
    }

    public void SwitchInputDetectionOnOrOff(bool isOn)
    {
        this.isOn = isOn;
    }
}
