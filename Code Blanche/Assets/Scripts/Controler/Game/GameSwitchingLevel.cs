using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GameSwitchingLevel : State {
	
    Game Layer {
    	get { return ((Game)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	
	public int currentMapIndex = -1;
	public GameObject[] maps;
	public GameObject currentMap;
	
	
	public override void OnAwake(){
		base.OnAwake();
		maps = Resources.LoadAll<GameObject>("Map/" + Layer.mapSet);
	}
	
	public override void OnEnter() {
		base.OnEnter();
		if(currentMap != null){
			currentMap.Remove();
		}
		
		if(hasNextMap()){
			loadNextMap();
		}
		
		SwitchState<GamePlaying>();
		
	}

	bool hasNextMap() {
		return maps.Length > currentMapIndex + 1;
	}

	void loadNextMap() {
		currentMapIndex++;
		currentMap = GameObjectExtend.createClone(maps[currentMapIndex]);
		GameObject startLocation = currentMap.FindChildRecursive("StartLocation");
		Layer.playerGo.transform.position = startLocation.transform.position;
	}

	public override void OnExit() {
		base.OnExit();
		
	}
}
