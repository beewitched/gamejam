using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // --- | Serialized | -------------------------------------------------------------------------

    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private float jumpStrenght = 3f;

    private KeyCode jumpKey = KeyCode.Space;


    // --- | Variables | --------------------------------------------------------------------------

    private Rigidbody2D controller;
    private BoxCollider2D collider;


    // --- | Methods | ----------------------------------------------------------------------------

    private void Awake()
    {
        // Get components.
        controller = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // Get horizontal movement.
        Vector2 moveDir = controller.velocity;
        moveDir.x = Input.GetAxisRaw("Horizontal") * speed;

        // Move character.
        controller.velocity = moveDir;
    }

    private void Update()
    {
        // Detect ground.
        RaycastHit2D[] leftHits = Physics2D.RaycastAll(transform.position + Vector3.left * collider.bounds.extents.x, Vector2.down);
        RaycastHit2D[] rightHits = Physics2D.RaycastAll(transform.position + Vector3.right * collider.bounds.extents.x, Vector2.down);
        bool isGrounded = ((leftHits.Length > 1 && leftHits[1].distance < 0.05f) || (rightHits.Length > 1 && rightHits[1].distance < 0.05f));

        // Jump.
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        controller.velocity += Vector2.up * jumpStrenght;
    }
}
