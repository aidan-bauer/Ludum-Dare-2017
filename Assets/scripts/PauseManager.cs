using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

	public bool pauseToggle = false;
	public GameObject pauseCanvas;

	PlayerMovement movement;

	PlayerStatus status;
	ScoreTracker tracker;
	EnemyMovement[] enemyMovements;
	EnemySight[] enemySights;

	// Use this for initialization
	void Awake () {
		movement = FindObjectOfType (typeof(PlayerMovement)) as PlayerMovement;
		status = FindObjectOfType (typeof(PlayerStatus)) as PlayerStatus;
		tracker = FindObjectOfType (typeof(ScoreTracker)) as ScoreTracker;

		enemyMovements = FindObjectsOfType (typeof(EnemyMovement)) as EnemyMovement[];
		enemySights = FindObjectsOfType (typeof(EnemySight)) as EnemySight[];

		//setPause (false);
	}

	void Start() {
		setPause (false);
		setPauseUI (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (movement.controls [3])) {
			//pause or unpause the game
			pauseToggle = !pauseToggle;
			setPause (pauseToggle);
			setPauseUI (pauseToggle);
		}
	}

	public void setPause(bool paused) {
		movement.pause = paused;
		status.pause = paused;
		tracker.pause = paused;

		for (int i = 0; i < enemyMovements.Length; i++) {
			enemyMovements[i].pause = paused;
			enemyMovements [i].agent.isStopped = paused;
			enemySights [i].pause = paused;
		}

		if (paused) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
	}

	public void setPauseUI(bool paused) {
		pauseCanvas.SetActive (paused);
	}
}
