using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {

	public bool idle = true;
	public float damage;
	public float speed;
	private Rigidbody2D rb2d;
	private float direction;
	private Animator anim;
	public float health;

	private bool isDamageable;
	public GameObject explosionEffect;

	private GameObject cam;

	void Start () {
		idle = true;
		damage = 0;
		speed = 3.4f;
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		direction = 0;
		health = 5;
		isDamageable = false;
		cam = GameObject.FindWithTag("MainCamera");
	}
	
	void Update () {
		anim.SetBool ("isIdle", idle);
		rb2d.velocity = new Vector2 (direction * speed, rb2d.velocity.y);
		if (!idle){
			isDamageable = true;

			Vector3 viewPos = cam.GetComponent<Camera>().WorldToViewportPoint(transform.position);
            if (viewPos.x > 2 || viewPos.x < -1 || viewPos.y > 1 || viewPos.y < 0){
                Destroy(gameObject);
            }
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			if (idle){
				idle = false;
				transform.gameObject.tag = "Enemy";
				damage = 1;

				if (other.transform.position.x < transform.position.x){
					direction = -1;
				}else{
					direction = 1;
				}
			}else{
				DamagePlayer(other);
			}
		}
	}

	void DamagePlayer(Collider2D player){
		if (LifeController.isDamageable){
			if (player.transform.position.x < transform.position.x){
				PlayerController.knockRight = true;
			}else{
				PlayerController.knockRight = false;
			}
			player.SendMessage("ApplyDamage",damage);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Stop"){
			direction = -1 * direction;
		}
	}	
	
	void ApplyDamage(float damage){
		if (isDamageable && damage > 0){
			StartCoroutine(FlashingDamage ());
			health -= damage;
			
			if (health <= 0) {
				GetComponent<Renderer>().enabled = false;
				Instantiate (explosionEffect, gameObject.transform.position, Quaternion.identity);
				Destroy(gameObject);
			}

			isDamageable = false;
			Invoke ("ResetIsDamageable", 1.2f);
		}
	}

	void ResetIsDamageable(){
		isDamageable = true;
	}

	IEnumerator FlashingDamage(){
		int i = 0;
		while(i<8){
			GetComponent<Renderer>().enabled = false;
			yield return new WaitForSeconds(0.05f);
			GetComponent<Renderer>().enabled = true;
			yield return new WaitForSeconds(0.05f);
			i++;
		}
		GetComponent<Renderer>().enabled = true;
	}


}
