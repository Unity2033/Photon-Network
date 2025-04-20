using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField titleInputField;
    [SerializeField] InputField capacityInputField;

    [SerializeField] Transform parentTransform;

    [SerializeField] Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game Server");
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
        Debug.Log(roomList.Count);

        GameObject prefab = null;

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
                    GameObject clone = Instantiate(Resources.Load<GameObject>("Room"), parentTransform);

                    clone.GetComponent<Information>().View(room.Name, room.PlayerCount, room.MaxPlayers);

                    dictionary.Add(room.Name, clone);
                }
                else // 룸의 정보가 변경되는 경우
                {
                    dictionary.TryGetValue(room.Name, out prefab);

                    prefab.GetComponent<Information>().View(room.Name, room.PlayerCount, room.MaxPlayers);
                }
            }
        }
    }
}