using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCollider : MonoBehaviour {

	AudioSource audio;
	public AudioClip[] audioClip;

	void Start(){
		audio = GetComponent<AudioSource> ();
	}

	void OnTriggerStay2D(Collider2D col){
		if(col.CompareTag("Ladder")){
			PlayerController.onLadder = true;
			PlayerController.ladderPositionX = col.transform.position.x;
		}

		if(col.CompareTag("TopLadder")){
			PlayerController.topLadder = true;
		}

		if(col.CompareTag("TopLadder2")){
			PlayerController.playerAboveLadder = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if(col.CompareTag("Ladder")){
			PlayerController.onLadder = false;
		}

		if(col.CompareTag("TopLadder")){
			PlayerController.topLadder = false;

			if(PlayerController.verticalForceButton > 0){
				PlayerController.getOffLadder = true;
			}
		}

		if(col.CompareTag("TopLadder2")){
			PlayerController.playerAboveLadder = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.CompareTag("Ground")){
			PlayerController.usingLadder = false;

			if(PlayerController.verticalSpeed < -2){
				PlaySound(0);
			}
		}
	}

	void PlaySound(int id){
		audio.clip = audioClip [id];
		audio.Play();
	}

}