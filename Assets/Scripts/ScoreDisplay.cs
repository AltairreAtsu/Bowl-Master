using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	public Text[] scoreDisplayTexts, frameScoreDisplayTexts;
	
	public void FillRolls (List<int> rolls){
		// Your Code Here
		string rollString = FormatRolls(rolls);
		for (int i = 0; i < rollString.Length; i++){
			scoreDisplayTexts [i].text = rollString [i].ToString();
		}
	}

	public void FillFrames (List<int> frames){
		for (int i = 0; i < frames.Count; i++){
			frameScoreDisplayTexts [i].text = frames [i].ToString ();
		}
	}

	public static string FormatRolls (List<int> rolls){
		string output = "";
		// Your Code Here
		for (int i = 0; i < rolls.Count; i++){

			if (rolls[i] == 0){	// Always Enter 0 as Dash
				output = output.Insert (output.Length, "-");	
			} else if (rolls[i] == 10){ // Strike
				output = output.Insert (output.Length, "X");
				if( output.Length < 19 )
					output = output.Insert (output.Length, "-");
			} else if ( (i > 0) && !(rolls[i - 1] == 10) && (rolls[i] + rolls[i - 1] == 10) ) { // Spare
				output = output.Insert (output.Length, "/");
			} else { // Normal
				output = output.Insert (output.Length, rolls [i].ToString ());
			}

		}

		return output;
	}
}
