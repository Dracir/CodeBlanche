﻿using UnityEngine;
using System.Collections;
using Rick.RandomBags;

public class RandomBagTest : MonoBehaviour {

	public bool test;
		
	void OnDrawGizmos () {
		if(test){
			test = false;
			RandomBag<int> bag = new RandomBag<int>(new System.Random());
			bag.add(1);
			bag.add(2);
		}
	}
	
}
