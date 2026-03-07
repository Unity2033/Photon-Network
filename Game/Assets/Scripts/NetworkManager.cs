using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> transforms = new List<Transform>();

    private void Awake()
    {
        SetTransform();
    }

    private void Start()
    {
        Create();
    }

    public void Create()
    {
        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;

        PhotonNetwork.Instantiate("Character", transforms[index].position, Quaternion.identity);
    }

    public void SetTransform()
    {
        for(int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            Transform prefab = Instantiate(Resources.Load<Transform>("Create Position " + i));

            transforms.Add(prefab);
        }
    }

}
