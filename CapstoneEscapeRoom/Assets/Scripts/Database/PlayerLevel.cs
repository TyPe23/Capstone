using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel {
	public int level;
	public DateTime startTime;
	public DateTime endTime;
	public TimeSpan time;



	public PlayerLevel() { 
	}

	public void getTime() {
		if (startTime == null | endTime == null) {
			return;
		}
		else {
			time = endTime - startTime;
        }
    }
}
