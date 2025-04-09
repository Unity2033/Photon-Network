using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject rotationPrefab;

    private void OnEnable()
    {
        rotationPrefab = GameObject.Find("Rotation GameObject");

        speed = rotationPrefab.GetComponent<RotationGameObject>().Speed;

        transform.localRotation = rotationPrefab.transform.localRotation;
    }

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
