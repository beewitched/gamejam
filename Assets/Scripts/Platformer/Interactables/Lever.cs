using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IInteractWithPlayer
{
    [Header("Settings")]
    [SerializeField]
    private bool isActive = false;

    [Header("Sprites")]
    [SerializeField]
    private Sprite activeSprite;
    [SerializeField]
    private Sprite inActiveSprite;

    [Header("Events")]
    [SerializeField]
    private UnityEvent OnActivate;
    [SerializeField]
    private UnityEvent OnDeActivate;

    private SpriteRenderer renderer;

    private void Reset()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnValidate()
    {
        if (renderer == null)
        {
            renderer = GetComponent<SpriteRenderer>();
        }
        SetSprite();
    }

    void IInteractWithPlayer.Interact()
    {
        isActive = !isActive;
        SetSprite();
        if (isActive)
        {
            OnActivate.Invoke();
        }
        else
        {
            OnDeActivate.Invoke();
        }
    }

    private void SetSprite()
    {
        if (isActive)
        {
            renderer.sprite = activeSprite;
        }
        else
        {
            renderer.sprite = inActiveSprite;
        }
    }
}
