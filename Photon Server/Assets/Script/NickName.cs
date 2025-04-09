using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class NickName : MonoBehaviourPunCallbacks
{
    public Text nameText;
    private Camera virtualCamera;

    private void Awake()
    {
        virtualCamera = Camera.main;

        nameText = GetComponent<Text>();

        PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("Name");

        if (photonView.IsMine)
        {
            nameText.text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            nameText.text = photonView.Owner.NickName;
        }
    }

    void Update()
    {
        transform.forward = virtualCamera.transform.forward;
    }
}
