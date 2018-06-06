using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CooldownTimer {

	public bool finished;
	public float duration;

	float timerStart;
	float timerStop;

	public CooldownTimer(float _duration) {
		duration = _duration;
	}

	public void start () {
		timerStart = Time.time;
		finished = false;
	}

	public void stop() {
		timerStop = Time.time;
		finished = true;
	}

	public void restart() {
		stop ();
		start ();
	}

	public float getElapsedTime() {
		float duration;

		if (!finished)
			duration = Time.time - timerStart;
		else
			duration = timerStop - timerStart;

		return duration;
	}

	public bool isTimeUp() {
		if (getElapsedTime () >= duration) {
			return true;
		}

		return false;
	}
}