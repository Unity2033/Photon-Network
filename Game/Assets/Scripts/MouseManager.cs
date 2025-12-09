using Photon.Pun;
using System;
using UnityEngine;

public class MouseManager : MonoBehaviourPunCallbacks
{
    [SerializeField] static MouseManager instance;

    public static MouseManager Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetMouse(false);
    }

    public void SetMouse(bool state)
    {
        Cursor.visible = state;

        Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
    }

    private void OnDestroy()
    {
        SetMouse(true);
    }
}
