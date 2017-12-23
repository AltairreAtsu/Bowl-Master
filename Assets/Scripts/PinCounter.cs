using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour { 
	[Tooltip ("Time without change in Pin standing to trigger Pins Settled.")]
	public float settleTime = 3.0f;

	private float lastChangeTime;
	private int lastSettledCount = 10;
	private int lastStandingCount = -1;

	private GameMaster gameMaster;
	private Text pinUI;

	// Use this for initialization
	void Start () {
		gameMaster = GameObject.FindObjectOfType<GameMaster>();
		pinUI = GameObject.Find ("Pin Count").GetComponent<Text>();
	}

	public void UpdatingStandingCountAndSettle(){
		// Update Last Standing Count
		// Call PinsHaveSettled
		int standingPins = CountStanding();
		UpdatePinDisplay (standingPins);

		if (lastStandingCount == -1){
			lastStandingCount = standingPins;
			lastChangeTime = Time.time;
		}

		if(lastStandingCount == standingPins && Time.time - lastChangeTime >= settleTime){
			PinsHaveSettled ();

		} else if(lastStandingCount != standingPins){
			lastStandingCount = standingPins;
			lastChangeTime = Time.time;
		}
	}

	public int CountStanding(){
		int standing = 0;
		foreach (GameObject pin in GameObject.FindGameObjectsWithTag ("Pin")){
			if (pin.GetComponent<Pin>().IsStanding())
				standing++;
		}

		return standing;
	}

	private void PinsHaveSettled(){
		int standing = CountStanding ();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = standing;
		pinUI.color = Color.green;

		gameMaster.UpdateList(pinFall);

		gameMaster.SetBallOutOfPlay (false);
		lastStandingCount = -1;
	}

	public void ResetLastStandingCount(){
		lastSettledCount = 10;
	}

	private void UpdatePinDisplay(int standingPins){
		pinUI.text = standingPins.ToString ();
		pinUI.color = Color.red;
	}

	void OnTriggerExit (Collider coll) {
		if(coll.gameObject.tag == "Player"){
			gameMaster.SetBallOutOfPlay (true);
		}
	}
		
}
