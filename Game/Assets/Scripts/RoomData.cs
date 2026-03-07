using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using System;

public class RoomData : MonoBehaviourPunCallbacks
{
    [SerializeField] Text roomText;

    [SerializeField] Button button;

    [SerializeField] string titleText;

    [SerializeField] RoomInfo roomInfo;

    [SerializeField] event Action OnEntered;

    public override void OnEnable()
    {
        base.OnEnable();

        OnEntered += UpdateRoomStatus;
    }

    private void Start()
    {
        button.onClick.AddListener(() => PhotonNetwork.JoinRoom(titleText));
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        PanelManager.Instance.Load(Panel.Error, message);
    }

    public void UpdateRoomInformation(RoomInfo roomInfo)
    {
        this.roomInfo = roomInfo;

        titleText = roomInfo.Name;

        roomText.text = roomInfo.Name + " ( " + roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers + " ) ";

        OnEntered?.Invoke();
    }

    public void UpdateRoomStatus()
    {
        if(roomInfo.IsOpen)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        OnEntered -= UpdateRoomStatus;
    }

}
