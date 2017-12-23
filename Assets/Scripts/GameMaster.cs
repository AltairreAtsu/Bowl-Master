using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
	private Ball ball;
	private PinCounter pinCounter;
	private PinSetter pinSetter;

	private bool ballOutOfPlay = false;

	// Must be at class level to preserve instance through function calls
	private List<int> pinFalls;

	// Use this for initialization
	private void Start () {
		ball = GameObject.FindGameObjectWithTag ("Player").GetComponent<Ball>();

		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
		pinCounter = GameObject.FindObjectOfType<PinCounter> ();

		pinFalls = new List<int> (21);
	}
	
	// Update is called once per frame
	private void Update () {
		if (ballOutOfPlay) {
			pinCounter.UpdatingStandingCountAndSettle ();
		}
	}

	public void UpdateList(int pinFall){
		pinFalls.Add (pinFall);

		ActionMaster.Action action = ActionMaster.NextAction (pinFalls);
		Debug.Log ("Action: " + action + ", PinFall: " + pinFall);

		if (action == ActionMaster.Action.Reset)
			pinCounter.ResetLastStandingCount ();

		if (action == ActionMaster.Action.EndTurnStrike){
			pinFalls.Add (0);
			action = ActionMaster.Action.EndTurn;
		}

		pinSetter.DoAction (action);
	}

	// Getters and Setters
	public bool GetBallLeftBox(){
		return ballOutOfPlay;
	}

	public void SetBallOutOfPlay(bool ballOutOfPlay){
		this.ballOutOfPlay = ballOutOfPlay;
		if(!ballOutOfPlay){
			ball.Reset ();
		}
	}

}
