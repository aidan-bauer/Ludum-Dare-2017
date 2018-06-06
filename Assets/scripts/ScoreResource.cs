using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreResource : MonoBehaviour {

	public IEnumerator ParticleFade() {
		foreach (Collider col in GetComponents<Collider>()) {
			col.enabled = false;
		}
		GetComponent<Renderer> ().enabled = false;

		//TODO: destroy object only when all particles from it's particle system have been destroyed
		ParticleSystem particles = GetComponentInChildren<ParticleSystem> ();
		particles.Stop ();

		while (true) {
			if (particles.particleCount == 0) {
				Destroy (this.gameObject);
			}
			//Debug.Log (particles.particleCount);
			yield return new WaitForSeconds (Time.deltaTime);
		}
		//yield return 0;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			PlayerStatus status = other.GetComponent<PlayerStatus> ();
			//Debug.Log ("collected");
			if ((status.resourceQuantity + 1) <= status.maxResourceQuantity) {
				status.resourceQuantity++;

				foreach (Collider col in GetComponents<Collider>()) {
					col.enabled = false;
				}
				GetComponent<Renderer> ().enabled = false;

				//TODO: destroy object only when all particles from it's particle system have been destroyed
				ParticleSystem particles = GetComponentInChildren<ParticleSystem> ();
				particles.Stop ();
				GetComponent<AudioSource> ().PlayOneShot (GetComponent<AudioSource> ().clip);
			}
		}
	}
}
