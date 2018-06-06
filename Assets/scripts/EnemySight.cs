using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

	public bool pause;
	public float fov = 90f;
	public bool playerInSight = false;
	public Vector3 playerLastPosition;

	[HideInInspector]public Transform player;
	public bool playerEntered = false;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (!pause) {
			if (playerEntered) {
				Vector3 playerDirection = player.position - transform.position;
				float playerAngle = Vector3.Angle (playerDirection, transform.forward);

				//if player is within the cone of vision
				if (playerAngle <= fov * 0.5f) {
					RaycastHit hit;
					//Debug.Log ("I CAN SEE YOU YOU NUMBSKULL");
					//Debug.Log ("Angle: " + playerAngle);
					//Debug.DrawLine (player.position, transform.position, Color.red);

					//if the player is within direct line of sight of the enemy
					if (Physics.Raycast (transform.position, playerDirection, out hit)) {
						if (hit.collider.CompareTag ("Player")) {
							playerInSight = true;

							playerLastPosition = player.transform.position;
						} else {
							playerInSight = false;
						}
					} else {
						playerInSight = false;
					}
				} else if (playerDirection.magnitude <= 1.5f) {
					playerInSight = true;
				} else {
					playerInSight = false;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			//player = other.transform;
			playerEntered = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Player")) {
			//player = null;
			playerEntered = false;
			playerInSight = false;
		}
	}
}
