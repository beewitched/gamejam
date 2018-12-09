using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSegment : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites = new Sprite[2];
    private Sprite startSprite;

    [SerializeField]
    private int maxIndex = 0;
    private int currIndex;

    [SerializeField]
    private int offset = 0;

    private SpriteRenderer renderer;
    private BoxCollider2D collider;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        startSprite = renderer.sprite;
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddIndex();
        }
    }

    public void AddIndex()
    {
        currIndex++;
        if (currIndex > 0 && currIndex < sprites.Length && currIndex <= maxIndex)
        {
            renderer.sprite = sprites[currIndex];
            collider.enabled = true;
        }
    }

    public void HideSegment()
    {
        currIndex = -offset;
        renderer.sprite = null;
        collider.enabled = false;
    }
}
