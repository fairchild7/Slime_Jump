using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float noiseScale = 1f;

    MaterialPropertyBlock propertyBlock;
    bool isDissolving = false;
    float fade = 1f;

    private void Awake()
    {
        spriteRenderer.material.SetFloat("_Scale", noiseScale);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            isDissolving = true;
        }

        if (isDissolving)
        {
            fade -= Time.deltaTime;

            if (fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;
            }

            spriteRenderer.material.SetFloat("_Fade", fade);
        }
    }
}
