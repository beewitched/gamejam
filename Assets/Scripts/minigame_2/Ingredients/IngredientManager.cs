using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour {

	private List<Ingredient> ingredients = new List<Ingredient>();
	private Dictionary<GameObject, Transform> spawnedObjects = new Dictionary<GameObject, Transform>();
	private static IngredientManager instance;
	public List<Transform> ingredient_spawn = new List<Transform>();

	public static IngredientManager Instance {
		get{
			return instance;
		}
	}

	void Awake() {
		if(instance == null) {
			instance = this;
		}
	}

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void spawnIngredients() {
		// Check if there are spawn points and ingredients, if not return 
		if(ingredient_spawn.Count > 0 && ingredients.Count > 0) return; 

		// Check if the ammount of spawnpoints is greaten than the ammount of ingredients
		if(ingredient_spawn.Count < ingredients.Count) return;

		int index = 0;
		foreach(Ingredient i in ingredients) {
			spawnedObjects.Add(
				Instantiate(i.GetVisualObj, ingredient_spawn[index].position, Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject,
				ingredient_spawn[index]
			);

			index++;
		}
	}
}
