using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topfManager : MonoBehaviour {

    public SpriteRenderer[] ingredients;
    public Dialogue dialog;
    public Dialogue dialogTwo;
    public Dialogue dialogFail;
    public GameObject potion;
    public bool animDone = false;

    private PickUpInfo[] items;

    // Use this for initialization
    void Start () {
        DialogueManager.Instance.StartDialogue(dialog);
        items = Inventory.Instance.Items;
        for(int i = 0; i < items.Length; i++)
        {
            ingredients[i].sprite = items[i].Icon;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(ingredients[2].transform.position.y <= -5)
        {
            potion.transform.Translate(Vector3.up * (3 - potion.transform.position.y) * 0.7f * Time.deltaTime);
            if (!animDone && potion.transform.position.y >= 2.5f)
            {
                animDone = true;
                if(GameManager.Instance.CheckInventory())
                {
                    DialogueManager.Instance.StartDialogue(dialogTwo);
                }
                else
                {
                    DialogueManager.Instance.StartDialogue(dialogFail);
                }
                
            }
        }
	}
}
