using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Area : MonoBehaviour {

	private Enemy3 enemy3;
	private void Start() {
		enemy3 = transform.parent.GetComponent<Enemy3>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			enemy3.canAttack = true;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.CompareTag("Player")){
			enemy3.canAttack = true;
		}
	}
}
