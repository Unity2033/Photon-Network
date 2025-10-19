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
        // mouseY에 마우스로 입력한 값을 저장합니다. 
        mouseY += Input.GetAxisRaw("Mouse Y") * speed * Time.deltaTime;

        // MouseY의 값을 -65 ~ 65 사이의 값으로 제한합니다.
        mouseY = Mathf.Clamp(mouseY, -65, 65);

        transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }

}