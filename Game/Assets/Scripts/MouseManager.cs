using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseManager : MonoBehaviour
{
    private static MouseManager instance;

    public static MouseManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<MouseManager>();

                if (instance == null)
                {
                    GameObject clone = new GameObject(typeof(MouseManager).Name);

                    instance = clone.AddComponent<MouseManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetMouse(bool state)
    {
        Cursor.visible = state;

        Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch (scene.buildIndex)
        {
            case 2 : SetMouse(false);
                break;
            default : SetMouse(true);
                break;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
