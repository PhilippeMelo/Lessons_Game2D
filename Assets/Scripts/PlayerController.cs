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

	public static bool onLadder = false;
	public static bool usingLadder = false;
	public static bool getOffLadder = false;
	public static bool topLadder = false;
	public static float verticalForceButton = 0;
	public static bool playerAboveLadder = false;
	private bool callFlipOnLadder = true;
	public static float ladderPositionX = 0;
	public static float verticalSpeed = 0;
	public static bool jumpOnLadder = false;
	private float initialGravityScale;
	public float limitFallingSpeed;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		initialGravityScale = rb2d.gravityScale;
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
			if ((usingLadder && Mathf.Abs(verticalForceButton) > 0) == false){
				isAttacking = true;
			}
		}
	}

	void move(){

		verticalSpeed = rb2d.velocity.y;

		if (verticalSpeed < -limitFallingSpeed)
			rb2d.velocity = new Vector2(rb2d.velocity.x, -limitFallingSpeed);

		isGrounded = (Physics2D.OverlapCircle (groundCheck.position, 0.005f, whatIsGround)) || (Physics2D.OverlapCircle (groundCheck2.position, 0.005f, whatIsGround));
		anim.SetBool ("grounded", isGrounded);
		
		horizontalForceButton = Input.GetAxis ("Horizontal");
		anim.SetFloat ("speedHorizontal", Mathf.Abs(horizontalForceButton));

		verticalForceButton = Input.GetAxis ("Vertical");
		anim.SetBool("usingLadder", usingLadder);
		anim.SetBool("topLadder", topLadder);

		if(getOffLadder){
			transform.Translate(0,0.268f,0);
			verticalForceButton = 0;
			getOffLadder = false;
		}

		if (onLadder){
			PlayerOnLadder();
		}else{
			usingLadder = false;
		}

		if ((horizontalForceButton > 0 && !lookingRight) || (horizontalForceButton < 0 && lookingRight)){
			if(verticalForceButton == 0 || !usingLadder){
				Flip ();
			}
		}

		if (knockForce <= 0){
			if (usingLadder){
				PlayerUsingLadder();
			}else{
				rb2d.gravityScale = initialGravityScale;
				rb2d.velocity = new Vector2 (horizontalForceButton * speed, rb2d.velocity.y);
			}
		}else{
			if(knockRight){
				rb2d.velocity = new Vector2 (-knockForce * speed, rb2d.velocity.y);
			}else{
				rb2d.velocity = new Vector2 (knockForce * speed, rb2d.velocity.y);
			}
			knockForce -= Time.deltaTime;
		}

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

	void PlayerOnLadder(){
		if (Input.GetKey(KeyCode.UpArrow) && !playerAboveLadder){
			
			usingLadder = true;
			transform.position = new Vector3(ladderPositionX, transform.position.y, transform.position.z);
			if (isGrounded){
				transform.Translate(0,0.07f,0);
			}
		}
		if (Input.GetKey(KeyCode.DownArrow) && playerAboveLadder){
			transform.Translate(0,-0.318f,0);
			usingLadder = true;
			transform.position = new Vector3(ladderPositionX, transform.position.y, transform.position.z);
		}
	}

	void PlayerUsingLadder(){
		horizontalForceButton = 0;

		if (Input.GetButtonDown("Jump")){
			jumpOnLadder = true;
			Invoke("ResetJumpOnLadder", 0.4f);
			usingLadder = false;
		}

		rb2d.velocity = new Vector2(0,0);
		rb2d.gravityScale = 0;
		transform.Translate(0,verticalForceButton * Time.deltaTime * 1.4f,0);

		if (Mathf.Abs(verticalForceButton) > 0){
			if (callFlipOnLadder){
				Flip();
				callFlipOnLadder = false;
				Invoke("ResetCallFlipOnLadder", 0.25f);
			}
		}
	}

	void ResetJumpOnLadder(){
		jumpOnLadder = false;
	}

	void ResetCallFlipOnLadder(){
		callFlipOnLadder = true;
	}

}
