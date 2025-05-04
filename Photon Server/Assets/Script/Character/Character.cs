using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] float power;
    [SerializeField] float mouseX;
    [SerializeField] float gravity;
    [SerializeField] float rotationVelocity;

    [SerializeField] Vector3 direction;
    [SerializeField] Vector3 inputDirection;

    [SerializeField] Vector3 initializeDirection;

    [SerializeField] Camera remoteCamera;
    [SerializeField] CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();  
    }

    void Start()
    {
        DisableCamera();

        initializeDirection = transform.position;
    }

    void Update()
    {
        if (photonView.IsMine == false) return;

        // UI에 포커스가 있다면 입력 무시
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            return;
        }

        Control();

        Jump();

        Move();

        Rotate();


    }

    void Jump()
    {
        if (characterController.isGrounded)
        {
            direction.y = -1.0f;

            if (Input.GetButtonDown("Jump"))
            {
                direction.y = power;
            }
        }
        else
        {
            direction.y -= gravity * Time.deltaTime;
        }
    }

    public void Control()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.z = Input.GetAxisRaw("Vertical");

        // direction 방향을 단위 벡터로 설정합니다.
        inputDirection.Normalize();

        // mouseX에 마우스로 입력한 값을 저장합니다.
        mouseX += Input.GetAxisRaw("Mouse X") * rotationVelocity * Time.deltaTime;

        inputDirection = characterController.transform.TransformDirection(inputDirection);
    }

    public void Move()
    {      
        Vector3 vector3 = new Vector3(inputDirection.x, direction.y, inputDirection.z);

        characterController.Move(vector3 * speed * Time.deltaTime);

        direction.y = vector3.y;
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

    public void InitializePosition()
    {
        characterController.enabled = false;

        transform.position = initializeDirection;

        characterController.enabled = true;
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

}