using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // --- | Singelton | --------------------------------------------------------------------------

    private static Inventory instance;
    public static Inventory Instance
    {
        get { return instance; }
    }


    // --- | Variables& Propterties | -------------------------------------------------------------

    private List<PickUpInfo> items = new List<PickUpInfo>();
    public List<PickUpInfo> Items
    {
        get
        {
            return items;
        }
    }


    // --- | Methods | ----------------------------------------------------------------------------

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public bool AddItem(PickUpInfo info)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Group == info.Group)
            {
                return false;
            }
        }
        items.Add(info);
        return true;
    }
}
