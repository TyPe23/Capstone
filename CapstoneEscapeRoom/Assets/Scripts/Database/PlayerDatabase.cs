//Original Author: Mary Nations
//Additional Authors: 
//Last edited by: Mary Nations
//On: 19 March 2023
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Creates a database for the scoreboards using PlayerPrefs
/// </summary>
public class PlayerDatabase : MonoBehaviour {

	public PlayerDatabase() {
        //resetPlayerPrefs();
        //makeDummyData();
        //print2DMatrix(helperRetrieveData("level1"));
        //print3DMatrix(retrieveData());
    }

    /// <summary>
    /// adds a player's information to the given level's playerPerf
    /// </summary>
	public static void addPlayerInfo() {
        //caclulate score
        calculateFinalScore();
        string score = (PlayerPrefs.GetInt("Score")).ToString();
        string playerInfo = "";
        //grab current level
        string level = "Level" + PlayerPrefs.GetInt("Level").ToString();
        //if this is not the first player in the list, add a "|" to delimit the players
        if (PlayerPrefs.GetString(level) != "" && PlayerPrefs.GetString(level) != null) {
            playerInfo += "|";
        }
        //add all values to a string
        playerInfo += (PlayerPrefs.GetString("Name") + "," + score.ToString() + "," + PlayerPrefs.GetString("Time"));

        //add string to player pref for level
        PlayerPrefs.SetString(level, (PlayerPrefs.GetString(level) + playerInfo));
    }

    /// <summary>
    /// calculates the player's final score by the amount of time taken on a level and the bonus points
    /// </summary>
    public static void calculateFinalScore() {
        int baseScore = 600;
        //get time taken in seconds
        string timeString = PlayerPrefs.GetString("Time");
        string[] times = timeString.Split(':');
        TimeSpan timeSpan = new TimeSpan(Int32.Parse(times[0]), Int32.Parse(times[1]), Int32.Parse(times[2]));
        int seconds = (int)timeSpan.TotalSeconds;
        //get 15 minutes - the time taken to finish the level, but don't let this drop below 0
        int timeBonus = 0;
        if (900 -seconds > 0) {
            timeBonus = 900 - seconds;
        }
        //caclulate score
        PlayerPrefs.SetInt("Score", baseScore + timeBonus + PlayerPrefs.GetInt("BonusPoints") - PlayerPrefs.GetInt("Hints"));
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
    /// Turns all level playerPrefs info into a 3D string matrix
    /// </summary>
    /// <returns></returns>
    public static string[][][] retrieveData() {
        //make dummy data for display purposes. Can delete before actual game play
        //if (PlayerPrefs.GetString("Level1") == null || PlayerPrefs.GetString("Level1") == "") {
        //    makeDummyData();
        //}

        string[][][] matrix = new string[Start.numOfLevels][][]; 
        //for all 5 levels
        for (int i = 1; i <= Start.numOfLevels; i++) {
            matrix[i-1] = helperRetrieveData("Level" + i.ToString());
        }
        return matrix;
    }

    /// <summary>
    /// resets all set playerPrefs for the individual at the beginning of a level
    /// </summary>
    public static void resetPlayerPrefs() {
        //reset level informaiton
        PlayerPrefs.SetString("Time", "00:00:00");
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetString("Name", "");
        PlayerPrefs.SetInt("BonusPoints", 0);
        PlayerPrefs.SetInt("Hints", 0);
        //The playerPref "Level" is not reset here b/c we need to
        //know which level the player is entering
    }

    /// <summary>
    /// sets the level the player selected
    /// </summary>
    /// <param name="level"></param>
    public static void setLevel(int level) {
        PlayerPrefs.SetInt("Level", level);
    }

    /// <summary>
    /// sets the player's name
    /// </summary>
    /// <param name="name"></param>
    public static void setName(string name) {
        PlayerPrefs.SetString("Name", name);
    }

    /// <summary>
    /// clears all level playerPrefs, which are used to store previous player informaiton
    /// </summary>
    public static void clearDatabase() {
        for (int i = 1; i <= Start.numOfLevels; i++) {
            PlayerPrefs.SetString("Level" + i.ToString(), "");
        }
        PlayerPrefs.SetString("Teams", "");
    }

    /// <summary>
    /// uses the currently set player Name and makes a key,value pair with the team name and teamID
    /// </summary>
    /// <param name="teamID"></param>
    public static void addTeam(string teamID) {
        string teamInfo = "";
        //if this is not the first team in the list, add a "|" to delimit the teams
        if (PlayerPrefs.GetString("Teams") != "" && PlayerPrefs.GetString("Teams") != null) {
            teamInfo += "|";
        }
        //add all values to a string
        teamInfo += (teamID + "," + PlayerPrefs.GetString("Name"));

        PlayerPrefs.SetString("Teams", (PlayerPrefs.GetString("Teams") + teamInfo));
        Debug.Log(PlayerPrefs.GetString("Teams"));
    }

    /// <summary>
    /// returns the team name associated with the given team ID
    /// </summary>
    /// <param name="teamID"></param>
    /// <returns></returns>
    public static string retrieveTeamName(string teamID) {
        // split the input string into key,value pairs using the delimiter "|"
        string[] rows = PlayerPrefs.GetString("Teams").Split('|');
        // create a 2D string array
        string[][] matrix = new string[rows.Length][];

        for (int i = 0; i < rows.Length; i++) {
            // split each row into columns using the "," delimiter
            string[] columns = rows[i].Split(',');
            // assign the columns to the current row in the matrix
            matrix[i] = columns;
        }
        //find the given ID and its coordinating name
        string teamName = "";
        for (int i = 0; i < matrix.GetLength(0); i++) {
            if (matrix[i][0] == teamID) {
                teamName = matrix[i][1];
            }
        }
        return (teamName);
    }

    /// <summary>
    /// Creates dummy data for testing and presentation purposes
    /// </summary>
    public static void makeDummyData() {
        //adding players
        string[] playerList = { "Mary", "Mathew", "Benjamin", "Ty" };
        var rand = new System.Random();

        //populate level informaiton
        foreach (string player in playerList) {
            PlayerPrefs.SetString("Name", player);
            for (int i = 1; i <= Start.numOfLevels; i++) {
                PlayerPrefs.SetInt("Level", i);
                PlayerPrefs.SetInt("Score", rand.Next(50, 500));
                PlayerPrefs.SetInt("BonusPoints", rand.Next(0, 10));
                string time = (rand.Next(10).ToString("D2") + ":" + rand.Next(59).ToString("D2") + ":" + rand.Next(59).ToString("D2"));
                PlayerPrefs.SetString("Time", time);
                addPlayerInfo();
            }
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
