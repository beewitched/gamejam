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
            Gizmos.DrawLine(controlPoints[i - 1], controlPoints[i]);
        }
        if (loop&& controlPoints.Length > 2)
        {
            Gizmos.DrawLine(controlPoints[controlPoints.Length - 1], controlPoints[0]);
        }
    }

    private void Awake()
    {
        controller = GetComponent<Rigidbody2D>();
        path = GetComponent<LineRenderer>();
        path.positionCount = controlPoints.Length;
        path.SetPositions(controlPoints);
        path.loop = loop;

        for (int i = 0; i < controlPoints.Length; i++)
        {
            controlPoints[i] = transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            Vector2 deltaDistance = NextControllpoint - (Vector2)transform.position;

            if (deltaDistance.magnitude < speed * Time.deltaTime)
            {
                controller.velocity = deltaDistance;
                SetNextControllPoint();
            }
            else
            {
                controller.velocity = deltaDistance.normalized * speed;
            }
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
