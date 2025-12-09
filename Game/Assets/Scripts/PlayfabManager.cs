using UnityEngine;
using Photon.Pun;
using PlayFab;
using UnityEngine.UI;
using Photon.Realtime;
using PlayFab.ClientModels;
using System.Collections;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField addressInputField;
    [SerializeField] InputField passwordInputField;

    public void Success(LoginResult loginResult)
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), Success, Failure);

        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";

        StartCoroutine(Connect());
    }

    public void Success(GetAccountInfoResult getAccountInfoResult)
    {
        PhotonNetwork.LocalPlayer.NickName = getAccountInfoResult.AccountInfo?.Username;
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    private IEnumerator Connect()
    {
        // Name Server에서 Master Server로 넘어가는 중...
        PhotonNetwork.ConnectUsingSettings(); 

        // 서버 연결이 완료되거나 시간 초과될 때까지 대기
        while(PhotonNetwork.IsConnectedAndReady == false)
        {
            yield return null;
        }

        // 특정 로비를 생성하여 진입하는 함수
        PhotonNetwork.JoinLobby();
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = addressInputField.text,
            Password = passwordInputField.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress
        (
            request,
            Success,
            Failure
        );
    }

    public void Subscribe()
    {
        PanelManager.Instance.Load(Panel.Subscribe, null);
    }

    public void Failure(PlayFabError playFabError)
    {
        var report = playFabError.GenerateErrorReport();

        var lines = report.Split("\n");

        switch (lines.Length)
        {
            case 4 : PanelManager.Instance.Load(Panel.Error, $"{lines[2]} \n\n {lines[3]}");
                break;
            case 5 : PanelManager.Instance.Load(Panel.Error, $"{lines[2]} \n\n {lines[3]} \n\n {lines[4]}");
                break;
        } 
    }
}
