using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public float baseMovementSpeed;
	[Disable] public float movementSpeed;
	
	public static Player instance;
	
	public float pickupDistance = 2;
	
	public List<Item> items;
	public Item selectedItem;
	public int selectedItemSlot = -1;
	
	
	
	void Awake(){
		Player.instance = this;
	}
	
	void Start () {
		movementSpeed = baseMovementSpeed;
	}
	
	void Update () {
		Camera.main.transform.position = transform.position;
		Camera.main.transform.Translate(0,0,-10);
		
	}

	public bool canUseAt(Vector3 position) {
		return (transform.position - position).magnitude < pickupDistance;
	}

	public bool hasItemSelect() {
		return selectedItemSlot != -1;
	}

	public bool hasItemOfTypeSelected(ItemType itemType) {
		return selectedItemSlot != -1 && selectedItem.itemType.Equals(itemType);
	}
	
	public bool hasItemOfTypeSelected(params ItemType[] itemsType) {
		if(selectedItemSlot != -1) return false;
		
		foreach (var type in itemsType) {
			if(selectedItem.itemType.Equals(type)) return true;
		}
		return false;
	}
}
