using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;
    [SerializeField] Rotation rotation;
    [SerializeField] Rigidbody rigidbody;

    private void Awake()
    {
        rotation = GetComponent<Rotation>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        DisableCamera();
    }

    void Update()
    {
        if(photonView.IsMine)
        {
            Pause();

            Control();
        }    
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Move();

            rotation.RotateY(rigidbody);
        }
    }

    void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MouseManager.Instance.SetMouse(true);

            PanelManager.Instance.Load(Panel.Pause);
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
            Camera eye = transform.GetComponentInChildren<Camera>();

            eye.GetComponent<AudioListener>().gameObject.SetActive(false);

            eye.gameObject.SetActive(false);
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
        rigidbody.MovePosition(rigidbody.position + rigidbody.transform.TransformDirection(direction) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Consumable") == false)
        {
            return;
        }

        PhotonView view = other.GetComponent<PhotonView>();
        
        if(view.IsMine)
        {
            PhotonNetwork.Destroy(view.gameObject);
        }
        else if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(view.gameObject);
        }
    }
}
