using UnityEngine;
using System.Collections;

public class DamageCollision : MonoBehaviour {

	public float damage = 1;

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			if (LifeController.isDamageable){
				if (other.transform.position.x < transform.position.x){
					PlayerController.knockRight = true;
				}else{
					PlayerController.knockRight = false;
				}
				other.SendMessage ("ApplyDamage",damage);
			}
		}
	}

	void Update () {
		if(transform.gameObject.tag == "Weapon"){
			Destroy(gameObject, 5f);
		}
	}

}
