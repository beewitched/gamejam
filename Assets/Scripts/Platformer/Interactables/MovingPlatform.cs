using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField]
    private bool loop = false;

    [SerializeField]
    private Vector2[] controlPoints = new Vector2[2];


    private void OnDrawGizmos()
    {
        for (int i = 1; i < controlPoints.Length; i++)
        {
            Gizmos.DrawLine(controlPoints[i - 1], controlPoints[i]);
        }
        if (loop)
        {
            Gizmos.DrawLine(controlPoints[controlPoints.Length - 1], controlPoints[0]);
        }
    }
}
