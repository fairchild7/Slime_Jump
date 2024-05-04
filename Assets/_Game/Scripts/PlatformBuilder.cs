using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBuilder : Singleton<PlatformBuilder>
{
    [SerializeField] List<PlatformSet> spriteSets = new List<PlatformSet>();

    public Platform CreatePlatform(int type, int width, int height, float xPos, bool isFirstPlatform = false, int platformId = 0)
    {
        Platform platform = CreatePlatform(type, width, height, isFirstPlatform, platformId);
        platform.transform.position = new Vector2(xPos, 0f);
        return platform;
    }

    public Platform CreatePlatform(int type, int width, int height, bool isFirstPlatform = false, int platformId = 0)
    {
        if (width < 2 || height < 2)
        {
            Debug.LogError("Invalid properties!");
            return null;
        }
        if (type > spriteSets.Count)
        {
            Debug.LogError("Type index out of range!");
            return null;
        }

        PlatformSet spriteSet = spriteSets[type];

        GameObject platform = new GameObject("Platform_" + type.ToString());
        Transform platformTf = platform.transform;

        if (!isFirstPlatform)
        {
            GameObject pointCollider = new GameObject("Point Collider");
            pointCollider.transform.parent = platform.transform;
            pointCollider.AddComponent<PointCollider>().AddPointCollider(width, height, platformId);
        }

        float firstSpawnX = -width / 2f;
        float firstSpawnY = height;

        for (int i = 0; i < width * height; i++)
        {
            GameUnit part = null;
            if (i / width == 0)
            {
                if (i % width == 0)
                {
                    part = SimplePool.Spawn(StringToPoolTypeEnum(type, "_TopLeft"), platformTf);
                }
                else if (i % width == width - 1)
                {
                    part = SimplePool.Spawn(StringToPoolTypeEnum(type, "_TopRight"), platformTf);
                }
                else
                {
                    part = SimplePool.Spawn(StringToPoolTypeEnum(type, "_TopMiddle"), platformTf);
                }
            }
            else if (i / width == height - 1)
            {
                if (i % width == 0)
                {
                    part = SimplePool.Spawn(StringToPoolTypeEnum(type, "_BottomLeft"), platformTf);
                }
                else if (i % width == width - 1)
                {
                    part = SimplePool.Spawn(StringToPoolTypeEnum(type, "_BottomRight"), platformTf);
                }
                else
                {
                    part = SimplePool.Spawn(StringToPoolTypeEnum(type, "_BottomMiddle"), platformTf);
                }
            }
            else
            {
                if (i % width == 0)
                {
                    part = SimplePool.Spawn(StringToPoolTypeEnum(type, "_MiddleLeft"), platformTf);
                }
                else if (i % width == width - 1)
                {
                    part = SimplePool.Spawn(StringToPoolTypeEnum(type, "_MiddleRight"), platformTf);
                }
                else
                {
                    part = SimplePool.Spawn(StringToPoolTypeEnum(type, "_MiddleMiddle"), platformTf);
                }
            }
            part.transform.localPosition = new Vector2(firstSpawnX + i % width, firstSpawnY - i / width);
        }
        platform.AddComponent<BoxCollider2D>();
        platform.GetComponent<BoxCollider2D>().offset = new Vector2(0f, height / 2f);
        platform.GetComponent<BoxCollider2D>().size = new Vector2(width, height);

        platform.AddComponent<Platform>().Setup(type, width, height);
        return platform.GetComponent<Platform>();
    }

    private PoolType StringToPoolTypeEnum(int typeId, string type)
    {
        return (PoolType)System.Enum.Parse(typeof(PoolType), "Type" + typeId + type);
    }
}

[Serializable]
public class PlatformSet
{
    public GameObject TopLeft;
    public GameObject TopMiddle;
    public GameObject TopRight;
    public GameObject MiddleLeft;
    public GameObject MiddleMiddle;
    public GameObject MiddleRight;
    public GameObject BottomLeft;
    public GameObject BottomMiddle;
    public GameObject BottomRight;
}
