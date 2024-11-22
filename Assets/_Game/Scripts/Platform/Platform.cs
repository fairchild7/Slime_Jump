using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    [HideInInspector] public List<PlatformPart> Parts = new List<PlatformPart>();

    int _type;
    public int Type => _type;
    int _height;
    public int Height => _height;
    int _width;
    public int Width => _width;

    private bool _isShaking;
    public bool IsShaking
    {
        get { return _isShaking; }
        set { _isShaking = value; }
    }

    private bool _isDestroyable = false;
    public bool IsDestroyable
    {
        get { return _isDestroyable; }
        set { _isDestroyable= value; }
    }

    private void Update()
    {
        CheckOutOfCameraView();
    }

    public void Setup(int type, int width, int height)
    {
        _type = type;
        _height = height;
        _width = width;
    }

    public void CheckOutOfCameraView()
    {
        if (!_isDestroyable)
        {
            return;
        }
        if (transform.position.x + _width / 2f < CameraManager.Instance.LeftEdge)
        {
            for (int i = 0; i < Parts.Count; i++)
            {
                SimplePool.Despawn(Parts[i]);
            }
            Destroy(gameObject);
        }
    }

    public void Shake()
    {
        _isShaking = true;
        ShakeLoop();
    }

    private void ShakeLoop()
    {
        transform.DORotate(new Vector3(0f, 0f, 0.5f), 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DORotate(new Vector3(0f, 0f, -0.5f), 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                ShakeLoop();
            });
        });
    }
}
