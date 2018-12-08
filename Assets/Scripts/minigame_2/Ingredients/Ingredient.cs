using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ingredient : IInteractable {
	private GameObject visual_obj = null;
	private Sprite visual_sprite = null;
	private int ammount = 0;
	private UnityEvent onAmmountZero = null;

	public Ingredient(GameObject visual_obj, Sprite visual_sprite, int ammount, UnityEvent onAmmountZero) {
		this.visual_obj = visual_obj;
		this.visual_sprite = visual_sprite;
		this.ammount = ammount;

		if(onAmmountZero == null) this.onAmmountZero = new UnityEvent();
		this.onAmmountZero = onAmmountZero;
	}

	public int takeIngredient() {
		ammount -= 1;

		if(ammount <= 0) {
			onAmmountZero.Invoke();
			return 0;
		} else {
			return ammount;
		}
	} 

	public GameObject GetVisualObj {
		get {
			return visual_obj;
		}
	}

	public Sprite GetVisualSprite {
		get {
			return visual_sprite;
		}
	}

	public int GetAmmount {
		get {
			return ammount;
		}
	}
}
