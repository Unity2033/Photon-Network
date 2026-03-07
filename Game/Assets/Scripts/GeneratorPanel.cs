using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using System;

public class GeneratorPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] int personnal = 0;

    [SerializeField] Toggle [ ] toggles;

    [SerializeField] InputField inputField;

    [SerializeField] Button createRoomButton;

    public override void OnEnable()
    {
        base.OnEnable();

        inputField.text = "";

        for(int i = 0; i < toggles.Length; i++)
        {
            if(i == 0)
            {
                toggles[i].isOn = true;
            }
            else
            {
                toggles[i].isOn = false;
            }
        }

        Select(true);
    }

    private void Start()
    {
        OnRoomNameChanged();
    }

    public void OnRoomNameChanged()
    {
        createRoomButton.interactable = string.IsNullOrWhiteSpace(inputField.text) == false;
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = personnal;

        roomOptions.IsOpen = true;

        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(inputField.text, roomOptions);

        gameObject.SetActive(false);
    }

    public void Select(bool state)
    {
        if(state == false)
        {
            return;
        }

        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                personnal = i + 2;

                break;
            }
        }  
    }
}
