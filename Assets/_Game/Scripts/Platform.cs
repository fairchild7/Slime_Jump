using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    int _type;
    public int Type => _type;
    int _height;
    public int Height => _height;
    int _width;
    public int Width => _width;

    public void Setup(int type, int height, int width)
    {
        _type = type;
        _height = height;
        _width = width;
    }
}
