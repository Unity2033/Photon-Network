using Photon.Pun;
using UnityEngine;

public class Head : MonoBehaviourPunCallbacks
{
    [SerializeField] float speed;
    [SerializeField] float mouseY;


    void Update()
    {
        if (photonView.IsMine == false) return;

        RotateX();
    }

    public void RotateX()
    {
        // mouseY�� ���콺�� �Է��� ���� �����մϴ�. 
        mouseY += Input.GetAxisRaw("Mouse Y") * speed * Time.deltaTime;

        // MouseY�� ���� -65 ~ 65 ������ ������ �����մϴ�.
        mouseY = Mathf.Clamp(mouseY, -65, 65);

        transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }

}