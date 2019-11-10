using UnityEngine;
using System.Collections;

public class Camera2D : MonoBehaviour {

	private Vector2 velocity;

	public float smoothTimeY = 0.2f;
	public float smoothTimeX = 0.2f;
	public float deltaY = 0.4f;
	
	public GameObject player;
	public bool fireFade = false;
	
	void Start(){
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
		//player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update(){
		if (player.transform.parent != null)
			Movement();
	}

	public void OpenFade()
	{
		transform.GetChild(0).GetComponent<Animator>().SetBool("fireFade", true);
		Invoke("CloseFade", 1);
	}

	public void CloseFade()
	{
		transform.GetChild(0).GetComponent<Animator>().SetBool("fireFade", false);
	}

	void FixedUpdate() {
		if (player.transform.parent == null)
			Movement();
	}

	void Movement() {
		float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + deltaY, ref velocity.y, smoothTimeY);

		transform.position = new Vector3(posX, posY, transform.position.z);
	}
}
