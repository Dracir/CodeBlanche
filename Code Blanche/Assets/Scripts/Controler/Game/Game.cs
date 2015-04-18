using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Game : StateLayer {
	
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	public static Game instance;
	
	public string mapSet;
	
	public GameObject playerGo;
	[Disable] public Player player;
	
	public override void OnAwake(){
		base.OnAwake();
		Game.instance = this;
		player = playerGo.GetComponent<Player>();
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
