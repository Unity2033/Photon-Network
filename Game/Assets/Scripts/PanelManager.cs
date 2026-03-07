using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public enum Panel
{
    Error,
    Subscribe,
    Generator,
    Pause
}

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject clone = null;

    private static PanelManager instance;

    private Dictionary<Panel, GameObject> dictionary = new Dictionary<Panel, GameObject>();

    public static PanelManager Instance 
    {       
        get 
        { 
            if(instance == null)
            {
                instance = FindAnyObjectByType<PanelManager>();

                if(instance == null)
                {
                    GameObject clone = new GameObject(typeof(PanelManager).Name);

                    instance = clone.AddComponent<PanelManager>();
                }
            }
            
            return instance; 
        }  
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Load(Panel panel, string message = null)
    {
        if(dictionary.TryGetValue(panel, out clone) == false)
        {
            clone = (GameObject)Instantiate(Resources.Load(panel.ToString()));

            clone.name = clone.name.Replace("(Clone)", "");

            dictionary.Add(panel, clone);

            DontDestroyOnLoad(clone);
        }
        else
        {
            clone = dictionary[panel];

            clone.SetActive(true);
        }

        if(message != null)
        {
            clone.GetComponent<ErrorPanel>().SetText(message);
        }
    }
}
