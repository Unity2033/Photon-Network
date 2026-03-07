using PlayFab.EconomyModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] Text errorText;

    private void Awake()
    {
        errorText = GetComponentInChildren<Text>();
    }

    public void SetText(string message)
    {
        int index = message.IndexOf(":");

        if (index >= 0)
        {
            message = message.Substring(index + 1);
        }

        var lines = message.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);

        List<string> filtered = new List<string>();

        foreach (var line in lines)
        {
            string clean = line.Trim();

            if (clean == "Invalid input parameters")
                continue;

            if (clean.StartsWith(":"))
                clean = clean.Substring(1).Trim();

            if (!string.IsNullOrEmpty(clean))
                filtered.Add(clean);
        }

        errorText.text = string.Join("\n\n", filtered);
    }
}
