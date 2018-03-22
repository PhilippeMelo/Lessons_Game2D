using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Child : MonoBehaviour {

	private float damage = 1f;
	public GameObject particlesExplosion;
    private float jumpForce = 200;
    private Rigidbody2D rb2d;
    public float speed = 20f;

	void Start () {
        rb2d = GetComponent<Rigidbody2D> ();
        Invoke("DestroyEnemy", 4f);
	}

    void DestroyEnemy(){
        Instantiate (particlesExplosion, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

	void Update () {
        rb2d.velocity = new Vector2 (speed * Time.deltaTime, rb2d.velocity.y);
        //transform.Translate(speed * 0.8f * Time.deltaTime,0,0);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			DamagePlayer(other);
            DestroyEnemy();
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Ground"){
			rb2d.AddForce(new Vector2(0, jumpForce));
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
		if (damage > 0){
            DestroyEnemy();
		}
	}
}
