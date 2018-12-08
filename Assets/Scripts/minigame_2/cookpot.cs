using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class cookpot : MonoBehaviour {

    public float heat = .5f;
    public float heatDecrease = 0.01f;
    public float heatDecreaseRate = 1f;
    public float increaseHeatRate = 0.3f;
    public List<string> ingrediens = new List<string>();
    private Collider col;
    private bool prevFueled = false;

    public Transform min;
    public Transform max;   
    public Image needle;

	void Start () {
        col = GetComponent<SphereCollider>();
        SetupNeedle();

        InvokeRepeating("DecreaseHeat", Time.deltaTime, heatDecreaseRate);

    }
	
	void Update () {
        if(heat > 0)
        {
            float range = max.position.y - min.position.y;
            float posY = range * heat + min.position.y;
            needle.transform.position = new Vector2(min.position.x, posY);
        }
	}
    
    private void SetupNeedle()
    {
        Vector2 newPos = (Vector2)min.position + (Vector2)(max.position - min.position) / 2;
        needle.transform.position = newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ingredient>() == null) return;

        other.GetComponent<Ingredient>().Interact();
        ingrediens.Add(other.GetComponent<Ingredient>().ingredient_name);
    }

    private void DecreaseHeat()
    {
        // End
        if (heat <= 0) return;

        heat -= heatDecrease;
    }

    public void AddHeat()
    {
        if(heat < 1f)
        {
            heat += increaseHeatRate;
            if (heat > 1f) heat = 1f;
        }
    }
}
