using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    private BoxCollider2D collider;
    private SpriteRenderer[] renderers;

    private void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    public void OpenDoor()
    {
        SetSpritesEnabled(false);
        collider.enabled = false;
    }

    public void CloseDoor()
    {
        SetSpritesEnabled(true);
        collider.enabled = true;
    }

    private void SetSpritesEnabled(bool enabled)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = enabled;
        }
    }
}
