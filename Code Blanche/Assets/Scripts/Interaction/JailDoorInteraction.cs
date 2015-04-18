using UnityEngine;
using System.Collections.Generic;
using Magicolo;

public class JailDoorInteraction : MonoBehaviour {

	public Player player;
	
	public JailDoorInteraction neighbourDoor;
	public List<Light> nearLights = new List<Light>();
	
	bool locked = true;
	
	void Start () {
		player = Player.instance;
		if(neighbourDoor == null) findNeighbourDoor();
		findNearLights();
	}

	void findNeighbourDoor() {
		foreach (var door in Object.FindObjectsOfType<JailDoorInteraction>()) {
			if(Vector3.Distance(transform.position, door.transform.position) < 1.1f){
				neighbourDoor = door;
				door.neighbourDoor = this;
				return;
			}
		}
		Debug.LogWarning("Jail door without friend.. " + transform.position);
	}

	void findNearLights() {
		foreach (var foundLight in Object.FindObjectsOfType<LightInteraction>()) {
			Light lightSource = foundLight.GetComponent<Light>();
			if(Vector3.Distance(transform.position, lightSource.transform.position) < lightSource.range){
				nearLights.Add(lightSource);
			}
		}
	}

	
	void OnMouseOver() {
		if(Player.instance.canUseAt(transform.position)){
			if(player.hasItemSelect()){
				setHoverText();
			}else{
				Game.instance.GetState<GamePlaying>().setUseCursor(); //TODO potentiel ? cursor ?
			}
			
		}
	}
	
	void setHoverText() {
		if(locked){
			if(player.hasItemOfTypeSelected(ItemType.LightBulb )  ){
				Game.instance.GetState<GamePlaying>().setHoveringText("Lockpick the door");
				Game.instance.GetState<GamePlaying>().setUseCursor();
			}
		}
	}
	
	void OnMouseExit() {
		Game.instance.GetState<GamePlaying>().setNormalCursor();
		Game.instance.GetState<GamePlaying>().clearTextOnMouse();
	}
	
	void OnMouseDown(){
		if(player.canUseAt(transform.position)){
			
			if(!player.hasItemSelect()){
				Game.instance.GetState<GamePlaying>().setHoveringText("A locked door");
				
			}else if(player.hasItemOfTypeSelected(ItemType.LightBulb) && locked){
				if(canSee()){
					locked = false;
					Game.instance.GetState<GamePlaying>().setNormalCursor();
					Game.instance.GetState<GamePlaying>().clearTextOnMouse();
					removeDoor();
				}else{
					Game.instance.GetState<GamePlaying>().setHoveringText("It's too hard to lockpick without light.", true);
				}
				
				
			}
		}
	}

	bool canSee() {
		if(nearLights.Count == 0) return false;
		
		foreach (var nearLight in nearLights) {
			if(!nearLight.enabled) return false;
		}
		
		return true;
	}
	void removeDoor() {
		neighbourDoor.gameObject.Remove();
		gameObject.Remove();
	}
}
