using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // --- | Serialized | -------------------------------------------------------------------------

    [SerializeField]
    private bool isActive = false;
    [SerializeField]
    private bool loop = false;
    [SerializeField]
    private bool moveDownInList = true;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private Vector3[] controlPoints = new Vector3[2];
    [SerializeField]
    private int nextControlpointID = 0;


    // --- | Componentes | ------------------------------------------------------------------------

    private Rigidbody2D controller;
    private LineRenderer path;


    // --- | Variables & Properties | -------------------------------------------------------------

    private Vector2 NextControllpoint
    {
        get
        {
            return controlPoints[nextControlpointID];
        }
    }


    // --- | Methods | ----------------------------------------------------------------------------

    // MonoBehaviour --------------------------------------

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        for (int i = 1; i < controlPoints.Length; i++)
        {
            Gizmos.DrawLine(controlPoints[i - 1] + transform.position, controlPoints[i] + transform.position);
        }
        if (loop&& controlPoints.Length > 2)
        {
            Gizmos.DrawLine(controlPoints[controlPoints.Length - 1] + transform.position, controlPoints[0] + transform.position);
        }
    }

    private void Awake()
    {
        controller = GetComponent<Rigidbody2D>();
        path = GetComponent<LineRenderer>();

        for (int i = 0; i < controlPoints.Length; i++)
        {
            controlPoints[i] += transform.position;
        }

        path.positionCount = controlPoints.Length;
        path.SetPositions(controlPoints);
        path.loop = loop;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            Vector2 deltaDistance = NextControllpoint - (Vector2)transform.position;
            Vector2 direction;
            if (deltaDistance.magnitude < speed * Time.deltaTime)
            {
                direction = deltaDistance;
                SetNextControllPoint();
            }
            else
            {
                direction = deltaDistance.normalized * speed;
            }
            controller.velocity = direction;

            Player[] players = GetComponentsInChildren<Player>();
            for (int i = 0; i < players.Length; i++)
            {
                players[i].AddForce(direction);
            }
        }
    }

    private void Update()
    {
        path.material.mainTextureScale = new Vector2(Vector3.Distance(controlPoints[0], controlPoints[controlPoints.Length - 1]), 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (collision.contacts[i].normal == Vector2.down)
                {
                    collision.transform.parent = transform;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }

    // Public Methods -------------------------------------

    public void SetActive()
    {
        isActive = true;
    }

    public void SetInactive()
    {
        isActive = false;
    }

    // Private Methods ------------------------------------

    private void SetNextControllPoint()
    {
        nextControlpointID += moveDownInList ? 1 : -1;
        if (loop)
        {
            if (nextControlpointID >= controlPoints.Length)
            {
                nextControlpointID = 0;
            }
            else if (nextControlpointID < 0)
            {
                nextControlpointID = controlPoints.Length - 1;
            }
        }
        else
        {
            if (nextControlpointID >= controlPoints.Length)
            {
                moveDownInList = !moveDownInList;
                nextControlpointID -= 2;
            }
            else if (nextControlpointID < 0)
            {
                moveDownInList = !moveDownInList;
                nextControlpointID += 2;
            }
        }
    }
}
