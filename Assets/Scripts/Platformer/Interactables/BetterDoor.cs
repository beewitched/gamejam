using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterDoor : MonoBehaviour
{
    [SerializeField]
    private float height = 2.5f;
    public float Height
    {
        get
        {
            return height;
        }
    }
}
