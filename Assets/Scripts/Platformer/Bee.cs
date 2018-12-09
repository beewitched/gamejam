using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bee : MonoBehaviour
{
    // --- | Serialized | -------------------------------------------------------------------------

    [Header("Settings")]
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float maxDistanceToPlayer = 1.5f;

    [Header("References")]
    [SerializeField]
    private Player player;

    [Header("Layers")]
    [SerializeField]
    private LayerMask obstacleLayers;


    // --- | Componetns | -------------------------------------------------------------------------

    private Rigidbody2D controller;
    private Animator animator;
    private SpriteRenderer renderer;


    // --- | Variables | --------------------------------------------------------------------------

    private bool isAutoControlled = true;
    public bool IsAutoControlled
    {
        get
        {
            return isAutoControlled;
        }
    }
    private bool targetPlayer = true;

    private Vector2 targetPos;

    private Vector3[] controllpoints;
    private int currentControllpointIndex = 0;
    private Vector3 CurrentControllPoint
    {
        get
        {
            return controllpoints[currentControllpointIndex];
        }
    }

    private System.Action OnBeeArival;

    private bool isMoveing = false;


    // --- | Methods | ----------------------------------------------------------------------------

    // MonoBehaviour --------------------------------------

    private void Awake()
    {
        controller = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (isAutoControlled)
        {
            if (targetPlayer)
            {
                targetPos = player.transform.position + Vector3.up;
            }
            controller.MovePosition(AutoMovement());
        }

        else
        {
            controller.MovePosition(ControlledMovement());
        }
    }

    // Public Methods -----------------------------------------------------------------------------

    /// <summary>
    /// Sends the bee along a path.
    /// </summary>
    /// <param name="controllpoints">The path the bee should follow.</param>
    /// <param name="OnBeeArival">The mehtod called on finishing.</param>
    public void SendBee(Vector3[] controllpoints, System.Action OnBeeArival)
    {
        this.OnBeeArival = OnBeeArival;
        this.controllpoints = controllpoints;
        isAutoControlled = false;
    }

    /// <summary>
    /// Sets the target position. Bee will idle around it.
    /// </summary>
    /// <param name="targetPos">The position to be idleing at.</param>
    public void SwitchTarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        targetPlayer = false;
    }

    /// <summary>
    /// Sets the target to the player. Bee will follow the player.
    /// </summary>
    public void TargetPlayer()
    {
        EndControll();
        Vector2 deltaDistance = player.PlayerCenter - (Vector2)transform.position;
        if (Physics2D.Raycast(transform.position, deltaDistance, deltaDistance.magnitude, obstacleLayers))
        {
            PortBeeToPlayer();
        }
        targetPlayer = true;
    }

    /// <summary>
    /// Sets the position of the bee to the players.
    /// </summary>
    /// <returns>Return the old position of the bee.</returns>
    public Vector2 PortBeeToPlayer()
    {
        Vector2 playerPos = player.PlayerCenter + Vector2.up * 0.05f;
        Vector2 beePos = transform.position;
        EndControll();
        transform.position = playerPos;
        targetPos = transform.position;
        return beePos;
    }

    // Private Methods ----------------------------------------------------------------------------

    /// <summary>
    /// Moves the bee to target position.
    /// </summary>
    /// <returns>Returns the new position.</returns>
    private Vector2 AutoMovement()
    {
        Vector2 deltaDistance = targetPos - (Vector2)transform.position;
        if (deltaDistance.magnitude > maxDistanceToPlayer)
        {
            float maxMove = deltaDistance.magnitude / 2;
            if (maxMove > speed)
            {
                maxMove = speed;
            }

            FlipSprite(deltaDistance.x);
            animator.SetBool("isMoving", true);
            return (Vector2)transform.position + deltaDistance.normalized * maxMove * Time.deltaTime;
        }
        animator.SetBool("isMoving", false);
        return transform.position;
    }

    /// <summary>
    /// Moves the bee along the path.
    /// </summary>
    /// <returns>Returns the new position.</returns>
    private Vector2 ControlledMovement()
    {
        float maxDistance = speed * Time.deltaTime;
        Vector2 deltaDistance = Vector2.zero;
        for (int i = currentControllpointIndex; i < controllpoints.Length; i++)
        {
            Vector2 newDelta = controllpoints[i] - transform.position;
            Debug.DrawRay(transform.position, newDelta, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, newDelta, newDelta.magnitude, obstacleLayers);
            if (!hit)
            {
                deltaDistance = newDelta;
                currentControllpointIndex = i;
                if (deltaDistance.magnitude < maxDistance) { continue; }
            }
            else
            {
                if (newDelta.magnitude < 0.01f)
                {
                    return transform.position;
                }
                else
                {
                    return hit.point - newDelta.normalized * 0.01f;
                }
            }

            FlipSprite(deltaDistance.x);
            animator.SetBool("isMoving", true);
            return (Vector2)transform.position + deltaDistance.normalized * maxDistance;
        }
        animator.SetBool("isMoving", false);
        return EndControll();
    }

    /// <summary>
    /// Stops the bee from following the path.
    /// </summary>
    private Vector2 EndControll()
    {
        if (!isAutoControlled)
        {
            Vector2 beePos = CurrentControllPoint;
            SwitchTarget(beePos);
            OnBeeArival();
            isAutoControlled = true;
            controllpoints = null;
            currentControllpointIndex = 0;
            OnBeeArival = null;
            return beePos;
        }
        return transform.position;
    }

    private void FlipSprite(float speed)
    {
        if (speed > 0 && renderer.flipX)
        {
            renderer.flipX = false;
        }
        else if (speed < 0 && !renderer.flipX)
        {
            renderer.flipX = true;
        }
    }
}
