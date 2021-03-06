﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeController : MonoBehaviour {

	private float startHealth = 4;
	public float health;
	private float maxHealth = 4;
	public float lifePoints = 3;
	private Animator anim;
	public static bool isDamageable;
	Vector3 beginPos;
	public Image healthGui;
	public Text lifePointsText;
	AudioSource audio;
	public AudioClip[] audioClip;

	private float startLifePoints = 3;
	public Text messageText;
	private bool gameOver;
	private PlayerController playerController;
	public GameObject particlesGameOver;

	void Start(){
		health = startHealth;
		isDamageable = true;
		anim = GetComponent<Animator>();
		beginPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		audio = GetComponent<AudioSource> ();
		UpdateView ();

		if (SceneManager.GetActiveScene().buildIndex == 1){ //Application.loadedLevel == 1
			health = startHealth;
			lifePoints = startLifePoints;
		}else{
			health = PlayerPrefs.GetFloat("Health");
			lifePoints = PlayerPrefs.GetInt("LifePoints");
		}
		messageText.text = " ";
		gameOver = false;
		playerController = GetComponent<PlayerController>();
	}

	void ApplyDamage(float damage){
		
		if (isDamageable && damage > 0){
			PlayerController.knockForce = 0.8f;
			health -= damage;
			PlaySound (0);
			UpdateView ();
			health = Mathf.Max (0, health);
			StartCoroutine(FlashingDamage ());
			anim.SetTrigger("damage");
			
			if (health <= 0) {
				lifePoints--;

				if (!gameOver) {
					if (lifePoints > 0) {
						RestartLevel ();
						//Invoke("RestartLevel", 3.0f);
					} else {

						isGameOver ();
					}
				}
					
			}

			//if (!gameOver){
				isDamageable = false;
				Invoke ("ResetIsDamageable", 1.5f);
			//}
		} 
	}

	void isGameOver(){
		UpdateView();
		gameOver = true;
		gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		messageText.text = "Game Over";
		playerController.enabled = false;
		isDamageable = false;

		//Effect Game Over
		Instantiate (particlesGameOver, gameObject.transform.position, Quaternion.identity);
		gameObject.SetActive(false);

		Invoke ("RestartScene", 5.0f);
	}

	void RestartScene(){
		SceneManager.LoadScene (0);
	}

	void RestartLevel(){
		health = startHealth;
		UpdateView ();
		transform.position = beginPos;

		gameOver = false;
		playerController.enabled = true;
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
