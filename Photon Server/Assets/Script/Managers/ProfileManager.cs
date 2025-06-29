using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] GameObject profilePanel;
    [SerializeField] InputField inputField;


    void Awake()
    {
        PlayFabClientAPI.GetAccountInfo
        (
            new GetAccountInfoRequest(),
            Success,
            Failure
        );
    }

    public void Submit()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = inputField.text
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName
        (
            request,
            Success,
            Failure
        );
    }

    void Success(UpdateUserTitleDisplayNameResult updateUserTitleDisplayNameResult)
    {
        profilePanel.SetActive(false);
    }

    void Success(GetAccountInfoResult result)
    {
        if (string.IsNullOrEmpty(result.AccountInfo.TitleInfo.DisplayName))
        {
            profilePanel.SetActive(true);
        }
    }

    void Failure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}
