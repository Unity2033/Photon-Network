using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] int count = 4;

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= count)
        {
            Debug.Log("Game Start");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

}
