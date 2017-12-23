using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Ball ball;

	[Tooltip ("Where on the Z Axis the Camera should stop following.")]
	public float stopPosition;

	private Vector3 offset;

	// Use this for initialization
	private void Start () {
		offset = ball.transform.position - transform.position;
	}
	
	// Update is called once per frame
	private void Update () {
		if(transform.position.z < stopPosition || !ball.inPlay)
			transform.position = ball.transform.position - offset;
	}
}
