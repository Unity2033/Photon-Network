using Photon.Pun;
using UnityEngine;

public class Head : MonoBehaviourPunCallbacks
{
    [SerializeField] Rotation rotation;

    [SerializeField] float minimumAngle = -55;
    [SerializeField] float maximumAngle = 55;

    public void Awake()
    {
        rotation = GetComponent<Rotation>();
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            rotation.RotateX(minimumAngle, maximumAngle);
        }
    }
}
