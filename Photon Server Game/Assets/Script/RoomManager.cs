using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreate;
    public InputField roomName;
    public InputField roomPerson;
    public Transform roomContent;
  
    Dictionary<string, RoomInfo> roomCatalog = new Dictionary<string, RoomInfo>();

    void Update()
    {
 
        if(roomName.text.Length > 0 && roomPerson.text.Length > 0)
        {
            roomCreate.interactable = true;
        }
        else
        {
            roomCreate.interactable = false;
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void CreateRoomObject()
    {
        // roomCatalog�� ���� ���� value���� ���ִٸ� RoomInfo�� �־��ݴϴ�.
        foreach(RoomInfo info in roomCatalog.Values)
        {
            // ���� �����մϴ�.
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            // roomContent ���� ������Ʈ�� ��ġ�� �����մϴ�.
            room.transform.SetParent(roomContent);

            // �� ������ �Է��մϴ�.
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    public void OnClickCreateRoom()
    {
        // �� �ɼ��� �����մϴ�.
        RoomOptions Room = new RoomOptions();

        // �ִ� �������� ���� �����մϴ�.
        Room.MaxPlayers = byte.Parse(roomPerson.text);

        // ���� ���� ���θ� �����մϴ�.
        Room.IsOpen = true;

        // �κ񿡼� �� ����� ���� ��ų�� �����մϴ�.
        Room.IsVisible = true;

        // ���� �����ϴ� �Լ�
        PhotonNetwork.CreateRoom(roomName.text, Room);
    }

    public void AllDeleteRoom()
    {
        // Transform ������Ʈ�� �ִ� ���� ������Ʈ�� �����Ͽ� ��ü ������ �õ��մϴ�.
        foreach(Transform trans in roomContent)
        {
            // Transform�� ������ �ִ� ���� ������Ʈ�� �����մϴ�.
            Destroy(trans.gameObject);
        }
    }

    // �ش� �κ� �� ����� ���� ������ ���� �� ȣ��Ǵ� �Լ�
    // (�߰�, ����, ����)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }

    public void UpdateRoom(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            // �ش� �̸��� roomCatalog�� Key ������ �����Ǿ� �ִٸ�
            if (roomCatalog.ContainsKey(roomList[i].Name))
            {
                // RemoveFromList : (true) �뿡�� ������ �Ǿ��� ��
                if (roomList[i].RemovedFromList)
                {
                    roomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }

            roomCatalog[roomList[i].Name] = roomList[i];
        }

    }




}
