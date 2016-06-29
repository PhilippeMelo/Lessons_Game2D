using UnityEngine;
using System.Collections;

public class FlashingParticles : MonoBehaviour {

	public int time = 40;
	public float shinningSeconds = 0.05f;

	void Start () {
		StartCoroutine (Flashing ());
	}

	IEnumerator Flashing(){
		int i = 0;
		while (i < time) {
			//GetComponent<ParticleSystem> ().enableEmission = false;
			GetComponent<ParticleSystem> ().GetComponent<Renderer> ().enabled = false;
			yield return new WaitForSeconds (shinningSeconds);
			GetComponent<ParticleSystem> ().GetComponent<Renderer> ().enabled = true;
			//GetComponent<ParticleSystem> ().enableEmission = false;
			yield return new WaitForSeconds (shinningSeconds);

			i++;
		}
		GetComponent<ParticleSystem> ().GetComponent<Renderer> ().enabled = true;
	}

}
