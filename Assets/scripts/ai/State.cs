using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
	protected EnemyMovement enemyMovement;

	public abstract void Tick();

	public virtual void OnStateEnter() { }
	public virtual void OnStateExit() { }

	public State(EnemyMovement enemyMovement)
	{
		this.enemyMovement = enemyMovement;
	}
}
