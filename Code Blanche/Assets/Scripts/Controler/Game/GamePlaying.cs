using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using UnityEngine.UI;

public class GamePlaying : State {
	
    Game Layer {
    	get { return ((Game)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	public Texture2D idleCursor;
	public Texture2D pickupCursor;
	public Texture2D useCursor;
	
	public bool showText;
	public Text mouseHoverText;
	
	[Disable] public int firstEmptySlot = 0;
	
	private Vector2 hotspotCursor = new Vector2(16,16);
	
	public Player player;
	
	public ItemPanel itemPanel;
	
	public override void OnStart() {
		base.OnStart();
		player = Player.instance;
	}
	
	
	
	public override void OnEnter() {
		base.OnEnter();
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate(){
		base.OnUpdate();
		
		movePlayer();
		handleSelect();
		handleDropItem();
		hideShowInventory();
		if(showText) moveHoverTextToMouse();
	}
	
	public void setCursorHoveringItem(){
		Cursor.SetCursor(pickupCursor,hotspotCursor, CursorMode.Auto);
	}
	
	public void setNormalCursor(){
		Cursor.SetCursor(idleCursor,hotspotCursor, CursorMode.Auto);
	}
	
	public void setUseCursor(){
		Cursor.SetCursor(useCursor,hotspotCursor, CursorMode.Auto);
	}
	

	public void clickOn(Item item) {
		if(hasEmptySlot()){
			pickupItem(item);
		}
		
	}
	
	

	#region itemSelect
	void handleSelect() {
		if(Input.GetKeyDown(KeyCode.Alpha1)) toggleSelectItem(0);
		if(Input.GetKeyDown(KeyCode.Alpha2)) toggleSelectItem(1);
		if(Input.GetKeyDown(KeyCode.Alpha3)) toggleSelectItem(2);
		if(Input.GetKeyDown(KeyCode.Alpha4)) toggleSelectItem(3);
		if(Input.GetKeyDown(KeyCode.Alpha5)) toggleSelectItem(4);
		if(Input.GetKeyDown(KeyCode.Alpha6)) toggleSelectItem(5);
	}

	void toggleSelectItem(int index) {
		if(player.items.Count >  index){
			if(player.selectedItemSlot == index){
				player.selectedItemSlot = -1;
				player.selectedItem = null;
			}else {
				player.selectedItem = player.items[index];
				player.selectedItemSlot = index;
			}
		}
		itemPanel.refresh();
	}
	#endregion
	
	
	void handleDropItem() {
		if(Input.GetKeyDown(KeyCode.Q) && player.selectedItemSlot != -1){
			Item item = player.selectedItem;
			player.selectedItemSlot = -1;
			player.selectedItem = null;
			
			player.items.Remove(item);
			item.transform.parent = null;
			item.GetComponent<SpriteRenderer>().enabled = true;
			item.GetComponent<BoxCollider2D>().enabled = true;
			item.transform.position = player.transform.position;
			itemPanel.refresh();
		}
	}

	public void removeCurrentItem() {
		Item item = player.selectedItem;
		player.selectedItemSlot = -1;
		player.selectedItem = null;
		
		player.items.Remove(item);
		item.gameObject.Remove();

		itemPanel.refresh();
	}
	
	bool hasEmptySlot() {
		return Player.instance.items.Count <= 6; // MAGIC NUMBER :)
	}
	
	void pickupItem(Item item) {
		Player.instance.items.Add(item);
		item.GetComponent<SpriteRenderer>().enabled = false;
		item.GetComponent<BoxCollider2D>().enabled = false;
		item.transform.parent = Player.instance.transform;
		itemPanel.refresh();
	}
	
	void movePlayer() {
		Vector3 movementIntention = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);
		movementIntention.Normalize();
		Vector3 movement = movementIntention * Layer.player.movementSpeed * Time.deltaTime;
		
		Layer.playerGo.transform.position += movement;
		
		Vector3 forward = Layer.playerGo.transform.position + movementIntention;
		
		if(movementIntention.magnitude > 0){
			var dir = movementIntention;
			var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
			Layer.playerGo.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	void hideShowInventory() {
		if(Input.GetKeyDown(KeyCode.I)){
			itemPanel.toggleVisibility();	
		}
	}
	
	
	public void setHoveringText(string text, bool overwrite = false) {
		if(!showText || overwrite){
			mouseHoverText.text = text;
			showText = true;
		}
		
	}

	public void clearTextOnMouse() {
		mouseHoverText.text = "";
		showText = false;
	}

	void moveHoverTextToMouse() {
		mouseHoverText.rectTransform.position = Input.mousePosition;
	}
}
