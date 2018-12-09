using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

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
    private Vector2 NextControllpoint
    {
        get
        {
            return controlPoints[nextControlpointID];
        }
    }

    private Rigidbody2D controller;
    private LineRenderer path;

    private void Awake()
    {
        controller = GetComponent<Rigidbody2D>();
        path = GetComponent<LineRenderer>();
        path.positionCount = controlPoints.Length;
        path.SetPositions(controlPoints);
        path.loop = loop;
    }

    private void FixedUpdate()
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
