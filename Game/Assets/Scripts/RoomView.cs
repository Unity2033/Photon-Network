using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RoomView : MonoBehaviourPunCallbacks
{
    [SerializeField] Text roomText;

    [SerializeField] Button button;

    [SerializeField] string titleText;

    [SerializeField] RoomInfo roomInfo;

    public event System.Action OnEntered;

    private void Start()
    {
        OnEntered += UpdateRoomStatus;
    }

    public void OnConnectRoom()
    {
        PhotonNetwork.JoinRoom(titleText);
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

    private void OnDestroy()
    {
        OnEntered -= UpdateRoomStatus;
    }
}
