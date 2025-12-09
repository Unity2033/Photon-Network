using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] Text errorText;

    public void SetText(string message)
    {
        errorText.text = message;
    }
}
