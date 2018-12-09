using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;


    public void SetDirection(Vector2 direction, Collider2D playerCollider)
    {
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), playerCollider);
        GetComponent<SpriteRenderer>().flipX = direction.x < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(collider.bounds.center, collider.radius);

        for(int i = 0; i < colliders.Length; i++)
        {
            IInteractWithAttack interactable = colliders[i].transform.GetComponent<IInteractWithAttack>();
            if (interactable != null)
            {
                interactable.GetHit();
            }
        }
        Destroy(gameObject);
    }
}