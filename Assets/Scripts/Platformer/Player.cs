using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // --- | Serialized | -------------------------------------------------------------------------

    [Header("Settings")]
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float jumpStrenght = 3f;

    [Header("Keys")]
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;
    [SerializeField]
    private KeyCode switchKey = KeyCode.Alpha3;
    [SerializeField]
    private KeyCode fireMisselKey = KeyCode.Mouse0;

    [Header("References")]
    [SerializeField]
    Bee bee;

    [Header("Prefabs")]
    [SerializeField]
    GameObject missel;

    [Header("Layers")]
    [SerializeField]
    private LayerMask obstacleLayers;

    // --- | Components | -------------------------------------------------------------------------

    private Rigidbody2D controller;
    private BoxCollider2D collider;


    // --- | Variables | --------------------------------------------------------------------------

    private bool canMove = true;


    // --- | Methods | ----------------------------------------------------------------------------

    // MonoBehaviour --------------------------------------

    private void Awake()
    {
        // Get components.
        controller = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // Get horizontal movement.
            Vector2 moveDir = controller.velocity;
            moveDir.x = Input.GetAxisRaw("Horizontal") * speed;

            // Move character.
            controller.velocity = moveDir;
        }
    }

    private void Update()
    {
        if (canMove)
        {
            // Detect ground.
            RaycastHit2D[] leftHits = Physics2D.RaycastAll(transform.position + Vector3.left * collider.bounds.extents.x, Vector2.down, obstacleLayers);
            RaycastHit2D[] rightHits = Physics2D.RaycastAll(transform.position + Vector3.right * collider.bounds.extents.x, Vector2.down, obstacleLayers);
            bool isGrounded = ((leftHits.Length > 1 && leftHits[1].distance < 0.05f) || (rightHits.Length > 1 && rightHits[1].distance < 0.05f));

            // Jump.
            if (Input.GetKeyDown(jumpKey) && isGrounded)
            {
                Jump();
            }

            // Swich position.
            if (Input.GetKeyDown(switchKey))
            {
                SwitchPos();
            }

            if (Input.GetKeyDown(fireMisselKey))
            {
                FireMissel();
            };
        }
    }

    // Public Methods -------------------------------------

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovemtn()
    {
        canMove = false;
    }

    // Private Methods ------------------------------------

    private void Jump()
    {
        controller.velocity += Vector2.up * jumpStrenght;
    }

    private void SwitchPos()
    {
        if (!CheckSpace(bee.transform.position))
        {
            return;
        }
        transform.position = bee.PortBeeToPlayer();
    }

    private bool CheckSpace(Vector2 pos)
    {
        return !Physics2D.OverlapBox(pos + collider.offset, collider.bounds.size, 0, obstacleLayers);
    }

    private void FireMissel()
    {
        Missle script = Instantiate(missel, (Vector2)collider.bounds.center, Quaternion.identity).GetComponent<Missle>();

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - collider.bounds.center;
        script.SetDirection(direction, collider);
    }
}
