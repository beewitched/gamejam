using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    //private BoxCollider2D[] collider;
    private DoorSegment[] segments;

    [SerializeField]
    private int segmentCount = 5;
    private int seg = 0;
    private float currSegments = 0;
    [SerializeField]
    private float groathSpeed = 0.5f;
    
    private void Awake()
    {
        segments = GetComponentsInChildren<DoorSegment>();
        //collider = GetComponentsInChildren<BoxCollider2D>();
    }

    private void Start()
    {
        currSegments = segmentCount;
    }

    private void Update()
    {
        if (currSegments < segmentCount)
        {
            currSegments += Time.deltaTime / groathSpeed;

            if (seg != Mathf.Floor(currSegments))
            {
                seg++;
                ShowSegment();
            }
        }
    }

    public void OpenDoor()
    {
        SetHideSegments();
        currSegments = segmentCount;
    }

    public void CloseDoor()
    {
        currSegments = 0;
        seg = 0;
    }

    private void SetHideSegments()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].HideSegment();
            //collider[i].enabled = false;
        }
    }

    private void ShowSegment()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].AddIndex();
            //collider[i].enabled = true;
        }
    }
}
