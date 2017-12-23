using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {
	// Shred Pins as they leave the Collider
	private void OnTriggerExit(Collider coll){
		if (coll.gameObject.tag == "Pin") {
			Destroy (coll.gameObject);
		}
	}
}
