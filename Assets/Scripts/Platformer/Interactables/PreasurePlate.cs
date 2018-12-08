using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class PreasurePlate : MonoBehaviour
{
    // --- | Serialized | -------------------------------------------------------------------------

    [Header("Sprites")]
    [SerializeField]
    private Sprite upSprite;
    [SerializeField]
    private Sprite downSprite;

    [Header("Events")]
    [SerializeField]
    private UnityEvent OnActivate;
    [SerializeField]
    private UnityEvent OnDeActivate;


    // --- | Components | -------------------------------------------------------------------------

    private SpriteRenderer renderer;


    // --- | Methods | ----------------------------------------------------------------------------

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        renderer.sprite = downSprite;
        OnActivate.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        renderer.sprite = upSprite;
        OnDeActivate.Invoke();
    }
}
