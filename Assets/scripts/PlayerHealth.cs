using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public bool pause = false;
	public bool dead = false;

	public int currHP = 0;
	public int maxHP = 2;

	private MenuFunctions menuFunctions;
	private MeshRenderer rend;
	private Color defaultColor;
	private Color defaultTint;

	// Use this for initialization
	void Awake () {
		menuFunctions = FindObjectOfType (typeof(MenuFunctions)) as MenuFunctions;
		rend = GetComponent<MeshRenderer> ();
		defaultColor = rend.material.color;
		defaultTint = rend.material.GetColor ("_EmissionColor");

		currHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		if (currHP <= 0) {
			dead = true;
			rend.material.color = Color.black;
			rend.material.SetColor ("_EmissionColor", Color.black);
			menuFunctions.setBoolTrue ("fadeOutFailure");
		}
	}

	public void hurt() {
		currHP--;
		StartCoroutine (HurtFlicker ());
	}

	IEnumerator HurtFlicker() {
		rend.material.color = Color.red;
		rend.material.SetColor ("_EmissionColor", Color.red);
		yield return new WaitForSeconds (0.5f);
		rend.material.color = defaultColor;
		rend.material.SetColor ("_EmissionColor", defaultTint);
	}
}
