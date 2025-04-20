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
        // JoinLobby : Ư�� �κ� �����Ͽ� �����ϴ� �Լ�
        PhotonNetwork.JoinLobby();
    }

    public IEnumerator Connect()
    {
        // Photon�� NameServer���� MasterServer�� �Ѿ�� ��
        PhotonNetwork.ConnectUsingSettings();  // NameServer���� �ڵ����� MasterServer�� ����

        // 3. ���� ���� ���
        float timeout = 5f;  // �ִ� ��� �ð� (��)
        float time = 0;

        // ���� ������ �Ϸ�ǰų� �ð� �ʰ� �� ������ ���
        while (PhotonNetwork.IsConnectedAndReady == false)
        {
            if (time < timeout)  // ���� �ð� �ʰ� ó��
            {
                timeout -= Time.deltaTime;
                yield break;
            }


            yield return null;  // �� ������ ���
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
