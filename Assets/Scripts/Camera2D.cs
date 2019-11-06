using UnityEngine;
using System.Collections;

public class Camera2D : MonoBehaviour {

	private Vector2 velocity;

	public float smoothTimeY = 0.2f;
	public float smoothTimeX = 0.2f;
	public float deltaY = 0.4f;
	
	public GameObject player;
	
	void Start(){
		//player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update(){
		if (player.transform.parent != null)
			Movement();
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
