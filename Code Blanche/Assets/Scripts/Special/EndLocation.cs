using UnityEngine;
using System.Collections;

public class EndLocation : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			Game.instance.SwitchState<GameSwitchingLevel>();
		}
	}
}
