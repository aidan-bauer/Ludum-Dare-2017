using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	public RectTransform bar;
	[Range(0f, 1f)]
	public float percentProgress = 0f;

	private Vector3 scale;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scale = new Vector3 (percentProgress, 1, 1);
		bar.localScale = scale;
	}
}
