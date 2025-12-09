using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

public class Character : MonoBehaviourPun 
{
    [SerializeField] Rotation rotation;
    [SerializeField] Camera remoteCamera;
    [SerializeField] CharacterController characterController;

    [SerializeField] float speed;
    [SerializeField] Vector3 direction;

    private void Awake()
    {
        rotation = GetComponent<Rotation>();
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        DisableCamera();
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                MouseManager.Instance.SetMouse(true);

                PanelManager.Instance.Load(Panel.Pause, null);
            }

            Control();

            Move();

            rotation.RotateY();
        }
    }

    public void DisableCamera()
    {
        // 현재 플레이어가 나 자신이라면
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            remoteCamera.gameObject.SetActive(false);
        }
    }

    public void Control()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        // direction 방향을 단위 벡터로 설정합니다.
        direction.Normalize();
    }

    public void Move()
    {
        characterController.Move(characterController.transform.TransformDirection(direction) * speed * Time.deltaTime);
    }

}
