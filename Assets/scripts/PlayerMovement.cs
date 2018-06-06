using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool pause = false;

	public float moveSpeed = 7.5f;

	Rigidbody rigid;
	Light flashLight;
	Vector3 mousePos, objectPos, flashLightDir;
	Transform flashLightRot;
	float xMove, yMove;
	float angle;

	/*Controls:
	0: Interact/pick up resource
	1: Turn light on/off
	2: Activate smokescreen/distraction
	3: Pause*/
	public KeyCode[] controls = {KeyCode.E, KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Escape};

	// Use this for initialization
	void Awake () {
		rigid = GetComponent<Rigidbody> ();
		flashLight = GetComponentInChildren<Light> ();
		flashLightRot = flashLight.transform.parent;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!pause) {
			//lateral movement
			xMove = Input.GetAxis ("Horizontal");
			yMove = Input.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (xMove, 0, yMove);
            movement = Vector3.ClampMagnitude (movement, 1f);   //so we don't move faster on a diagonal

			rigid.velocity = movement * moveSpeed;

			//rotation
			if (xMove != 0 || yMove != 0) {
				angle = Mathf.Atan2 (yMove, xMove) * Mathf.Rad2Deg;
			}
			transform.rotation = Quaternion.Euler (new Vector3 (0, -angle, 0));

			//toggle flashlight
			/*if (Input.GetKeyDown (controls [1])) {
				if (flashLight.enabled == true) {
					flashLight.enabled = false;
				} else {
					flashLight.enabled = true;
				}
			}*/
		}
	}

	void LateUpdate() {
		mousePos = Input.mousePosition;
		objectPos = Camera.main.WorldToScreenPoint (transform.position);
		flashLightDir = mousePos - objectPos;
		float mouseAngle = Mathf.Atan2 (flashLightDir.y, flashLightDir.x) * Mathf.Rad2Deg;
		flashLightRot.rotation = Quaternion.Euler (new Vector3 (0, -mouseAngle, 0));
	}
}
