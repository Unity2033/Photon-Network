using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pause : MonoBehaviourPunCallbacks
{
    public void Resume()
    {
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        gameObject.SetActive(false);

        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby Scene");
    }
}