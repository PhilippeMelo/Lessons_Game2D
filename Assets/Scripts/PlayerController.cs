using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Transform groundCheck;
	public Transform groundCheck2;
	public float speed = 2.4f;
	public float jumpForce = 340;
	public LayerMask whatIsGround;
	private Animator anim;
	public GameObject weaponPrefab;
	public Transform pointWeapon;
	public float weaponSpeed = 380;
	AudioSource audio;
	public AudioClip[] audioClip;

	private float horizontalForceButton = 0;
	private bool isShooting = false;
	private float timerAttacking = 0;

	public static bool knockRight = true;
	public static float knockForce = 0f;

	[HideInInspector]
	public bool lookingRight = true;

	private Rigidbody2D rb2d;
	public bool isGrounded = false;
	private bool jump = false;

	[HideInInspector]
	private bool isAttacking = false;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
	}
	
	void Update () {
		inputCheck ();
		move ();
	}

	void inputCheck (){

		if (Input.GetButtonDown("Jump") && isGrounded){
			jump = true;
		}

		if (Input.GetButtonDown("Fire1") && !isAttacking){
			isAttacking = true;
		}
	}

	void move(){
		
		horizontalForceButton = Input.GetAxis ("Horizontal");
		anim.SetFloat ("speedHorizontal", Mathf.Abs(horizontalForceButton));

		if (knockForce <= 0){
			rb2d.velocity = new Vector2 (horizontalForceButton * speed, rb2d.velocity.y);
		}else{
			if(knockRight){
				rb2d.velocity = new Vector2 (-knockForce * speed, rb2d.velocity.y);
			}else{
				rb2d.velocity = new Vector2 (knockForce * speed, rb2d.velocity.y);
			}
			knockForce -= Time.deltaTime;
		}

		isGrounded = (Physics2D.OverlapCircle (groundCheck.position, 0.15f, whatIsGround)) || (Physics2D.OverlapCircle (groundCheck2.position, 0.15f, whatIsGround));
		anim.SetBool ("grounded", isGrounded);

		if ((horizontalForceButton > 0 && !lookingRight) || (horizontalForceButton < 0 && lookingRight))
			Flip ();

		if (jump) {
			rb2d.AddForce(new Vector2(0, jumpForce));
			jump = false;
		}

		if (isAttacking) {
			timerAttacking = 0.22f;

			GameObject goWeapon = (GameObject) Instantiate (weaponPrefab, pointWeapon.position, Quaternion.identity);

			if (lookingRight){
				goWeapon.GetComponent<Rigidbody2D>().AddForce(Vector3.right * weaponSpeed);
			}else{
				goWeapon.GetComponent<Rigidbody2D>().AddForce(Vector3.left * weaponSpeed);
			}
			isAttacking = false;
		
		}

		if (timerAttacking > 0){
			isShooting = true;
          	timerAttacking -= Time.deltaTime;
        }else{
          	isShooting = false;
          	timerAttacking = 0f;
        }
		anim.SetBool("shotting", isShooting);
	}

	public void Flip(){
		lookingRight = !lookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}

	void PlaySound(int id){
		audio.clip = audioClip [id];
		audio.Play();
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Ground"){
			PlaySound (0);
		}
	}

}
