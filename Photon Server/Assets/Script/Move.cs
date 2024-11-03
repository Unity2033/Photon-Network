using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;

    public void OnKeyUpdate()
    {
        // direction.x�� ���� ���� �޽��ϴ�.
        direction.x = Input.GetAxisRaw("Horizontal");

        // direction.z�� ���� ���� �޽��ϴ�.
        direction.z = Input.GetAxisRaw("Vertical");

        // direction ������ ���� ���ͷ� �����մϴ�.
        direction.Normalize();
    }

    public void OnMove(Rigidbody rigidBody)
    {
        rigidBody.position += rigidBody.transform.TransformDirection(direction * speed * Time.deltaTime);
    }
}