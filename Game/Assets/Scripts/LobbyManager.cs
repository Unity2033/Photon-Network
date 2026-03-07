using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform parentTransform;

    [SerializeField] Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();

    public void GenerateRoom()
    {
        PanelManager.Instance.Load(Panel.Generator);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnConnectedToMaster()
    {
        // JoinLobby : 특정 로비를 생성하여 진입하는 함수
        if(PhotonNetwork.InLobby == false)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab = null;

        foreach(RoomInfo roomInfo in roomList)
        {
            // 룸이 삭제된 경우
            if(roomInfo.RemovedFromList)
            {
                dictionary.TryGetValue(roomInfo.Name, out prefab);

                Destroy(prefab);

                dictionary.Remove(roomInfo.Name);
            }
            else // 룸의 정보가 변경되는 경우
            {
                // 룸이 처음 생성되는 경우
                if(dictionary.TryGetValue(roomInfo.Name, out prefab) == false)
                {
                    prefab = Instantiate(Resources.Load<GameObject>("Room"), parentTransform);
                    
                    dictionary.Add(roomInfo.Name, prefab);
                }

                prefab.GetComponent<RoomData>().UpdateRoomInformation(roomInfo);
            }
        }
       
    }
}
