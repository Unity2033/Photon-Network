using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField titleInputField;

    [SerializeField] Toggle [ ] toggles; 

    [SerializeField] int personnal = 0;

    private void Start()
    {
        Select(true);
    }

    public void OnCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = personnal;

        roomOptions.IsOpen = true;

        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(titleInputField.text, roomOptions);

        gameObject.SetActive(false);
    }

    public void Select(bool power)
    {
        if(power == false) { return; }

        if (toggles[0].isOn)
        {
            personnal = 2;
        }
        else if(toggles[1].isOn)
        {
            personnal = 3;
        }
        else if (toggles[2].isOn)
        {
            personnal = 4;
        }
    }

    public void Cancle()
    {
        gameObject.SetActive(false);
    }
  
}
