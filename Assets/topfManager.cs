using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topfManager : MonoBehaviour {

    public SpriteRenderer[] ingredients;
    public Dialogue dialog;

    private PickUpInfo[] items;

    // Use this for initialization
    void Start () {
        DialogueManager.Instance.StartDialogue(dialog);
        items = Inventory.Instance.Items;
        for(int i = 0; i < items.Length; i++)
        {
            Debug.Log("a");
            ingredients[i].sprite = items[i].Icon;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
