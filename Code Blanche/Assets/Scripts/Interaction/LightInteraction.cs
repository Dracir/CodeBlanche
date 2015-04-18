using UnityEngine;
using System.Collections;

public class LightInteraction : MonoBehaviour {

	public Player player;
	public bool hasBulb = true;
	public GameObject lightBulbPrefab;
	
	void Start () {
		player = Player.instance;
	}
	
	void Update () {
	
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
		if(hasBulb){
			if(player.hasItemOfTypeSelected(ItemType.Pillow,ItemType.PillowCase )  ){
				Game.instance.GetState<GamePlaying>().setHoveringText("Remove Light bulb");
				Game.instance.GetState<GamePlaying>().setUseCursor();
			}
		}else{
			if(player.hasItemOfTypeSelected(ItemType.LightBulb)){
				Game.instance.GetState<GamePlaying>().setHoveringText("Put Light bulb in");
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
				Game.instance.GetState<GamePlaying>().setHoveringText("A Light bulb. I could pick it up but it's too hot for my hands!");
				
			}else if(player.hasItemOfTypeSelected(ItemType.Pillow) && hasBulb){
				removeBulb();
				
			}else if(player.hasItemOfTypeSelected(ItemType.PillowCase) && hasBulb){
				removeBulb();
				
			}else if(player.hasItemOfTypeSelected(ItemType.LightBulb) && !hasBulb){
				addBulb();
			}
		}
	}

	
	void removeBulb() {
		hasBulb = false;
		var lights = GetComponentsInChildren<Light>();
		foreach (var lightComponent in lights) {
			lightComponent.enabled = false;
		}
		GameObjectExtend.createClone(lightBulbPrefab,null, transform.position);
	}
	
	
	void addBulb() {
		hasBulb = true;
		var lights = GetComponentsInChildren<Light>();
		foreach (var lightComponent in lights) {
			lightComponent.enabled = true;
		}
		Game.instance.GetState<GamePlaying>().removeCurrentItem();
	}
}
