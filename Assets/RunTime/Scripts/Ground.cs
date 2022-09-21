using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Ground : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public SpriteRenderer Sprite => spriteRenderer == null ? spriteRenderer = GetComponent<SpriteRenderer>() : spriteRenderer;
}
