using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

	public bool pause = false;

	public int resourceQuantity = 0;
	int currResourceQuantity = 0;
	public int maxResourceQuantity = 4;
	public float[] distortionAmount = { 0.0f, 0.01f, 0.05f, 0.1f, 0.2f };
	public float[] distortionTint = { 0, 0.1f, 0.2f, 0.35f, 0.55f };
	public float[] volumeLevels = { 0, 0.5f, 0.15f, 0.4f };

	public MeshRenderer distortionRend;
	Color tintColor;

	private AudioSource source;

	// Use this for initialization
	void Awake () {
		source = GetComponent<AudioSource> ();

		//distortColor = distortionRend.material.color;
		tintColor = Color.HSVToRGB (0, distortionTint [0], 0.81f);
		distortionRend.material.SetVector ("_IntensityAndScrolling", 
			new Vector4 (distortionAmount [resourceQuantity], distortionAmount [0], 0.5f, 0.5f));
		distortionRend.material.SetColor ("_Tint", tintColor);
		source.volume = volumeLevels [0];
	}

	void Update() {
		if (!pause) {
			if (currResourceQuantity != resourceQuantity) {
				tintColor = Color.HSVToRGB (0, distortionTint [resourceQuantity], 0.81f);
				distortionRend.material.SetColor ("_Tint", tintColor);
				distortionRend.material.SetVector ("_IntensityAndScrolling", new Vector4 (distortionAmount [resourceQuantity], distortionAmount [resourceQuantity], 0.5f, 0.5f));
				source.volume = volumeLevels [resourceQuantity];
				currResourceQuantity = resourceQuantity;
			}
		}
	}

	/*void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Resource")) {
			Debug.Log ("collected");
			if ((resourceQuantity + 1) <= maxResourceQuantity) {
				resourceQuantity++;
				Destroy (other.gameObject);
			}
		}
	}*/
}
