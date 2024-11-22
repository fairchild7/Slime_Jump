using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] Camera _camera;
    [SerializeField] Transform _leftEdgePointTf;

    private float _leftEdge;
    public float LeftEdge => _leftEdge;

    private void Update()
    {
        _leftEdge = _camera.ScreenToWorldPoint(_leftEdgePointTf.position).x;
    }
}
