using System;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private void Awake()
    {
        SetMouse(false);
    }

    public void SetMouse(bool state)
    {
        Cursor.visible = state;

        Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
    }
}