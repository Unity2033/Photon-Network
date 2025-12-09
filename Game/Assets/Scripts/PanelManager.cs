using System.Collections.Generic;
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
    GameObject clone = null; 

    Dictionary<Panel, GameObject> dictionary = new();

    static PanelManager instance;

    public static PanelManager Instance { get { return instance; } }

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

        DontDestroyOnLoad(gameObject);
    }

    public void Load(Panel panel, string message)
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


        if (clone.GetComponent<ErrorPanel>() == true)
        {
            clone.GetComponent<ErrorPanel>().SetText(message);
        }
    }
}
