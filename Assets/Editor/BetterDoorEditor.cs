using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BetterDoor))]
public class BetterDoorEditor : Editor
{
     public override void OnInspectorGUI()
    {
        BetterDoor door = (BetterDoor)target;

        DrawDefaultInspector();

        int childs = Mathf.CeilToInt(door.Height);
        while (childs < door.transform.childCount)
        {
            DestroyImmediate(door.transform.GetChild(door.transform.childCount - 1).gameObject);
        }
        while (childs > door.transform.childCount)
        {
            new BetterSegment(door.transform.childCount * 2, door.transform, Vector2.up * door.transform.childCount);
        }

    }
}
