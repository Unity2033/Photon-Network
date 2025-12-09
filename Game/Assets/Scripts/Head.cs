using Photon.Pun;
using UnityEngine;

public class Head : MonoBehaviourPunCallbacks
{
    [SerializeField] Rotation rotation;

    [SerializeField] float minimumAngle = -65;
    [SerializeField] float maximumAngle = 65;

    private void Awake()
    {
        rotation = GetComponent<Rotation>();
    }

    void Update()
    {
        if(photonView.IsMine)
        {
            rotation.RotateX(minimumAngle, maximumAngle);
        }
    }
}
