using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour {

	public Vector3 lockRot = new Vector3(-90, 0, 0);
	Quaternion lockRotQuat;

	// Use this for initialization
	void Awake () {
		lockRotQuat = Quaternion.Euler (lockRot);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = lockRotQuat;
	}
}
