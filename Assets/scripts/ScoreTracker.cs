using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour {

	public ProgressBar bar;

	public bool pause = false;

	public int score = 0;
	public int scoreGoal = 1000;
	public int scoreBase = 5;

	int scoreToAdd = 0;

	private MenuFunctions menuFunctions;
	private ScoreResource[] scoreResources;
	private AudioSource source;

	private bool resourcesCollected = false;

	// Use this for initialization
	void Start () {
		menuFunctions = FindObjectOfType (typeof(MenuFunctions)) as MenuFunctions;
		scoreResources = FindObjectsOfType (typeof(ScoreResource)) as ScoreResource[];
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (score >= scoreGoal) {
			menuFunctions.setBoolTrue ("fadeOutVictory");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			PlayerStatus status = other.GetComponent<PlayerStatus> ();

			if (status.resourceQuantity == 1) {
				scoreToAdd = scoreBase;
			} else if (status.resourceQuantity > 1) {
				scoreToAdd = Mathf.RoundToInt (Mathf.Pow (scoreBase, status.resourceQuantity));
			} else if (status.resourceQuantity == 0) {
					
			}

			score += scoreToAdd;
			status.resourceQuantity = 0;
			source.PlayOneShot (source.clip);

			Debug.Log (score+", "+((float)score / (float)scoreGoal));
			bar.percentProgress = (float)score / (float)scoreGoal;

			//determine if it's still possible for the player to win
			checkIfAllCollected ();
		}
	}

	void checkIfAllCollected() {
		resourcesCollected = true;

		foreach (ScoreResource resource in scoreResources) {
			if (resource.GetComponent<Renderer> ().enabled) {
				resourcesCollected = false;
			}
		}

		if (resourcesCollected) {
			if (score < scoreGoal) {
				menuFunctions.setBoolTrue ("fadeOutFailure");
			}
		}
	}
}
