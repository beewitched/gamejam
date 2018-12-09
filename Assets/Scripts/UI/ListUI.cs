using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListUI : MonoBehaviour {
    public GameObject ingredientDisplay;
    public GameObject parentList;
    private PickUpInfo[] pickUps;
    public KeyCode openCloseKey = KeyCode.Tab;
    private bool open = false;
    public Transform openPos;
    public Transform closePos;
    void Start () {
        pickUps = GameManager.Instance.GetCookingList;

        BuildUI(pickUps);

	}
	
	// Update is called once per frame
	void Update () {
        if(open)
        {
            transform.position = openPos.position;
        } else
        {
            transform.position = closePos.position;
        }

        if(Input.GetKeyDown(openCloseKey)) 
        {
            if (open) open = false;
            else open = true;
        } 
	}

    public void UpdateItems()
    {
        PickUpInfo[] pickupsInventory = Inventory.Instance.Items;

        foreach(PickUpInfo i in pickupsInventory)
        {
            foreach(PickUpInfo p in pickUps)
            {
                if(i != null)
                {
                    if (p.Group.Equals(i.Group))
                    {
                        Transform item = parentList.transform.transform.Find(i.Group);
                        item.GetComponent<Image>().color = Color.black;
                    }
                }

            }
        }
    }

    private void BuildUI(PickUpInfo[] list)
    {
        foreach(PickUpInfo p in list)
        {
            GameObject obj = Instantiate(ingredientDisplay, parentList.transform) as GameObject;
            obj.GetComponent<Image>().sprite = p.Icon;
            obj.name = p.Group;
            if(p.Name.StartsWith("Ei"))
            {
                obj.transform.localScale = new Vector3(1.5f, 1.5f);
            }
        }
    }
}
