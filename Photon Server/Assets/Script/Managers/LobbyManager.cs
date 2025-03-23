using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Dropdown dropDown;

    public void Connect()
    {
        // ������ �����ϴ� �Լ�
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // JoinLobby : Ư�� �κ� �����Ͽ� �����ϴ� �Լ�
        PhotonNetwork.JoinLobby
        (
            new TypedLobby
            (
                 dropDown.options[dropDown.value].text,
                 LobbyType.Default
            )
        );
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.IsMessageQueueRunning = true;

        PhotonNetwork.LoadLevel("Room");
    }
}