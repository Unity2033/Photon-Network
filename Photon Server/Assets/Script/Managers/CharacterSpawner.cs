using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> transformList = new List<Transform>();

    private void Awake()
    {
        CreateTransform();
    }

    void Start()
    {
        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;

        PhotonNetwork.Instantiate("Character", transformList[index].position, Quaternion.identity);
    }

    void CreateTransform()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            Transform clone = Instantiate(Resources.Load<Transform>("Create Position " + i));

            transformList.Add(clone);
        }
    }
}
