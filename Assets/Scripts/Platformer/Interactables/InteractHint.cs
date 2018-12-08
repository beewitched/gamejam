using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHint : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        CircleCollider2D parentCol = GetComponentInParent<CircleCollider2D>();
        if (parentCol)
        {
            CircleCollider2D col = gameObject.AddComponent<CircleCollider2D>();
            col.isTrigger = true;
            col.radius = parentCol.radius;

            Vector2 coliderPos = (Vector2)parentCol.transform.position + parentCol.offset;
            col.offset = coliderPos - (Vector2)transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShowHint();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideHint();
    }

    public void ShowHint()
    {
        canvas.enabled = true;
    }

    public void HideHint()
    {
        canvas.enabled = false;
    }
}
