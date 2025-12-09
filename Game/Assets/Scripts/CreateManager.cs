using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class CreateManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> transformList = new List<Transform>();

    private void Awake()
    {
        Create();
    }

    void Start()
    {
        SetPosition();
    }

    public void SetPosition()
    {
        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;

        PhotonNetwork.Instantiate("Character", transformList[index].position, Quaternion.identity);
    }

    public void Create()
    {
        for(int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            Transform clone = Instantiate(Resources.Load<Transform>("Create Position " + i));

            transformList.Add(clone);
        }
    }
}
