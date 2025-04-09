using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Move))]
[RequireComponent(typeof(Rotation))]
public class Character : MonoBehaviourPun
{
    [SerializeField] Move move;
    [SerializeField] Rotation rotation;
    [SerializeField] Camera remoteCamera;
    [SerializeField] Rigidbody rigidBody;

    private void Awake()
    {
        move = GetComponent<Move>();
        rotation = GetComponent<Rotation>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        DisableCamera();
    }

    void Update()
    {
        if (photonView.IsMine == false) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MouseManager.Instance.SetMouse(true);
        }

        move.OnKeyUpdate();
        rotation.OnKeyUpdate();
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine == false) return;

        move.OnMove(rigidBody);
        rotation.RotateY(rigidBody);
    }

    public void DisableCamera()
    {
        // 현재 플레이어가 나 자신이라면
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            remoteCamera.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView clone = other.GetComponent<PhotonView>();

        if (clone.IsMine)
        {
            PhotonNetwork.Destroy(clone.gameObject);
        }
    }

}