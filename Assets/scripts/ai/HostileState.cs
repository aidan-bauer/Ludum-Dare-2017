using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HostileState : State {
	public float range = 2f;

	private CooldownTimer attackTimer;

	private NavMeshAgent agent;
	private EnemySight sight;

	public HostileState(EnemyMovement enemyMovement) : base(enemyMovement) {}

	public override void Tick ()
	{
		//TODO: add code that assigns a new destination if the player moves (aka his position one frame doesn't equal his position next frame
		if (sight.player.position != agent.destination) {
			agent.destination = sight.player.position;
		}

		if (agent.remainingDistance <= range) {
			agent.isStopped = true;
			RaycastHit hit;

			if (attackTimer.finished) {
				if (Physics.Raycast (enemyMovement.transform.position, enemyMovement.transform.forward, out hit, range * 2)) {
					Debug.DrawRay (enemyMovement.transform.position, enemyMovement.transform.forward * range, Color.yellow);

					if (hit.collider.CompareTag ("Player")) {
						hit.collider.GetComponent<PlayerHealth> ().hurt ();
						enemyMovement.source.PlayOneShot (enemyMovement.clips [1]);
						attackTimer.start ();
					}
				}
			}

			if (attackTimer.getElapsedTime () >= attackTimer.duration) {
				attackTimer.stop ();
			}
		} else {
			agent.isStopped = false;
		}

		if (enemyMovement.sight.player.GetComponent<PlayerStatus> ().resourceQuantity == 1) {
			enemyMovement.setState (new AlertState (enemyMovement));
		} else if (enemyMovement.sight.player.GetComponent<PlayerStatus> ().resourceQuantity == 0) {
			enemyMovement.setState (new SearchState (enemyMovement));
		}

		if (!sight.playerInSight) {
			enemyMovement.setState (new SearchState (enemyMovement));
		}
	}

	public override void OnStateEnter ()
	{
		agent = enemyMovement.agent;
		sight = enemyMovement.sight;

		agent.speed = enemyMovement.hostileSpeed;

		agent.destination = sight.player.position;

		attackTimer = new CooldownTimer (1.5f);
		attackTimer.finished = true;

		enemyMovement.changeColor (enemyMovement.enemyColors [2]);
	}

	public override void OnStateExit ()
	{
		agent.isStopped = false;
	}
}
