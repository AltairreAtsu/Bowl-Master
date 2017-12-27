using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreMaster {
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
		List<int> frames = new List<int> ();

		for (int i = 1; i < rolls.Count; i += 2){
			if (frames.Count == 10) { break; }

			// Normal Open Frame
			if (rolls[i - 1] + rolls[i] < 10) {
				frames.Add (rolls [i - 1] + rolls [i]);
			}

			if (rolls.Count - i <= 1) { break; } // Not have look ahead

			if(rolls[i - 1] == 10){ //Strike
				i--; // Strike frame has just one ball
				frames.Add (10 + rolls[i + 1] + rolls[i + 2]);
			} else if (rolls [i - 1] + rolls [i] == 10) { // Spare
				frames.Add (10 + rolls [i + 1]);
			}
		}

		return frames;
	}
}
