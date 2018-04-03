using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour {

	private bool standingUp;
	private bool standingDown;

	private bool attacking;
	public static bool canAttack;

	public bool lookingRight;

	public Transform player;
	public GameObject weaponPrefab;
	public GameObject childrenPrefab;
	public Transform pointWeapon;
	private float weaponSpeed = 160;

	private Animator anim;

	private float health;
	private bool isDamageable;
	private float damage = 1f;

	public GameObject particlesExplosion;

	//Vectors to use in attack
	Vector2 rightUp = (Vector2.right + Vector2.up).normalized;
	Vector2 rightDown = (Vector2.right + Vector2.down).normalized;
	Vector2 leftUp = (Vector2.left + Vector2.up).normalized;
	Vector2 leftDown = (Vector2.left + Vector2.down).normalized;

	void Start () {
		attacking = true;
		canAttack = false;
		standingUp = false;
		standingDown = false;
		lookingRight = false;
		anim = GetComponent<Animator> ();
		health = 3;
		isDamageable = true;
	}

	void StandingUp(){
		transform.gameObject.tag = "Enemy";
		anim.SetBool ("standingUp", true);
		anim.SetBool ("standingDown", false);
		Invoke ("StandingDown", 0.5f);
	}

	void StandingDown(){
		Shoot();
		anim.SetBool ("standingUp", false);
		anim.SetBool ("standingDown", true);
		Invoke ("Idle", 0.3f);
	}

	void Idle(){
		transform.gameObject.tag = "BlockWeapon";
		anim.SetBool ("standingUp", false);
		anim.SetBool ("standingDown", false);
		Invoke ("CanAttack", 1.0f);
	}

	void CanAttack(){
		canAttack = false;
		attacking = true;
	}

	public void Flip(){
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}

	void Shoot(){
		GameObject goWeapon = (GameObject) Instantiate (weaponPrefab, pointWeapon.position, Quaternion.identity);
		GameObject goWeapon2 = (GameObject) Instantiate (weaponPrefab, pointWeapon.position, Quaternion.identity);
		GameObject goWeapon3 = (GameObject) Instantiate (weaponPrefab, pointWeapon.position, Quaternion.identity);

		if (lookingRight){
			goWeapon.GetComponent<Rigidbody2D>().AddForce(Vector2.right * weaponSpeed);
			goWeapon2.GetComponent<Rigidbody2D>().AddForce(rightUp * weaponSpeed);
			goWeapon3.GetComponent<Rigidbody2D>().AddForce(rightDown * weaponSpeed);
		}else{
			goWeapon.GetComponent<Rigidbody2D>().AddForce(Vector2.left * weaponSpeed);
			goWeapon2.GetComponent<Rigidbody2D>().AddForce(leftUp * weaponSpeed);
			goWeapon3.GetComponent<Rigidbody2D>().AddForce(leftDown * weaponSpeed);
		}
	}

	void Update () {

		if ((player.position.x < transform.position.x && lookingRight) || (player.position.x > transform.position.x && !lookingRight))
			Flip ();

		if (player.position.x < transform.position.x){
			lookingRight = false;
		}else{
			lookingRight = true;
		}

		//EnemyArea passa canAttack, então quando ele está atacando, chama StandingUP, mas canAttack = false
		if (canAttack && attacking){
			attacking = false;
			StandingUp();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			DamagePlayer(other);
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

	void ApplyDamage(float damage){

		if (isDamageable && damage > 0){
			StartCoroutine(FlashingDamage ());
			health -= damage;
			
			if (health <= 0) {
				//Effect Explosion
				Instantiate (particlesExplosion, gameObject.transform.position, Quaternion.identity);
				
				CreateChildren();

				Destroy(gameObject);
			}

			isDamageable = false;
			Invoke ("ResetIsDamageable", 1.2f);
		}
	}

	void CreateChildren(){
		Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
		GameObject child1 = (GameObject) Instantiate (childrenPrefab, pos, Quaternion.identity);
		GameObject child2 = (GameObject) Instantiate (childrenPrefab, pos, Quaternion.identity);
		GameObject child3 = (GameObject) Instantiate (childrenPrefab, pos, Quaternion.identity);
		child1.GetComponent<Enemy3Child>().speed = -25;
		child2.GetComponent<Enemy3Child>().speed = 0;
		child3.GetComponent<Enemy3Child>().speed = 25;
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
