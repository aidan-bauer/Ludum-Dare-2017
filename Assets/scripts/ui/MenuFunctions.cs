using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour {

	Animator anim;

	public void setBoolTrue(string paramName) {
		anim = GetComponent<Animator> ();
		anim.SetBool (paramName, true);
	}

	public void setBoolFalse(string paramName) {
		anim = GetComponent<Animator> ();
		anim.SetBool (paramName, false);
	}

	public void loadScene(string sceneName) {
		SceneManager.LoadScene (sceneName);
	}

	public void quit() {
		Application.Quit ();
	}
}
