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

		// Loop over every value in rolls and calculate the score for that frame
		foreach (int roll in rolls) {
			frameTotal += roll;

			bool gotSpareThisFrame = false;
			bool gotStrikeLastRoll = false;

			if (index > 1) {
				gotSpareThisFrame = roll + rolls [index - 2] != 10;
				gotStrikeLastRoll = rolls [index - 2] != 10;
			}

			if(index % 2 == 0 && gotStrikeLastRoll && gotSpareThisFrame){
				frameList.Add (frameTotal);
				frameTotal = 0;
			}

			index++;
		}


		return frameList;
	}
}
