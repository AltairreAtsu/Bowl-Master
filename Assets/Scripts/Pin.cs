using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

	[Tooltip ("How much a pin is allowed to rotate before it is considered to have fallen over.")]
	public float  standingThreshold = 10f;
	[Tooltip ("The Distance to raise the pins on the raise animation.")]
	public float distanceToRaise = 40f;

	private Rigidbody rigidBody;

	private void Start(){
		rigidBody = GetComponent<Rigidbody> ();
	}

	public bool IsStanding(){
		float angleX = Mathf.Abs(270 - transform.eulerAngles.x);
		float angleZ = Mathf.Abs(transform.eulerAngles.z);

		if (angleZ > standingThreshold && angleZ < 360 - standingThreshold)
			return false;
		if (angleX > standingThreshold && angleX < 360 - standingThreshold)
			return false;
		
		return true;
	}

	public void Raise(){
		transform.Translate (new Vector3 (0, distanceToRaise, 0), Space.World);
		transform.eulerAngles = new Vector3 (270, 0, 0);
		rigidBody.useGravity = false;
	}

	public void Lower(){
		transform.Translate (new Vector3 (0, distanceToRaise * -1, 0), Space.World);
		rigidBody.useGravity = true;
	}
}
