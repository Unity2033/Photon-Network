using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolution : MonoBehaviour
{

    public GameObject moon;
    public GameObject origin;
    public float speed = 1.0f;

    void Start()
    {
        StartCoroutine(RotateCoroutine());
    }

    IEnumerator RotateCoroutine()
    {
        while(true)
        {
            transform.RotateAround
            (
                origin.transform.position, // 기준이 되는 게임 오브젝트
                Vector3.down,              // 기준 축
                speed * Time.deltaTime     // 회전 속도
            );

            moon.transform.Rotate(0.1f, 0.1f, 0.1f);

            yield return null;
        }
    }
    
    void Update()
    {
        
    }
}
