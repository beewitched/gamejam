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
    [SerializeField]
    private KeyCode interactKey = KeyCode.E;


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
    private Animator animator;
    private SpriteRenderer renderer;


    // --- | Variables | --------------------------------------------------------------------------

    private bool canMove = true;
    private IInteractWithPlayer interactable;
    private bool isGrounded = false;
    private Vector2 velocity = Vector2.zero;
    private Vector2 externalForces = Vector2.zero;
    private float currSpeed = 0f;


    // --- | Properties | -------------------------------------------------------------------------

    public Vector2 PlayerCenter
    {
        get
        {
            return collider.bounds.center;
        }
    }


    // --- | Methods | ----------------------------------------------------------------------------

    // MonoBehaviour --------------------------------------

    private void Awake()
    {
        // Get components.
        controller = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // Get horizontal movement.
            Vector2 moveDir = controller.velocity;
            currSpeed = Input.GetAxisRaw("Horizontal") * speed;
            moveDir.x = currSpeed + externalForces.x;

            // Move character.
            controller.velocity = moveDir;
        }
        externalForces = Vector2.zero;
    }

    private void Update()
    {
        // Detect ground.
        isGrounded = false;
        for (int i = -1; i <= 1; i++)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + Vector3.right * collider.bounds.extents.x * i, Vector2.down, obstacleLayers);
            if (hits.Length > 1 && hits[1].distance < 0.05f)
            {
                isGrounded = true;
            }
        }

        if (canMove && ((DialogueManager.Instance && !DialogueManager.Instance.IsActive) || !DialogueManager.Instance))
        {
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

            // Shoot missle.
            if (Input.GetKeyDown(fireMisselKey))
            {
                FireMissle();
            };

            // Interact.
            if (Input.GetKeyDown(interactKey) && interactable != null && interactable.IsInteractable)
            {
                interactable.Interact();
            }
        }

        UpdateAnimations();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractWithPlayer interactable = collision.GetComponent<IInteractWithPlayer>();
        if (interactable != null && this.interactable != interactable)
        {
            this.interactable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractWithPlayer interactable = collision.GetComponent<IInteractWithPlayer>();
        if (interactable != null && this.interactable == interactable)
        {
            this.interactable = null;
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

    public void AddForce(Vector2 force)
    {
        externalForces += force;
    }

    // Private Methods ------------------------------------

    private void Jump()
    {
        controller.velocity += Vector2.up * jumpStrenght;
        animator.SetTrigger("jump");
    }

    private void SwitchPos()
    {
        if (!CheckSpace(bee.transform.position))
        {
            return;
        }
        transform.position = bee.PortBeeToPlayer() - collider.offset;
    }

    private bool CheckSpace(Vector2 pos)
    {
        return !Physics2D.OverlapBox(pos, collider.bounds.size, 0, obstacleLayers);
    }

    private void FireMissle()
    {
        Missle script = Instantiate(missel, (Vector2)collider.bounds.center, Quaternion.identity).GetComponent<Missle>();

        //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - collider.bounds.center;
        Vector2 direction = renderer.flipX ? Vector2.left : Vector2.right;
        script.SetDirection(direction, collider);
    }

    private void UpdateAnimations()
    {
        if (controller.velocity.x < 0 && !renderer.flipX)
        {
            renderer.flipX = true;
        }
        else if (controller.velocity.x > 0 && renderer.flipX)
        {
            renderer.flipX = false;
        }

        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("speed", currSpeed);
        animator.SetFloat("gravity", controller.velocity.y);
    }
}
