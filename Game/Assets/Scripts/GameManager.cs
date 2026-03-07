using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] double time;
    [SerializeField] double initializeTime;

    [SerializeField] int minute;
    [SerializeField] int second;
    [SerializeField] int milliseconds;

    public void Awake()
    {
        initializeTime = PhotonNetwork.Time;

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("InitializeTime", RpcTarget.AllBuffered, PhotonNetwork.Time);
        }
    }

    [PunRPC]
    void InitializeTime(double time)
    {
        initializeTime = time;
    }

    private void Update()
    {
        time = PhotonNetwork.Time - initializeTime;

        minute = (int)time / 60;
        second = (int)time % 60;
        milliseconds = (int)(time * 100) % 100;


    }
}
