using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeController : MonoBehaviour {

	private float startHealth = 4;
	public float health;
	private float maxHealth = 4;
	public float lifePoints = 3;
	private Animator anim;
	private bool isDamageable;
	Vector3 beginPos;
	public Image healthGui;
	public Text lifePointsText;
	AudioSource audio;
	public AudioClip[] audioClip;


	void Start(){
		health = startHealth;
		isDamageable = true;
		anim = GetComponent<Animator>();
		beginPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		audio = GetComponent<AudioSource> ();
		UpdateView ();
	}

	void ApplyDamage(float damage){
		
		if (isDamageable){
			health -= damage;
			PlaySound (0);
			UpdateView ();
			health = Mathf.Max (0, health);
			StartCoroutine(FlashingDamage ());
			anim.SetTrigger("damage");
			
			if (health <= 0) {
				lifePoints--;
				RestartLevel ();
			}

			isDamageable = false;
			Invoke ("ResetIsDamageable", 1.5f);
		} 
	}

	void RestartLevel(){
		health = startHealth;
		UpdateView ();
		transform.position = beginPos;
	}

	void ResetIsDamageable(){
		isDamageable = true;
	}

	IEnumerator FlashingDamage(){
		int i = 0;
		while(i<5){
			GetComponent<Renderer>().enabled = true;
			yield return new WaitForSeconds(0.1f);
			GetComponent<Renderer>().enabled = false;
			yield return new WaitForSeconds(0.1f);
			i++;
		}
		GetComponent<Renderer>().enabled = true;
	}

	void UpdateView(){
		healthGui.fillAmount = health / maxHealth;
		lifePointsText.text = lifePoints.ToString ();

	}

	void PlaySound(int id){
		audio.clip = audioClip [id];
		audio.Play();
	}
}
