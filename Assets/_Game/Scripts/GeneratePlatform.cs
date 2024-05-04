using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{
    [Header("Start Platform Settings")]
    [SerializeField] Vector2 startPlatformSize = new Vector2(5f, 5f);
    [SerializeField] int startPlatformType = 0;
    [SerializeField] float startX = 0f;

    [Header("Generate Settings")]
    [SerializeField] float minXRange = 1f;
    [SerializeField] float maxXRange = 3f;
    [SerializeField] int minimumPlatformWidth = 2;
    [SerializeField] int maximumPlatformWidth = 10;
    [SerializeField] int minimumPlatformHeight = 2;
    [SerializeField] int maximumPlatformHeight = 8;
    [SerializeField] int maximumLowerHeight = 5;
    [SerializeField] int maximumHigherHeight = 2;
    [SerializeField] int defaultPlatformNumbers = 4;

    private int platformCount = 0;
    private Platform currentPlatform;
    private float currentX;

    private void Start()
    {
        currentPlatform = null;
        for (int i = 0; i < defaultPlatformNumbers; i++)
        {
            GenerateNext();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateNext();
        }
    }

    public Platform GenerateNext()
    {
        platformCount++;
        if (currentPlatform == null)
        {
            currentX = startX;
            return currentPlatform = PlatformBuilder.Instance.CreatePlatform(startPlatformType, (int)startPlatformSize.x, (int)startPlatformSize.y, startX, true, platformCount);
        }
        else
        {
            int nextWidth = Random.Range(minimumPlatformWidth, maximumPlatformWidth);

            int nextMinHeight = currentPlatform.Height - maximumLowerHeight;
            nextMinHeight = Mathf.Clamp(nextMinHeight, 0, maximumPlatformHeight);
            int nextMaxHeight = currentPlatform.Height + maximumHigherHeight;
            int nextHeight = Random.Range(nextMinHeight, nextMaxHeight + 1);
            nextHeight = Mathf.Clamp(nextHeight, minimumPlatformHeight, maximumPlatformHeight);

            float randomX = Random.Range(minXRange, maxXRange);
            float nextX = currentX + currentPlatform.Width / 2f + nextWidth / 2f + randomX;
            Debug.Log("currentX " + currentX + " currentP " + currentPlatform.Width / 2f + " nextP " + nextWidth / 2f + " random " + randomX);
            currentX = nextX;

            return currentPlatform = PlatformBuilder.Instance.CreatePlatform(currentPlatform.Type, nextWidth, nextHeight, nextX, false, platformCount);
        }
    }
}
