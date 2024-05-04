using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform cameraTf;
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float cameraSpeed = 20;
    [SerializeField] float fixedY;

    void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        cameraTf.position = Vector3.Lerp(cameraTf.position, new Vector3(desiredPos.x, fixedY, desiredPos.z), GameManager.DeltaTime * cameraSpeed);
    }
}
