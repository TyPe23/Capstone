using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;

[Serializable]
public class Player {

    public string name;
    /// <summary>
    /// There are currently 5 levels added
    /// </summary>
    public List<PlayerLevel> levels = new List<PlayerLevel>();
    public string ID;

    public Player() {
        name = null;
        ID = null;
        addLevels();
    }

    public void setName(string input) {
        name = input;
    }

    /// <summary>
    /// Adds a new level instance for every level in the game. !!! If levels are added, the for loop needs to be changed !!!
    /// </summary>
    private void addLevels() {
        for (int i = 1; i <= Start.numOfLevels; i++) {
            PlayerLevel newLevel = new PlayerLevel();
            newLevel.level = i;
            levels.Add(newLevel);
        }
    }
}
