using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	public bool pause;

	public Color[] enemyColors;
	public AudioClip[] clips;

	public Transform[] waypoints;
	public int waypointIndex;
	public float patrolSpeed = 2.0f;
	public float alertSpeed = 3.0f;
	public float hostileSpeed = 5.0f;

	public float searchTime = 30f;
	public float lookTime = 5f;

	[HideInInspector]public NavMeshAgent agent;
	[HideInInspector]public EnemySight sight;
	[HideInInspector]public AudioSource source;
	Material enemyMat;

	private State currentState;

	// Use this for initialization
	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		sight = GetComponent<EnemySight> ();
		source = GetComponent<AudioSource> ();
		enemyMat = GetComponent<Renderer> ().material;

		setState (new PatrolState (this));
	}
	
	// Update is called once per frame
	void Update () {
		if (!pause) {
			currentState.Tick ();
		}
	}

	public void setState(State state) {
		if (currentState != null)
			currentState.OnStateExit ();

		currentState = state;
		transform.name = "Enemy - " + currentState.GetType ().Name;

		if (currentState != null)
			state.OnStateEnter ();
	}

	public void changeColor(Color enemyColor) {
		enemyMat.color = enemyColor;
		enemyColor.a = 0.75f;
		enemyMat.SetColor ("_EmissionColor", enemyColor);
	}
}
