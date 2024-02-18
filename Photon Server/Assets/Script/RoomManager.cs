using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime; // ?�느 ?�버???�속?�을 ???�벤?��? ?�출?�는 ?�이브러�?

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomButton;
    public InputField roomName;
    public InputField roomPerson;
    public Transform roomContent;

    // �?목록???�?�하�??�한 ?�료구조
    Dictionary<string, RoomInfo> roomDictionary = new Dictionary<string, RoomInfo>();

    void Update()
    {
        if(roomName.text.Length > 0 && roomPerson.text.Length > 0)
            roomButton.interactable = true;
        else
            roomButton.interactable = false;
    }

    // 룸에 ?�장?????�출?�는 콜백 ?�수
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void OnClickCreateRoom()
    {
        // �??�션???�정?�니??
        RoomOptions Room = new RoomOptions();

        // 최�? ?�속?�의 ?��? ?�정?�니??
        Room.MaxPlayers = byte.Parse(roomPerson.text);

        // 룸의 ?�픈 ?��?�??�정?�니??
        Room.IsOpen = true;

        // 로비?�서 �?목록???�출 ?�킬지 ?�정?�니??
        Room.IsVisible = true;

        // 룸을 ?�성?�는 ?�수
        PhotonNetwork.CreateRoom(roomName.text, Room);
    }

    // ?�당 로비??�?목록??변�??�항???�으�??�출(추�?, ??��, 참�?)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        RemoveRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }

    void UpdateRoom(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            // ?�당 ?�름??RoomCatalog??key 값으�??�정?�어 ?�다�?
            if (roomDictionary.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : (true) 룸에????��가 ?�었????
                if (roomList[i].RemovedFromList)
                {
                    roomDictionary.Remove(roomList[i].Name);
                }
                else
                {
                    roomDictionary[roomList[i].Name] = roomList[i];
                }
            }
            else
            {
                roomDictionary[roomList[i].Name] = roomList[i];
            }
        }
    }

    public void RemoveRoom()
    {
        foreach(Transform room in roomContent)
        {
            Destroy(room.gameObject);
        }
    }

    public void CreateRoomObject()
    {
        // RoomCatalog???�러 개의 Value값이 ?�어가?�다�?RoomInfo???�어줍니??
        foreach (RoomInfo info in roomDictionary.Values)
        {
            // 룸을 ?�성?�니??
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            // RoomContect???�위 ?�브?�트�??�정?�니??
            room.transform.SetParent(roomContent);

            // �??�보�??�력?�니??
            room.GetComponent<Information>().RoomData(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
}
