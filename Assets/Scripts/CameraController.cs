using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followTarget;
    public float followOffset = 0;

    // Update is called once per frame
    void Update()
    {
        if (followTarget != null)
        {
            transform.position = new Vector3(transform.position.x, followTarget.position.y + followOffset, transform.position.z);
        }
    }
}
