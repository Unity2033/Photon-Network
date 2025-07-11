using PlayFab;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using Photon.Realtime;
using System.Collections;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;

    public void Success(LoginResult result)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";

        StartCoroutine(Connect());
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    public IEnumerator Connect()
    {
        // Photon의 NameServer에서 MasterServer로 넘어가는 중
        PhotonNetwork.ConnectUsingSettings();  // NameServer에서 자동으로 MasterServer로 연결

        // 서버 연결이 완료되거나 시간 초과 될 때까지 대기
        while (PhotonNetwork.IsConnectedAndReady == false)
        {
            yield return null;  // 한 프레임 대기
        }

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
        Debug.Log("AA");
    }
}
