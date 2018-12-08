using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Ingredient : MonoBehaviour, IInteractable {

    public string ingredient_name = "IngredientTMP";
    public Sprite sprite = null;
    public int ammount = 0;
    public UnityEvent onAmmountZero = new UnityEvent();

    private Vector3 distance;
    private float posX, posY;
    private Rigidbody rgbd;

    private GameObject draggedObject;
    public bool origin = true;

    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void Pickup()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            distance.z++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            distance.z--    ;
        }

        Vector3 currPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, distance.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(currPos);

        
        if (origin)
        {
            draggedObject.transform.position = worldPos;
        }
        else
        {
            transform.position = worldPos;
        }
    }

    public void Interact()
    {
        Destroy(this.gameObject);
    }

    void Start () {
        this.gameObject.name = ingredient_name;
        this.rgbd = GetComponent<Rigidbody>();

        if(origin) rgbd.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
	
	void Update () {

    }

    private void OnMouseDown()
    {
        if(origin)
        {
            draggedObject = Instantiate(this.gameObject, this.transform.position, Quaternion.identity) as GameObject;
            draggedObject.GetComponent<Ingredient>().origin = false;
            draggedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

        if(origin)
        {
            distance = Camera.main.WorldToScreenPoint(draggedObject.transform.position);
        } else
        {
            distance = Camera.main.WorldToScreenPoint(transform.position);
        }

        posX = Input.mousePosition.x - distance.x;
        posY = Input.mousePosition.y - distance.y;
    }

    private void OnMouseExit()
    {
        rgbd.velocity = Vector3.zero;
    }
    private void OnMouseDrag()
    {
        Pickup();
    }


}
