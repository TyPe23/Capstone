using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel {
	
	public int level;
	public TimeSpan bestTime;
	public int bonusPoints;
	public int totalScore;

	public PlayerLevel() {
		bestTime = new TimeSpan();
		bonusPoints = 0;
		totalScore = 0;
	}
}
