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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}