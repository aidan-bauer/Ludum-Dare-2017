using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchState : State {

	//private CooldownTimer searchTimer = new CooldownTimer (30f);
	//private CooldownTimer lookTimer = new CooldownTimer (5f);

	private CooldownTimer searchTimer;
	private CooldownTimer lookTimer;

	private NavMeshAgent agent;
	private EnemySight sight;

	public SearchState(EnemyMovement enemyMovement) : base(enemyMovement) {}

	public override void Tick ()
	{
		if (lookTimer.isTimeUp ()) {
			wander ();
			/*agent.isStopped = false;
			Vector3 searchPos = enemyMovement.transform.position + 
				(Random.insideUnitSphere * agent.GetComponent<SphereCollider> ().radius);
			searchPos.y = 0.6f;
			agent.destination = searchPos;
			Debug.Log (searchPos+", "+agent.destination+", "+searchTimer.getElapsedTime()+", "+agent.isStopped);

			lookTimer.restart ();*/
		}

		if (agent.remainingDistance <= 1f) {
			agent.isStopped = true;
		}

		if (sight.playerInSight) {
			if (sight.player.GetComponent<PlayerStatus> ().resourceQuantity == 1) {
				enemyMovement.setState (new AlertState (enemyMovement));
			} else if (sight.player.GetComponent<PlayerStatus> ().resourceQuantity >= 2) {
				enemyMovement.setState (new HostileState (enemyMovement));
			}
		}

		if (searchTimer.isTimeUp ()) {
			searchTimer.stop ();
			enemyMovement.setState (new PatrolState (enemyMovement));
		}
	}

	public override void OnStateEnter ()
	{
		agent = enemyMovement.agent;
		sight = enemyMovement.sight;

		searchTimer = new CooldownTimer (enemyMovement.searchTime);
		lookTimer = new CooldownTimer (enemyMovement.lookTime);

		searchTimer.start ();
		lookTimer.start ();
	}

	public override void OnStateExit ()
	{
		searchTimer.stop ();
		lookTimer.stop ();
		//make sure that the agent can move on the way out JUST IN CASE
		agent.isStopped = false;
	}

	void wander() {
		agent.isStopped = false;
		Vector3 searchPos = enemyMovement.transform.position + 
			(Random.insideUnitSphere * agent.GetComponent<SphereCollider> ().radius);
		searchPos.y = 0.6f;
		//agent.destination = searchPos;
		setDestination (searchPos);
		Debug.Log (searchPos+", "+agent.destination+", "+searchTimer.getElapsedTime()+", "+agent.isStopped);

		lookTimer.start ();
	}

	public void setDestination(Vector3 target) {
		Vector3 dir = target - enemyMovement.transform.position;
		float angle = Vector3.Angle (dir, enemyMovement.transform.forward);

		if (Mathf.Abs(angle) > 45) {
			Quaternion.LookRotation (dir);
		}
		agent.destination = target;
	}
}
