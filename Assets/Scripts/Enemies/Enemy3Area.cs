using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Area : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			Enemy3.canAttack = true;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.CompareTag("Player")){
			Enemy3.canAttack = true;
		}
	}
}
