using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Dropdown dropdown;

    public void Connect()
    {
        // ������ �����ϴ� �Լ�
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.LoadLevel("Room Scene");
    }

    public override void OnConnectedToMaster()
    {
        // JoinLobby : Ư�� �κ� �����Ͽ� �����ϴ� �Լ�
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