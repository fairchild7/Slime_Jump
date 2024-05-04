using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBuilder : Singleton<PlatformBuilder>
{
    [SerializeField] List<SpriteSet> spriteSets = new List<SpriteSet>();

    public Platform CreatePlatform(int type, int width, int height, float xPos)
    {
        Platform platform = CreatePlatform(type, width, height);
        platform.transform.position = new Vector2(xPos, 0f);
        return platform;
    }

    public Platform CreatePlatform(int type, int width, int height)
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

        SpriteSet spriteSet = spriteSets[type];

        GameObject platform = new GameObject("Platform_" + type.ToString());

        float firstSpawnX = -width / 2f;
        float firstSpawnY = height;

        for (int i = 0; i < width * height; i++)
        {
            GameObject part = null;
            if (i / width == 0)
            {
                if (i % width == 0)
                {
                    part = InstantiateSprite(spriteSet.TopLeft, platform.transform);
                }
                else if (i % width == width - 1)
                {
                    part = InstantiateSprite(spriteSet.TopRight, platform.transform);
                }
                else
                {
                    part = InstantiateSprite(spriteSet.TopMiddle, platform.transform);
                }
            }
            else if (i / width == height - 1)
            {
                if (i % width == 0)
                {
                    part = InstantiateSprite(spriteSet.BottomLeft, platform.transform);
                }
                else if (i % width == width - 1)
                {
                    part = InstantiateSprite(spriteSet.BottomRight, platform.transform);
                }
                else
                {
                    part = InstantiateSprite(spriteSet.BottomMiddle, platform.transform);
                }
            }
            else
            {
                if (i % width == 0)
                {
                    part = InstantiateSprite(spriteSet.MiddleLeft, platform.transform);
                }
                else if (i % width == width - 1)
                {
                    part = InstantiateSprite(spriteSet.MiddleRight, platform.transform);
                }
                else
                {
                    part = InstantiateSprite(spriteSet.MiddleMiddle, platform.transform);
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

    private GameObject InstantiateSprite(Sprite sprite, Transform parent)
    {
        GameObject go = new GameObject(sprite.name);
        go.AddComponent<SpriteRenderer>();
        go.GetComponent<SpriteRenderer>().sprite = sprite; 
        go.transform.parent = parent;
        return go;
    }

    private GameObject InstantiateSprite(Sprite sprite, Vector3 position, Quaternion rotation,  Transform parent)
    {
        GameObject go = InstantiateSprite(sprite, parent);
        go.transform.position = position;
        go.transform.rotation = rotation;
        return go;
    }
}

[Serializable]
public class SpriteSet
{
    public Sprite TopLeft;
    public Sprite TopMiddle;
    public Sprite TopRight;
    public Sprite MiddleLeft;
    public Sprite MiddleMiddle;
    public Sprite MiddleRight;
    public Sprite BottomLeft;
    public Sprite BottomMiddle;
    public Sprite BottomRight;
}
