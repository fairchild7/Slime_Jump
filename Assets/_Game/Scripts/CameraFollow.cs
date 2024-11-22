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
    [SerializeField] float afterRespawnCameraSpeed = 5f;
    [SerializeField] float fixedY;

    private float snapshotXPos;

    private bool isWaitingState = false;

    private void Awake()
    {
        this.RegisterListener(EventID.OnPlayerDead, (param) =>
        {
            isWaitingState = true;
            snapshotXPos = cameraTf.position.x;
        });  
    }

    void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        if (!isWaitingState)
        {
            cameraTf.position = Vector3.Lerp(cameraTf.position, new Vector3(desiredPos.x, fixedY, desiredPos.z), GameManager.DeltaTime * cameraSpeed);
        }
        else
        {
            cameraTf.position = Vector3.Lerp(cameraTf.position, new Vector3(desiredPos.x, fixedY, desiredPos.z), GameManager.DeltaTime * afterRespawnCameraSpeed);
        }
    }
}
