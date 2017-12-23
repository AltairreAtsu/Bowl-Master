using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Ball))]
public class BallDragLaunch : MonoBehaviour {

	private Ball ball;

	private float startTime, endTime;
	private Vector3 dragStart, dragEnd;

	[Tooltip ("Amount of Padding to add to the edges of the lane.")]
	public float padding = 13f;

	// Use this for initialization
	void Start () {
		ball = GetComponent<Ball>();
	}

	public void DragStart(){
		if (!ball.inPlay) {
			// Capture Time & Position of Mouse Click
			startTime = Time.time;
			dragStart = Input.mousePosition;
		}
	}

	public void DragEnd(){
		if (!ball.inPlay) {
			// Launch Ball
			endTime = Time.time;
			dragEnd = Input.mousePosition;

			float dragDurration = endTime - startTime;

			float launchSpeedX = (dragEnd.x - dragStart.x) / dragDurration;
			float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDurration;

			launchSpeedX = Mathf.Clamp (launchSpeedX, -500, 500);
			launchSpeedZ = Mathf.Clamp (launchSpeedZ, 0, 1780);

			Vector3 velocity = new Vector3 (launchSpeedX, 0, launchSpeedZ);

			ball.inPlay = true;
			ball.Luanch (velocity);
		}
	}

	public void MoveStart(float xNudge){
		if(!ball.inPlay){
			transform.Translate(new Vector3 (xNudge, 0, 0));

			float newX = (Mathf.Clamp (transform.position.x, -52.5f + padding, 52.5f - padding));
			transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		}
	}

}
