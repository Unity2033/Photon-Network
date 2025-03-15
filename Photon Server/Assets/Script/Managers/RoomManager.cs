using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField titleInputField;
    [SerializeField] InputField capacityInputField;

    [SerializeField] Transform contentTransform;

    private Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();

    void Start()
    {
        if (PhotonNetwork.InLobby == false)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.IsMessageQueueRunning = true; // 네트워크 이벤트 다시 활성화
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game Scene");
    }

    public void OnCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = byte.Parse(capacityInputField.text);

        roomOptions.IsOpen = true;

        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(titleInputField.text, roomOptions);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab;

        foreach (RoomInfo room in roomList)
        {
            // 룸이 삭제된 경우
            if (room.RemovedFromList == true)
            {
                dictionary.TryGetValue(room.Name, out prefab);

                Destroy(prefab);

                dictionary.Remove(room.Name);
            }
            else // 룸의 정보가 변경되는 경우
            {
                // 룸이 처음 생성되는 경우
                if (dictionary.ContainsKey(room.Name) == false)
                {
                    GameObject clone = Instantiate(Resources.Load<GameObject>("Room"), contentTransform);

                    clone.GetComponent<Information>().SetData
                    (
                        room.Name,
                        room.PlayerCount,
                        room.MaxPlayers
                    );

                    dictionary.Add(room.Name, clone);
                }
                else // 룸의 정보가 변경되는 경우
                {
                    dictionary.TryGetValue(room.Name, out prefab);

                    prefab.GetComponent<Information>().SetData
                    (
                        room.Name,
                        room.PlayerCount,
                        room.MaxPlayers
                    );
                }
            }
        }
    }
}
