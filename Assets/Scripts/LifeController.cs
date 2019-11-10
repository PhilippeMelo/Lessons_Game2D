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
	public static bool isDamageable;
	Vector3 beginPos;
	public Image healthGui;
	public Text lifePointsText;
	AudioSource audio;
	public AudioClip[] audioClip;

	private float startLifePoints = 4;
	public Text messageText;
	private bool gameOver;
	private PlayerController playerController;
	public GameObject particlesGameOver;
	private bool canKill = true;

	private bool isDead = false;

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

	void KillPlayer() {
		if (canKill) {
			canKill = false;
			isDamageable = true;
			ApplyDamage(100);
			Invoke("ResetCanKill", 0.1f);
		}
	}

	void ResetCanKill()
	{
		canKill = true;
	}

	void ApplyDamage(float damage){
		
		if (isDamageable && damage > 0){
			isDamageable = false;
			Invoke ("ResetIsDamageable", 1.5f);

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
						GameObject.Find("Camera").SendMessage("OpenFade");
						isDead = true;
						FreezeGame();
						Invoke("RestartLevel", 0.2f);
					} else {

						isGameOver ();
					}
				}
			}
			//if (!gameOver){
			//}
		} 
	}

	public void Charge(int value)
	{
		if (health < 4) health += value;
		if (health  > 4) health = 4;
		UpdateView();
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
		UnFreezeGame();
		isDead = false;
		//canKill = true;
	}

	void FreezeGame()
	{
		GetComponent<Renderer>().enabled = false;
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		playerController.enabled = false;
		PlayerController.knockForce = 0;
	}

	void UnFreezeGame()
	{
		GetComponent<Renderer>().enabled = true;
		GetComponent<Rigidbody2D>().constraints = playerController.initialConstraints;
		playerController.enabled = true;
		PlayerController.knockForce = 0;
	}

	void ResetIsDamageable(){
		isDamageable = true;
	}

	IEnumerator FlashingDamage()
	{
		if (isDead)
		{
			GetComponent<Renderer>().enabled = false;
		} else {
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
	}

	void UpdateView(){
		healthGui.fillAmount = health / maxHealth;
		lifePointsText.text = lifePoints.ToString ();

	}

	void PlaySound(int id){
		audio.clip = audioClip [id];
		audio.Play();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("CheckPoint"))
		{
			beginPos = new Vector3(other.transform.position.x, other.transform.position.y, 0);
		}
	}
}
