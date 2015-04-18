using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public Sprite GuiIcon;
	public ItemType itemType;
	public string itemName;
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnMouseOver() {
		if(Player.instance.canUseAt(transform.position)){
			Game.instance.GetState<GamePlaying>().setHoveringText(itemName);
			Game.instance.GetState<GamePlaying>().setCursorHoveringItem();
		}
	}
	
	void OnMouseExit() {
		Game.instance.GetState<GamePlaying>().setNormalCursor();
		Game.instance.GetState<GamePlaying>().clearTextOnMouse();
	}
	
	void OnMouseDown(){
		if(Player.instance.canUseAt(transform.position)){
			Game.instance.GetState<GamePlaying>().clickOn(this);
		}
	}
}
