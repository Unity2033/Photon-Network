using PlayFab;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using Photon.Realtime;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;

    public void Success(LoginResult result)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    public override void OnConnectedToMaster()
    {
        // JoinLobby : 특정 로비를 생성하여 진입하는 함수
        PhotonNetwork.JoinLobby();
    }

    public void Success(RegisterPlayFabUserResult result)
    {
       
    }

    public void Join()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            RequireBothUsernameAndEmail = false    
        };

        PlayFabClientAPI.RegisterPlayFabUser
        (
            request,
            Success,
            Failure
        );
    }

    public void Access()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress
        (
            request,
            Success,
            Failure
        ); 
    }

    public void Failure(PlayFabError error)
    {
       
    }
}
