using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterSegment : MonoBehaviour
{
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    private Sprite[] sprites;

    public BetterSegment(int offset, Transform parent, Vector2 position)
    {
        GameObject gameObject = new GameObject("Segment");
        gameObject.transform.parent = parent;
        gameObject.transform.localPosition = position;
        BetterSegment segment = gameObject.AddComponent<BetterSegment>();
        segment.offset = offset < 0 ? 0 : offset;
    }
}
