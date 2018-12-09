using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterSegment : MonoBehaviour
{
    [SerializeField]
    private int offset = 0;

    public BetterSegment(int offset, Transform parent)
    {
        GameObject gameObject = new GameObject("Segment");
        gameObject.transform.parent = parent;
        gameObject.transform.position = Vector3.zero;
        BetterSegment segment = gameObject.AddComponent<BetterSegment>();
        segment.offset = offset < 0 ? 0 : offset;
    }
}
