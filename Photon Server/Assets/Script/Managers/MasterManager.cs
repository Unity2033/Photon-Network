using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class MasterManager : MonoBehaviourPunCallbacks
{
    private WaitForSeconds waitForSeconds = new WaitForSeconds(5.0f);

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(Create());
        }
    }

    public IEnumerator Create()
    {
        while (true)
        {
            if (PhotonNetwork.CurrentRoom != null)
            {
                PhotonNetwork.InstantiateRoomObject("Energy", Vector3.zero, Quaternion.identity);
            }

            yield return waitForSeconds;
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);

        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(Create());
        }
    }
}
