using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class cookpot : MonoBehaviour {

    private float heat = 50f;
    public List<string> ingrediens = new List<string>();
    private Collider col;
    private bool prevFueled = false;

    public Transform min;
    public Transform max;
    public Image needle;

	void Start () {
        col = GetComponent<SphereCollider>();
        SetupNeedle();
	}
	
	void Update () {

	}
    
    private void SetupNeedle()
    {
        Vector2 newPos = (Vector2)min.position + (Vector2)(max.position - min.position) / 2;
        Debug.Log(newPos);
        needle.transform.position = newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ingredient>() == null) return;

        other.GetComponent<Ingredient>().Interact();
        ingrediens.Add(other.GetComponent<Ingredient>().ingredient_name);
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
