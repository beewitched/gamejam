using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private PickUp[] cookingList = new PickUp[2];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
