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

		// Loop over every value in rolls and calculate the score for that frame
		foreach (int roll in rolls) {



		return frameList;
	}
}
