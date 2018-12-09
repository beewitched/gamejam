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

    private List<PickUpInfo> pickups = new List<PickUpInfo>();


    // --- | Methods | ----------------------------------------------------------------------------

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public bool AddPickUp(PickUpInfo info)
    {
        for (int i = 0; i < pickups.Count; i++)
        {
            if (pickups[i].Group == info.Group)
            {
                return false;
            }
        }
        pickups.Add(info);
        return true;
    }
}
