using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Databasetwo{

	public Databasetwo() {
        //resetPlayerPrefs();
        //makeDummyData();
        //print2DMatrix(helperRetrieveData("level1"));
        //print3DMatrix(retrieveData());
    }

    /// <summary>
    /// adds a player's information to the given level's playerPerf
    /// </summary>
    /// <param name="level"></param>
    /// <param name="name"></param>
    /// <param name="score"></param>
    /// <param name="bonus"></param>
    /// <param name="time"></param>
	public static void setPlayerPref(string level, string name, int score, int bonus, string time) {
        //add bonus points to score
        score += bonus;
        string playerInfo = "";
        //if this is not the first player in the list, add a "|" to delimit the players
        if (PlayerPrefs.GetString(level) != "" && PlayerPrefs.GetString(level) != null) {
            playerInfo += "|";
        }
        //add all values to a string ending in "|"
        playerInfo += (name + "," + score.ToString() + "," + time);

        //add string to player pref for level
        PlayerPrefs.SetString(level, (PlayerPrefs.GetString(level) + playerInfo));
    }

    /// <summary>
    /// returns the given level's playerPerf data in a 2D string matrix
    /// </summary>
    /// <param name="level"></param>
    public static string[][] helperRetrieveData(string level) {
        // split the input string into rows using the delimiter "|"
        string[] rows = PlayerPrefs.GetString(level).Split('|');
        // create the 2D string array
        string[][] matrix = new string[rows.Length][];

        for (int i = 0; i < rows.Length; i++) {
            // split each row into columns using the "," delimiter
            string[] columns = rows[i].Split(',');
            // assign the columns to the current row in the matrix
            matrix[i] = columns;
        }
        return matrix;
    }

    /// <summary>
    /// Turns all level playerPerf info into a 3D string matrix
    /// </summary>
    /// <returns></returns>
    public static string[][][] retrieveData() {
        string[][][] matrix = new string[Start.numOfLevels][][]; 
        //for all 5 levels
        for (int i = 1; i <= Start.numOfLevels; i++) {
            matrix[i-1] = helperRetrieveData("level" + i.ToString());
        }
        return matrix;
    }

    /// <summary>
    /// Creates dummy data for testing and presentation purposes
    /// </summary>
    public static void makeDummyData() {
        //adding players
        Player p1 = new Player();
        Player p2 = new Player();
        Player p3 = new Player();
        Player p4 = new Player();

        p1.setName("Mary");
        p2.setName("Mathew");
        p3.setName("Benjamin");
        p4.setName("Ty");

        Player[] playerList = { p1, p2, p3, p4 };
        var rand = new System.Random();

        //populate level informaiton
        foreach (Player player in playerList) {
            int i = 1;
            foreach (PlayerLevel level in player.levels) {
                level.totalScore = rand.Next(50, 500);
                level.bonusPoints = rand.Next(0, 10);
                level.setTime(rand.Next(10), rand.Next(59), rand.Next(59));
                //Debug.Log("Setting " + player.name + "level" + i.ToString());
                setPlayerPref("level" + i.ToString(), player.name, level.totalScore, level.bonusPoints, level.bestTime.ToString());
                i++;
            }
        }
    }

    public static void resetPlayerPrefs() {
        for (int i = 1; i <= Start.numOfLevels; i++) {
            PlayerPrefs.SetString("level" + i.ToString(), "");
        }
    }

    public static void print3DMatrix(string[][][] matrix) {
        for (int i = 0; i < matrix.GetLength(0); i++) {
            Debug.Log("Level " + (i + 1).ToString());
            for (int j = 0; j < matrix[i].GetLength(0); j++) {
                //Debug.Log(matrix[i][j][0] + " " + matrix[i][j][1] + " " + matrix[i][j][2]);
                for (int k = 0; k < matrix[i][j].GetLength(0); k++) {
                    Debug.Log(matrix[i][j][k]);
                }
            }
        }
    }

    public static void print2DMatrix(string[][] matrix) {
        for (int i = 0; i < matrix.GetLength(0); i++) {
            Debug.Log("Level " + (i + 1).ToString());
            for (int j = 0; j < matrix[i].GetLength(0); j++) {
                Debug.Log(matrix[i][j]);
                //for (int k = 0; k < matrix[i][j].GetLength(0); k++) {
                //    Debug.Log("matrix[" + i + "," + j + "," + k + "] = " + matrix[i][j][k]);
                //}
            }
        }
    }
}
