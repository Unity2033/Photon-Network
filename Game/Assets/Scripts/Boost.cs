using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;

    [SerializeField] bool state;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float distance = 0.2f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            state = true;
        }
    }

    private void FixedUpdate()
    {
        if (state)
        {
            rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, 0, rigidbody.linearVelocity.z);

            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            state = false;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(rigidbody.position + Vector3.up * 0.1f, Vector3.down, distance);
    }
}
