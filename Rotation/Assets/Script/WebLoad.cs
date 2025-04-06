using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebLoad : MonoBehaviour
{
    [SerializeField] RawImage image;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(GetTexture(image));
    }

    IEnumerator GetTexture(RawImage webImage)
    {
        var url = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Pok%C3%A9_Ball_icon.svg/770px-Pok%C3%A9_Ball_icon.svg.png";

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        yield return www.SendWebRequest();
        
        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            webImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

}
