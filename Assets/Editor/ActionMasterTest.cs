using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest {
	private List<int> pinFalls;

	private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
	private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
	private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
	private ActionMaster.Action reset = ActionMaster.Action.Reset;

	private ActionMaster.Action RollBowl(int pinFall){
		pinFalls.Add (pinFall);
		ActionMaster.Action action = ActionMaster.NextAction (pinFalls);
		if (action == ActionMaster.Action.EndTurnStrike){
			pinFalls.Add (0);
			action = ActionMaster.Action.EndTurn;
		}
		return action;
	}

	[SetUp]
	public void SetUp(){
		pinFalls = new List<int> (21);
	}

	[Test]
	public void T00PassingTest () {
		Assert.AreEqual (1, 1);
	}

	[Test]
	public void T01OneStrikeReturnsEndTurn (){
		RollBowl (10);
		Assert.AreEqual (endTurn, ActionMaster.NextAction(pinFalls));
	}

	[Test]
	public void T02Bowl8ReturnsTidy (){
		RollBowl (8);
		Assert.AreEqual (tidy, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T03Bowl82ReturnsReset (){
		RollBowl (8);
		RollBowl (2);
		Assert.AreEqual (endTurn, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T04LastBowlReturnsEndGame (){
		for (int i = 0; i < 9; i++){
			RollBowl (10);
		}

		RollBowl (8); // Frame 10 Bowl 1
		RollBowl (1); // Frame 10 Bowl 2
		Assert.AreEqual (endGame, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T05LastBowlSpare(){
		for (int i = 0; i < 9; i++){
			RollBowl (10);
		}

		RollBowl (8); // Frame 10 Bowl 1
		RollBowl (2); // Frame 10 Bowl 2
		Assert.AreEqual (reset, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T06LastBowlPostSpare(){
		for (int i = 0; i < 9; i++){
			RollBowl (10);
		}

		RollBowl (8); // Frame 10 Bowl 1
		RollBowl (2); // Frame 10 Bowl 2
		RollBowl (2); // Frame 10 Bowl 3
		Assert.AreEqual (endGame, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T07LastFrameStrike(){
		for (int i = 0; i < 9; i++){
			RollBowl (10);
		}

		RollBowl (10);
		Assert.AreEqual(reset, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T08PostStrikeReturnsTidy(){
		for (int i = 0; i < 9; i++){
			RollBowl (10);
		}

		RollBowl (10);
		RollBowl (2);
		Assert.AreEqual (tidy, ActionMaster.NextAction(pinFalls));
	}

	[Test]
	public void T09ResetOnStikeOn20(){
		for (int i = 0; i < 9; i++){
			RollBowl (10);
		}

		RollBowl (10);
		RollBowl (10);
		Assert.AreEqual (reset, ActionMaster.NextAction(pinFalls));
	}

	[Test]
	public void T10PostStrikeBowl0ReturnsTidy(){
		for (int i = 0; i < 9; i++){
			RollBowl (10);
		}

		RollBowl (10);
		RollBowl (0);
		Assert.AreEqual (tidy, ActionMaster.NextAction(pinFalls));
	}

	[Test]
	public void T11EarlierFrameBowl0ThenStrikeGotSpare(){
		RollBowl (0);
		RollBowl (10);
		Assert.AreEqual (2, pinFalls.Count);
	}


	[Test]
	public void T12Dondi10thFrameTurkey () {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1};
		foreach (int roll in rolls) {
			RollBowl (roll);
		}
		RollBowl (10);
		Assert.AreEqual (reset, ActionMaster.NextAction (pinFalls));
		RollBowl (10);
		Assert.AreEqual (reset, ActionMaster.NextAction (pinFalls));
		RollBowl (10);
		Assert.AreEqual (endGame, ActionMaster.NextAction (pinFalls));
	}
}
