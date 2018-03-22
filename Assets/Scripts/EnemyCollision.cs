using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {

	public float damage = 1;

	AudioSource audio;
	public AudioClip audioClip;
	GameObject cam;

	void Start(){
		audio = GetComponent<AudioSource> ();
		cam = GameObject.FindWithTag("MainCamera");
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Enemy")){
			other.SendMessage ("ApplyDamage",damage);
			damage = 0;
			Destroy(gameObject, 0.03f);
		}

		if (other.CompareTag("BlockWeapon")){
			Revert();
		}
    }

	void Revert(){
		float value = Mathf.Abs(transform.GetComponent<Rigidbody2D>().velocity.x);
		Vector2 forceInverse = transform.GetComponent<Rigidbody2D>().velocity * -1;
		Vector2 force = forceInverse + (Vector2.up * value);
		transform.GetComponent<Rigidbody2D>().velocity = force;
		PlaySound();
	}

	void Update () {
		if(transform.gameObject.CompareTag("Weapon")){
			Vector3 viewPos = cam.GetComponent<Camera>().WorldToViewportPoint(transform.position);
			if (viewPos.x > 1 || viewPos.x < 0 || viewPos.y > 1 || viewPos.y < 0){
				Destroy(gameObject);
			}
			Destroy(gameObject, 3f);
		}
	}

	void PlaySound(){
		audio.clip = audioClip;
		audio.Play();
	}

}