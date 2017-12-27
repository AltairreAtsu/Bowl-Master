using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]
public class ScoreDisplayTest : MonoBehaviour {

	[Test]
	public void T00PassingTest () {
		Assert.AreEqual (1, 1);
	}

	[Test]
	public void T01Bowl1 () {
		int[] rolls = { 1 };
		string rollString = "1";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	[Test]
	public void T02Bowl12 () {
		int[] rolls = { 1, 2 };
		string rollString = "12";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	[Test]
	public void T03Bowl125 () {
		int[] rolls = { 1, 2, 5 };
		string rollString = "125";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	[Test]
	public void T04Bowl10 () {
		int[] rolls = { 10 };
		string rollString = "X-";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	[Test]
	public void T05Bowl102 () {
		int[] rolls = { 10, 2 };
		string rollString = "X-2";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	[Test]
	public void T06Bowl2105 () {
		int[] rolls = { 2, 10, 5 };
		string rollString = "2X-5";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	[Test]
	public void T07Bowl82 () {
		int[] rolls = { 8, 2};
		string rollString = "8/";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	[Test]
	public void T08Bowl100 () {
		int[] rolls = { 10, 0};
		string rollString = "X--";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	[Test]
	public void T09Bowl2100 () {
		int[] rolls = { 2, 10, 0};
		string rollString = "2X--";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	[Test]
	public void T10AllStrikes () {
		int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10 , 10, 10, 10};
		string rollString = "X-X-X-X-X-X-X-X-X-XXX";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	[Test]
	public void T11AllOnes () {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
		string rollString = "11111111111111111111";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	[Test]
	public void T12SpareOnLast () {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 8, 2, 1};
		string rollString = "1111111111111111118/1";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}
}
