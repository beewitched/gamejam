using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class Beedoor : MonoBehaviour
{
    // --- | Serialized | -------------------------------------------------------------------------

    [Header("Settings")]
    [SerializeField]
    private bool isOpen = false;

    [Header("Sprites")]
    [SerializeField]
    private Sprite openSprite;
    [SerializeField]
    private Sprite closedSprite;


    // --- | Components | -------------------------------------------------------------------------

    private SpriteRenderer renderer;
    private BoxCollider2D collider;


    // --- | Methods | ----------------------------------------------------------------------------

    // MonoBehaviour --------------------------------------

    private void OnValidate()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        if (isOpen)
        {
            renderer.sprite = openSprite;
            collider.enabled = false;
        }
        else
        {
            renderer.sprite = closedSprite;
            collider.enabled = true;
        }
    }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Public Methods -------------------------------------

    public void Open()
    {
        isOpen = true;
        renderer.sprite = openSprite;
        collider.enabled = false;
    }

    public void Close()
    {
        isOpen = false;
        renderer.sprite = closedSprite;
        collider.enabled = true;
    }
}
