using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
	private Ball ball;
	private PinCounter pinCounter;
	private PinSetter pinSetter;
	private ScoreDisplay scoreDisplay;
	private LevelManager levelManager;

	private bool ballOutOfPlay = false;


	// Must be at class level to preserve instance through function calls
	private List<int> rolls;

	// Use this for initialization
	private void Start () {
		ball = GameObject.FindGameObjectWithTag ("Player").GetComponent<Ball>();

		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
		pinCounter = GameObject.FindObjectOfType<PinCounter> ();

		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay> ();
		levelManager = GameObject.FindObjectOfType<LevelManager> ();

		rolls = new List<int> (21);
	}
	
	// Update is called once per frame
	private void Update () {
		if (ballOutOfPlay) {
			pinCounter.UpdatingStandingCountAndSettle ();
		}
	}

	public void UpdateList(int pinFall){
		rolls.Add (pinFall);

		ActionMaster.Action action = ActionMaster.NextAction (rolls);
		Debug.Log ("Action: " + action + ", PinFall: " + pinFall);

		if (action == ActionMaster.Action.EndGame){
			levelManager.LoadNextLevel ();
			return;
		}

		if (action == ActionMaster.Action.Reset || action == ActionMaster.Action.EndTurn)
			pinCounter.ResetLastStandingCount ();

		if (action == ActionMaster.Action.EndTurnStrike){
			rolls.Add (0);
			action = ActionMaster.Action.EndTurn;
		}

		pinSetter.DoAction (action);
	}

	public bool getBallOutOfPlay(){
		return ballOutOfPlay;
	}

	public void SetBallOutOfPlay(bool ballOutOfPlay){
		this.ballOutOfPlay = ballOutOfPlay;
		if(!ballOutOfPlay){
			ball.Reset ();
			scoreDisplay.FillRolls (rolls);
			scoreDisplay.FillFrames (ScoreMaster.ScoreCumulative(rolls));
		}
	}

}
