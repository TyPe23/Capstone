using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;

[Serializable]
public class Player {
    [XmlElement("name")]
    public string name;

    [XmlElement("level")]
    //public PlayerLevel level;
    public int level;

    public Player() {
        //name = "PlayerName3";
        //level.time = new TimeSpan(2, 4, 6);
    }

    public void setName(string input) {
        name = input;
    }
}
