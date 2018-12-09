﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    // --- | Singelton | --------------------------------------------------------------------------

    private static Inventory instance;
    public static Inventory Instance
    {
        get { return instance; }
    }


    // --- | Serialized | -------------------------------------------------------------------------

    [Header("Settings")]
    [SerializeField]
    private int inventorySize = 3;

    [Header("Events")]
    private UnityEvent OnInventoryFull;


    // --- | Variables& Propterties | -------------------------------------------------------------

    private PickUpInfo[] items;
    public PickUpInfo[] Items
    {
        get
        {
            return items;
        }
    }
    private int itemCount = 0;
    private bool isFull = false;


    // --- | Methods | ----------------------------------------------------------------------------

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);

        items = new PickUpInfo[inventorySize];
    }

    public bool AddItem(PickUpInfo info)
    {
        if (isFull) { return false; }
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Group == info.Group)
            {
                return false;
            }
        }

        itemCount++;
        items[itemCount] = info;
        if (itemCount >= inventorySize)
        {
            isFull = true;
            OnInventoryFull.Invoke();
        }
        return true;
    }
}
