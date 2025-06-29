using UnityEngine;

public class Factory : MonoBehaviour
{
    public static T Create<T>(string name) where T : MonoBehaviour
    {
        GameObject prefab = Resources.Load<GameObject>(name);

        GameObject clone = Instantiate(prefab);

        return clone.GetComponent<T>();
    }
}
