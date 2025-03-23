using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;

public class NickNameManager : MonoBehaviour
{
    public void SetUsername(string userName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName
        (
            request,
            result => Debug.Log("Succecs"),
            error  => Debug.Log("Failure")
         );
    }

}
