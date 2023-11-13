using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceCycle : MonoBehaviour
{
    float rotationSpeed = 30f; // 回転速度（度/秒）
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
