using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingredientDrop : MonoBehaviour
{
    public Rigidbody rigid;
    public bool dropped = false;
    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Drop()
    {
        rigid.useGravity = true;
        dropped = true;
    }
}
