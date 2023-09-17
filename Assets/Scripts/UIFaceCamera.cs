using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 position = 2 * transform.position - Camera.main.transform.position;
        transform.LookAt(position, Vector3.up);
    }
}
