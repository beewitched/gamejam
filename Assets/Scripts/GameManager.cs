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
    public PickUpInfo[] GetCookingList {
        get { return cookingList; }
    }

    [SerializeField]
    private PickUp[] recipe = new PickUp[2];
    private PickUpInfo[] cookingList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            enabled = false;
            return;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        cookingList = new PickUpInfo[recipe.Length];
        for (int i = 0; i < recipe.Length; i++)
        {
            cookingList[i] = new PickUpInfo(recipe[i].Name, recipe[i].Group, recipe[i].Renderer.sprite);
        }
    }

    public bool CheckInventory()
    { 
        for(int i = 0; i < cookingList.Length; i++)
        {
            if (cookingList[i] == null)
            {
                continue;
            }

            bool found = false;
            for(int y = 0; y < Inventory.Instance.Items.Length; y++)
            {
                if(Inventory.Instance.Items[y] == null)
                {
                    continue;
                }
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
