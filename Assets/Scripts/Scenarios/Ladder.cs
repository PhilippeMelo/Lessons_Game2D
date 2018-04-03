using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

	public GameObject platformLadder;

	void Update () {
		if(PlayerController.usingLadder){
			platformLadder.GetComponent<BoxCollider2D>().enabled = false;
		}else{
			if(!PlayerController.jumpOnLadder){
				platformLadder.GetComponent<BoxCollider2D>().enabled = true;
			}
		}
	}
}
