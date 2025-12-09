using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform parentTransform;

    [SerializeField] Dictionary<string, GameObject> dictionary = new();

    public override void OnConnectedToMaster()
    {
        // JoinLobby() : 특정 로비를 생성하여 진입하는 함수
        if (PhotonNetwork.InLobby == false)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void GeneratorRoom()
    {
        PanelManager.Instance.Load(Panel.Generator, null);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab = null;

        foreach(RoomInfo roomInfo in roomList)
        {
            // 룸이 삭제된 경우
            if(roomInfo.RemovedFromList == true)
            {
                dictionary.TryGetValue(roomInfo.Name, out prefab);

                Destroy(prefab);

                dictionary.Remove(roomInfo.Name);
            }
            else // 룸의 정보가 변경되는 경우
            {
                // 룸이 처음 생성되는 경우
                if (dictionary.ContainsKey(roomInfo.Name) == false)
                {
                    GameObject clone = Instantiate(Resources.Load<GameObject>("Room"), parentTransform);

                    clone.GetComponent<RoomView>().UpdateRoomInformation(roomInfo);

                    dictionary.Add(roomInfo.Name, clone);
                }
                else
                {
                    dictionary.TryGetValue(roomInfo.Name, out prefab);

                    prefab.GetComponent<RoomView>().UpdateRoomInformation(roomInfo);
                }
            }

        }

    }

}
