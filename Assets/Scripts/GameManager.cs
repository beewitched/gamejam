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

    public PickUp[] GetCookingList {
        get { return cookingList; }
    }

    [SerializeField]
    private PickUp[] cookingList = new PickUp[2];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public bool CheckInventory()
    { 
        for(int i = 0; i < cookingList.Length; i++)
        {
            bool found = false;
            for(int y = 0; y < Inventory.Instance.Items.Length; y++)
            {
                if(cookingList[i].Name == Inventory.Instance.Items[y].Name)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                return false;
            }
        }
        return true;
    }
}
