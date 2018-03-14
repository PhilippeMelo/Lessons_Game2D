using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {

	public float damage = 1;

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Enemy")){
			other.SendMessage ("ApplyDamage",damage);
			damage = 0;
			Destroy(gameObject, 0.03f);
		}
    }

}