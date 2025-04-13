using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rotation))]
public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;
    [SerializeField] Camera remoteCamera;
    [SerializeField] CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();  
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

        Keyboard();

        Move();
    }

    public void Keyboard()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        // direction 방향을 단위 벡터로 설정합니다.
        direction.Normalize();
    }

    public void Move()
    {      
       characterController.Move(transform.TransformDirection(direction * speed * Time.deltaTime));
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