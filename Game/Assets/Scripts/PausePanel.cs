using UnityEngine;
using Photon.Pun;

public class PausePanel : MonoBehaviourPunCallbacks
{
    public void Continue()
    {
        gameObject.SetActive(false);

        MouseManager.Instance.SetMouse(false);
    }

    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        gameObject.SetActive(false);

        PhotonNetwork.LoadLevel("Lobby");
    }
}
