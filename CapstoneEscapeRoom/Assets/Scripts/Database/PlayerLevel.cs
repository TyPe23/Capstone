using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{

	public int level;
	public TimeSpan bestTime;
	public int bonusPoints;
	public int totalScore;
    bool item1 = false;
    bool item2 = false;
    int bpoints = 250;

    public PlayerLevel() {
		bestTime = new TimeSpan();
		bonusPoints = 0;
		totalScore = 0;
	}

	public void setTime(int x, int y, int z) {
		bestTime = new TimeSpan(x, y, z);
	}



    
    public void i1()
    {
        if (item1 == false)
        {
            updatepoints();
            item1 = true;
        }
    }
    public void i2()
    {
        if (item2 == false)
        {
            updatepoints();
            item2 = true;
        }
    }


    void updatepoints()
    {
        bonusPoints += bpoints;
    }
}