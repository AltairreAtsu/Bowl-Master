using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster {

	public enum Action {Tidy, Reset, EndTurn, EndGame, EndTurnStrike};

	public static Action NextAction(List<int> pinFalls){
		Debug.Log (pinFalls.Count);
		int pins = pinFalls [pinFalls.Count - 1];
		int bowl = pinFalls.Count;

		if (pins < 0 || pins > 10) {throw new UnityException ("Pins must not be less than 0 or greater than 10!");}

		bool gotSpare = (bowl > 1) && (bowl % 2 == 0) && (pins + pinFalls [bowl - 2] == 10);

		// Last Frame Handling
		if (bowl == 21){
			// Absolute End
			//UpdateArrayAndBowlIndex (pins);
			return Action.EndGame;
		}

		if( bowl == 19 && pins == 10){
			// Frame 10 Bowl 19, got Strike
			//UpdateArrayAndBowlIndex (pins, 1);
			return Action.Reset;
		}

		if (bowl == 20) {
			gotSpare = (pins != 0) && (pins + pinFalls [bowl - 2] == 10);
			bool gotStrike = pinFalls [bowl - 2] == 10;

			if (gotSpare || pins == 10){
				// Last Bowl and Got Spare or Strike
				//UpdateArrayAndBowlIndex (pins, 1);
				return Action.Reset;

			} else if(gotStrike){
				// Bowl 20, Bowl 21 Awarded, No Strike
				//UpdateArrayAndBowlIndex (pins, 1);
				return Action.Tidy;

			} else {
				// Bowl 20 no Spare or Strike
				//UpdateArrayAndBowlIndex (pins);
				return Action.EndGame;
			} 
		}

		// Other Frame Handling
		if(pins == 10 && !gotSpare){
			// Strike not on Bowl 19
			return Action.EndTurnStrike;

		}
			
		if (bowl % 2 != 0){
			// Mid Frame or Last Frame	
			//UpdateArrayAndBowlIndex (pins, 1);
			return Action.Tidy;
		} else {
			// End of Frame	
			//UpdateArrayAndBowlIndex (pins, 1);
			return Action.EndTurn;
		}

		throw new UnityException ("Not sure what action to return!");
	}

//	private void UpdateArrayAndBowlIndex(int pins, int incriment){
//		pinFalls [bowl - 1] = pins;
//		//bowl += incriment;
//	}
//
//	private void UpdateArrayAndBowlIndex(int pins){
//		pinFalls [bowl - 1] = pins;
//	}
//
//	// Getter
//	public int GetBowl(){
//		return bowl;
//	}
}
