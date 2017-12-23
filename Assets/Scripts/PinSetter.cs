using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSetter : MonoBehaviour {
	[Tooltip ("Bowling Pins Prefab to instantiate.")]
	public GameObject pinsPrefab;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	public void DoAction(ActionMaster.Action action){
		switch (action) {
		case ActionMaster.Action.Tidy:
			animator.SetTrigger ("onTidyTrigger");
			break;
		case ActionMaster.Action.Reset:
			animator.SetTrigger ("onResetTrigger");
			break;
		case ActionMaster.Action.EndTurn:
			animator.SetTrigger ("onResetTrigger");
			break;
		default:
			throw new UnityException ("End game code not in place!");
		}
	}
		
	public void RaisePins(){
		foreach (GameObject pin in GameObject.FindGameObjectsWithTag("Pin")){
			Pin pinScript = pin.GetComponent<Pin> ();
			if (pinScript.IsStanding ()) {
				pinScript.Raise ();
			}
		}
	}

	public void LowerPins(){
		// Lower the Raised Pins
		foreach (GameObject pin in GameObject.FindGameObjectsWithTag("Pin")){
			pin.GetComponent<Pin> ().Lower ();
		}
	}

	public void RenewPins(){
		// Renews Standing Pins
		GameObject pins = Instantiate(pinsPrefab) as GameObject;
		pins.transform.position = new Vector3 (0, 20, 1829);
	}
}
