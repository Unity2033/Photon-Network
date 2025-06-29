using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] float mouseX;
    [SerializeField] float rotationVelocity;

    [SerializeField] Vector3 direction;

    [SerializeField] Camera remoteCamera;
    [SerializeField] CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (photonView.IsMine)
        {
            State(false);
        }

        DisableCamera();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // UI에 포커스가 있다면 입력 무시
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }

            Control();

            Move();

            Rotate();
        }

    }

    public void Control()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        // direction 방향을 단위 벡터로 설정합니다.
        direction.Normalize();

        // mouseX에 마우스로 입력한 값을 저장합니다.
        mouseX += Input.GetAxisRaw("Mouse X") * rotationVelocity * Time.deltaTime;
    }

    public void Move()
    {      
        characterController.Move(characterController.transform.TransformDirection(direction) * speed * Time.deltaTime);
    }

    public void Rotate()
    {
        transform.eulerAngles = new Vector3(0, mouseX, 0);
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

    public void State(bool state)
    {
        Cursor.visible = state;

        Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Authorized")
        {
            PhotonView clone = other.GetComponent<PhotonView>();

            if (clone.IsMine)
            {
                PhotonNetwork.Destroy(clone.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            State(true);
        }
    }
}