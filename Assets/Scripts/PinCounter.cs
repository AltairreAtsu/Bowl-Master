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

	private void Update(){
		if(gameMaster.getBallOutOfPlay())
			foreach(GameObject pin in GameObject.FindGameObjectsWithTag ("Pin")){
				pin.GetComponent<Rigidbody> ().isKinematic = false;
			}
	}

	public void UpdatingStandingCountAndSettle(){
		// Ball Bowled, Count Pins and detect settle

		int standingPins = CountStanding();
		UpdatePinDisplay (standingPins);

		if (lastStandingCount == -1){
			lastStandingCount = standingPins;
			lastChangeTime = Time.time;
		}

		if(lastStandingCount == standingPins && Time.time - lastChangeTime >= settleTime){
			PinsHaveSettled (standingPins);

		} else if(lastStandingCount != standingPins){
			lastStandingCount = standingPins;
			lastChangeTime = Time.time;
		}
	}

	public int CountStanding(){
		// Count All Standing Pins
		int standing = 0;
		foreach (GameObject pin in GameObject.FindGameObjectsWithTag ("Pin")){
			if (pin.GetComponent<Pin>().IsStanding())
				standing++;
		}

		return standing;
	}

	private void PinsHaveSettled(int standingPins){
		// When pins have settled Reset and update Game Master
		int pinFall = lastSettledCount - standingPins;
		pinUI.color = Color.green;
		lastSettledCount = standingPins;

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

	private void OnTriggerExit (Collider coll) {
		if(coll.gameObject.tag == "Player"){
			gameMaster.SetBallOutOfPlay (true);
		}
	}
		
}
