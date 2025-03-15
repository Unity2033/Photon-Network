using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Dropdown dropdown;

    public void Connect()
    {
        // 서버에 접속하는 함수
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.LoadLevel("Room Scene");
    }

    public override void OnConnectedToMaster()
    {
        // JoinLobby : 특정 로비를 생성하여 진입하는 함수
        PhotonNetwork.JoinLobby
        (
           new TypedLobby
           (
               dropdown.options[dropdown.value].text,
               LobbyType.Default
           )
        );
    }
}