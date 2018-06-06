using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State {

	public Transform[] waypoints;
	private int waypointIndex = 0;
	public float patrolDistance = 0.5f;

	private NavMeshAgent agent;
	private EnemySight sight;

	public PatrolState(EnemyMovement enemyMovement) : base(enemyMovement) {}

	public override void Tick () {
		//move on once we reach a destinations
		if (agent.remainingDistance < patrolDistance) {
			//change destination
			setDestination (waypoints[waypointIndex]);
			waypointIndex = (waypointIndex + 1) % waypoints.Length;
		}

		//under what conditions we transfer out of the patrol state
		if (sight.playerInSight) {
			if (sight.player.GetComponent<PlayerStatus> ().resourceQuantity == 1) {
				enemyMovement.setState (new AlertState (enemyMovement));
			} else if (sight.player.GetComponent<PlayerStatus> ().resourceQuantity >= 2) {
				enemyMovement.setState (new HostileState (enemyMovement));
			}
		}
	}

	public override void OnStateEnter ()
	{
		sight = enemyMovement.GetComponent<EnemySight> ();
		agent = enemyMovement.agent;
		agent.speed = enemyMovement.patrolSpeed;
		waypoints = enemyMovement.waypoints;

		setDestination (waypoints[waypointIndex]);

		enemyMovement.changeColor (enemyMovement.enemyColors [0]);
	}

	public void setDestination(Transform target) {
		Vector3 dir = target.position - enemyMovement.transform.position;
		float angle = Vector3.Angle (dir, enemyMovement.transform.forward);

		if (Mathf.Abs(angle) > 45) {
			Quaternion.LookRotation (dir);
		}
		agent.destination = target.position;
	}
}
