using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlink : MonoBehaviour {
	private Text text;

	[SerializeField][Tooltip("Time between each button alpha blink.")] 
	private float blinkTime = 1f;
	[SerializeField][Tooltip ("Time the text is visible before blinking away.")]
	private float visibleTime = 0.5f;
	private float time = 0f;

	private void Start (){
		text = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if ( time > blinkTime && !text.enabled ){
			text.enabled = true;
			time = 0f;
		} else if ( time > visibleTime) {
			text.enabled = false;
			time = 0f;
		} 
	}
}
