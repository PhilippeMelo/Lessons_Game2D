using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {
	public bool idle;
	public float damage;
	public bool touchedPlayer;
	public float speed;
	private Transform player;
	public bool moveRight;
	public bool moveLeft;
	private float initialPositionY;
	private Animator anim;
	private float rangePlayerEnemy = 0.2f;

	public GameObject weaponPrefab;
	public Transform pointWeapon;
	public float weaponSpeed = 160;

	public float health;

	private bool isDamageable;
	public GameObject particlesExplosion;
	private bool attacking;

	private GameObject cam;

	void Start () {
		idle = true;
		damage = 1;
		initialPositionY = transform.position.y;
		touchedPlayer = false;
	
		speed = 1f;
		anim = GetComponent<Animator> ();
		moveRight = false;
		moveLeft = false;
		health = 3;

		isDamageable = false;
		attacking = false;

		player = GameObject.FindWithTag("Player").transform;
		cam = GameObject.FindWithTag("MainCamera");
	}

	void ApplyDamage(float damage){
		if (isDamageable && damage > 0){
			StartCoroutine(FlashingDamage ());
			health -= damage;
			
			if (health <= 0) {
				GetComponent<Renderer>().enabled = false;
				Instantiate (particlesExplosion, gameObject.transform.position, Quaternion.identity);
				Destroy(gameObject);
			}

			isDamageable = false;
			Invoke ("ResetIsDamageable", 1.2f);
		}
	}

	void Update () {

		anim.SetBool ("idle", idle);

		if (player.transform.position.x > transform.position.x + rangePlayerEnemy){
			moveRight = true;
		}else{
			moveRight = false;
		}

		if (player.transform.position.x < transform.position.x - rangePlayerEnemy){
			moveLeft = true;
		}else{
			moveLeft = false;
		}

		if (!idle){
			isDamageable = true;
			transform.gameObject.tag = "Enemy";

			if(!touchedPlayer){
				if (transform.position.y > player.transform.position.y + rangePlayerEnemy){
					transform.Translate(0,speed * 0.8f * -1 * Time.deltaTime,0);
				}

				if (transform.position.y < initialPositionY - 0.4f){
					if (moveRight){
						transform.Translate(speed * Time.deltaTime,0,0);
					}
					if (moveLeft){
						transform.Translate(speed * -1 * Time.deltaTime,0,0);
					}
				}

			}else{
				if(transform.position.y < initialPositionY){
					transform.Translate(0,speed * 0.8f * Time.deltaTime,0);
				}else{
					touchedPlayer = false;
				}
			}

			Vector3 viewPos = cam.GetComponent<Camera>().WorldToViewportPoint(transform.position);
			if (viewPos.x > 1|| viewPos.x < 0 || viewPos.y > 1 || viewPos.y < 0){
				Destroy(gameObject);
			}
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

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			if (idle){
				anim.SetBool("wakeup", true);
				Invoke("DesactiveIdle", 0.5f);

				if (!attacking){
					InvokeRepeating("AttackNow", 2.5f, 2.0f);
					attacking = true;
				}
			}else{
				touchedPlayer = true;
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

	void DesactiveIdle(){
		idle = false;
	}

	void AttackNow(){
		GameObject goWeapon = (GameObject) Instantiate (weaponPrefab, pointWeapon.position, Quaternion.identity);
		Vector2 targetDirection = (player.transform.position - transform.position).normalized;
		goWeapon.GetComponent<Rigidbody2D>().AddForce(targetDirection * weaponSpeed);
	}
}
