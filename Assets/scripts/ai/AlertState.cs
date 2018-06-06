using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertState : State {
	
	public float stopDistance = 3f;

	private NavMeshAgent agent;
	private EnemySight sight;

	public AlertState(EnemyMovement enemyMovement) : base(enemyMovement) {}

	public override void Tick () {
		//TODO: add code that assigns a new destination if the player moves (aka his position one frame doesn't equal his position next frame
		if (sight.player.position != agent.destination) {
			agent.destination = sight.player.position;
		}

        //bool stop = (agent.remainingDistance <= stopDistance) ? true : false;
        bool stop;
        if (agent.remainingDistance <= stopDistance)
        {
            stop = true;
        }
        else {
            stop = false;
        }

        if (stop == false)
            Debug.Log("investigating, "+agent.remainingDistance);
		agent.isStopped = stop;

		if (enemyMovement.sight.player.GetComponent<PlayerStatus> ().resourceQuantity >= 2) {
			enemyMovement.setState (new HostileState (enemyMovement));
		}

		if (!sight.playerInSight) {
			enemyMovement.setState (new SearchState (enemyMovement));
		}
	}

	public override void OnStateEnter () {
		agent = enemyMovement.agent;
		agent.speed = enemyMovement.alertSpeed;

		sight = enemyMovement.sight;
		agent.destination = sight.player.position;

		enemyMovement.changeColor (enemyMovement.enemyColors [1]);
		enemyMovement.source.PlayOneShot (enemyMovement.clips [0]);
	}

	public override void OnStateExit ()
	{	
		agent.isStopped = false;
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
