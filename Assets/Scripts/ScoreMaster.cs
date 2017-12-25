using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

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
		//int runningTotal = 0;
		int index = 1;
		int lastFrameEnd = 0;
		int holdIndex;
		bool gotStrike = false;
		bool gotSpare = false;

		// Loop over every value in rolls and calculate the score for that frame
		foreach (int roll in rolls) {
			bool endOfFrame;

			if(roll == 10){
				endOfFrame = true;
				holdIndex = index;
				gotStrike = true;
				lastFrameEnd = index;

			} else if ( (index > 1) && (roll + rolls[index - 2] == 10) && (index - lastFrameEnd == 2) ) {
				endOfFrame = true;
				holdIndex = index;
				gotSpare = true;
				lastFrameEnd = index;

			} else if ( index - lastFrameEnd == 2) {
				endOfFrame = true;
				lastFrameEnd = index;
			} else {
				endOfFrame = false;
			}

			if (endOfFrame && !gotStrike && !gotSpare) {
				int frameScore = roll + rolls [index - 2];
				frameList.Add (frameScore);
				Debug.Log ("End Of Frame, Added: " + frameScore);
			}

			index++;
		}


		return frameList;
	}
}
