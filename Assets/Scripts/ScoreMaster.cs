using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

	public enum scoreAction {STRIKE_END_TURN, SPARE_END_TURN, DO_NOTHING};

	// Returns a list of cumulative scores like a score card
	public static List<int> ScoreCumulative (List<int> rolls) {
		List<int> cumulativeScores = new List<int> ();
		int runningTotal = 0;

		foreach (int frameScore in ScoreFrames(rolls)){
			runningTotal += frameScore;
			cumulativeScores.Add (runningTotal);
		}

		return cumulativeScores;
	}

	// Returns a list of individual scores, not cumulative
	public static List<int> ScoreFrames (List<int> rolls){
		List<int> frameList = new List<int> ();

		int frameTotal = 0;
		int index = 1;
		int lastFrameIndex = 0;

		// Loop over every value in rolls and calculate the score for that frame
		foreach (int roll in rolls) {
			frameTotal += roll;

			bool gotSpareThisFrame = false;
			bool gotSpareLastFrame = false;
			bool gotStrikeLastFrame = false;

			bool gotStrikeThisFrame = roll == 10;
			bool endOfFrame = index - lastFrameIndex == 2;

			if (index > 1) {
				gotSpareThisFrame = roll + rolls [index - 2] == 10;
			}
			if (index > 2){
				gotSpareLastFrame = rolls[index - 3] + rolls [index - 2] == 10;
				gotStrikeLastFrame = (rolls [index - 2] == 10) || (rolls [index - 3] == 10) && index - lastFrameIndex == 2;
			}

//			Debug.Log ("Got Spare this Frame: " + gotSpareThisFrame);
//			Debug.Log ("Got Strike this Frame: " + gotStrikeThisFrame);
//			Debug.Log ("Got Strike last Frame: " + gotStrikeLastFrame);
			Debug.Log ("Index " + index + ", Last Frame Index: " + lastFrameIndex);

			if(endOfFrame && !gotStrikeThisFrame && !gotStrikeLastFrame && !gotSpareThisFrame){
				// Didn't get a strike on the last frame or a spare this frame
				frameList.Add (frameTotal);
				//Debug.Log (frameTotal.ToString ());

			} else if ( !endOfFrame && gotSpareLastFrame ) {
				// Got a Spare on the last frame and now calculating score mid frame
				frameList.Add (roll + 10);
				Debug.Log ((roll + 10).ToString ());
			}

			if (gotStrikeThisFrame) {
				lastFrameIndex = index;
				frameTotal = 0;

			} else if (endOfFrame){
				lastFrameIndex = index;
				frameTotal = 0;
			}

			index++;
		}


		return frameList;
	}
}
