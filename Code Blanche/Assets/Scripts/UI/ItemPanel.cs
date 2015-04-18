using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ItemPanel : MonoBehaviour {

	public Sprite frameUsingItem;
	public Sprite frameNeutreITem;
	
	public Image[] itemSlots;
	
	public Player player;
	
	void Start () {
		for (int index = 0; index < itemSlots.Length; index++) {
			itemSlots[index].transform.GetChild(0).gameObject.SetActive(true);
		}
		refresh();
	}
	
	void Update () {
		
	}

	public void refresh() {
		int nbItems = player.items.Count;
		for (int index = nbItems; index < itemSlots.Length; index++) {
			itemSlots[index].gameObject.SetActive( false );
		}
		for (int index = 0; index < nbItems; index++) {
			itemSlots[index].gameObject.SetActive(true);
			itemSlots[index].sprite = frameNeutreITem;
			itemSlots[index].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = player.items[index].GuiIcon;
		}
		if(player.selectedItemSlot != -1){
			itemSlots[player.selectedItemSlot].sprite = frameUsingItem;
		}
	}
	
	public void toggleVisibility(){
		gameObject.SetActive(!gameObject.active);
	}
}
