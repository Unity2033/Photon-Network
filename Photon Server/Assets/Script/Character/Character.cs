using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] float power;
    [SerializeField] float mouseX;
    [SerializeField] float gravity;
    [SerializeField] float rotationVelocity;

    [SerializeField] Vector3 direction;
    [SerializeField] Vector3 inputDirection;
    
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

        // direction ������ ���� ���ͷ� �����մϴ�.
        inputDirection.Normalize();

        // mouseX�� ���콺�� �Է��� ���� �����մϴ�.
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
        // ���� �÷��̾ �� �ڽ��̶��
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