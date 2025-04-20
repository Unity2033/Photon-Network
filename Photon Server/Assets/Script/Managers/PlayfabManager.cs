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

    public IEnumerator Connect()
    {
        // Photon의 NameServer에서 MasterServer로 넘어가는 중
        PhotonNetwork.ConnectUsingSettings();  // NameServer에서 자동으로 MasterServer로 연결

        // 3. 서버 연결 대기
        float timeout = 5f;  // 최대 대기 시간 (초)
        float time = 0;

        // 서버 연결이 완료되거나 시간 초과 될 때까지 대기
        while (PhotonNetwork.IsConnectedAndReady == false)
        {
            if (time < timeout)  // 연결 시간 초과 처리
            {
                timeout -= Time.deltaTime;
                yield break;
            }


            yield return null;  // 한 프레임 대기
        }
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
