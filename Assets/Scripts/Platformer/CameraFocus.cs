using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector2 offset = Vector2.zero;
    [SerializeField]
    private Vector2 lookAhead = new Vector2(160, 90);
    [SerializeField]
    private float lerp = 0.5f;
    private Vector2 focus;

    private void LateUpdate()
    {
        if (target)
        {
            Vector2 currAhead = new Vector2();
            currAhead.x = lookAhead.x * Input.GetAxisRaw("Horizontal");
            currAhead.y = lookAhead.y * Input.GetAxisRaw("Vertical");
            focus = (Vector2)target.position + offset + currAhead;
        }

        Vector2 deltaPos = (focus - (Vector2)transform.position);
        transform.position += (Vector3)deltaPos * lerp * Time.deltaTime;
    }
}
