using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
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


    // --- | Componetns | -------------------------------------------------------------------------

    private Rigidbody2D controller;


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


    // --- | Methods | ----------------------------------------------------------------------------

    // MonoBehaviour --------------------------------------

    private void Awake()
    {
        controller = GetComponent<Rigidbody2D>();
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
        targetPlayer = true;
        transform.position = (player.transform.position + Vector3.up);
    }

    /// <summary>
    /// Sets the position of the bee to the players.
    /// </summary>
    /// <returns>Return the old position of the bee.</returns>
    public Vector2 PortBeeToPlayer()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 beePos = transform.position;
        EndControll();
        if (!targetPlayer)
        {
            targetPos = playerPos;
        }
        transform.position = playerPos;
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
            return (Vector2)transform.position + deltaDistance.normalized * maxMove * Time.deltaTime;
        }
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
            deltaDistance = controllpoints[i] - transform.position;
            currentControllpointIndex = i;
            if (deltaDistance.magnitude < maxDistance) { continue; }


            return (Vector2)transform.position + deltaDistance.normalized * maxDistance;
        }
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
}
