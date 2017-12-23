using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	[Tooltip ("Sound Played after ball is released and starts rolling.")]
	public AudioClip rollingSound;
	[Tooltip ("Determines if the Ball is currently rolling and in play.")]
	public bool inPlay = false;

	private AudioSource audioSource;
	private Rigidbody ballRigidbody;
	private Vector3 startPosition;

	// Use this for initialization
	private void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();
		ballRigidbody = gameObject.GetComponent<Rigidbody> ();

		ballRigidbody.useGravity = false;

		startPosition = transform.position;

		//Luanch (launchVelocity);
	}

	public void Luanch (Vector3 launchVelocity) {
		audioSource.clip = rollingSound;
		audioSource.Play ();
		ballRigidbody.useGravity = true;

		ballRigidbody.velocity = launchVelocity;
	}

	public void Reset(){
		ballRigidbody.useGravity = false;
		ballRigidbody.velocity = new Vector3(0, 0, 0);
		ballRigidbody.angularVelocity = new Vector3(0, 0, 0);
		transform.position = startPosition;
		transform.rotation = Quaternion.identity;
		inPlay = false;
	}

}
