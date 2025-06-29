using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class Identifier : MonoBehaviourPunCallbacks
{
    private Camera virtualCamera;
    [SerializeField] Text nameText;

    private void Awake()
    {
        virtualCamera = Camera.main;

        PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("Name");

        Load();

        if (photonView.IsMine)
        {
            nameText.text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            nameText.text = photonView.Owner.NickName;
        }
    }

    void Update()
    {
        transform.forward = virtualCamera.transform.forward;
    }


    void Load()
    {
        PlayFabClientAPI.GetAccountInfo
        (
            new GetAccountInfoRequest(),
            SynchronizationName,
            Failure
        );
    }

    void SynchronizationName(GetAccountInfoResult getAccountInfoResult)
    {
        PhotonNetwork.LocalPlayer.NickName = getAccountInfoResult.AccountInfo.TitleInfo.DisplayName;
    }

    void Failure(PlayFabError playFabError)
    {
        Debug.LogError(playFabError.GenerateErrorReport());
    }
}
