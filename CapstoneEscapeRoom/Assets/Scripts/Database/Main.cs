using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization;
//using System.Xml.XmlDocument;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

using UnityEditor;
using UnityEngine.UI;

//[InitializeOnLoad]
public class Main {

    public static string document = "playerData.xml";

    public Main() {
        // open and load the document
        XmlDocument doc = new XmlDocument();
        doc.Load(document);


        // --------------------------- for testing; delete later ------------------------------- //
        ////adding players
        //Player p1 = new Player();
        ////Player p2 = new Player();
        ////Player p3 = new Player();

        //p1.setName("player3");
        //p1.ID = "12354";
        ////p2.setName("player2");
        ////p3.setName("player3");

        //XmlElement playerElm = makePlayerElement(p1, doc);
        ////addPlayerElement(playerElm, doc);
        ////playerElm = makePlayerElement(p2, doc);
        ////addPlayerElement(playerElm, doc);
        ////playerElm = makePlayerElement(p3, doc);
        ////addPlayerElement(playerElm, doc);

        //alterPlayer(playerElm, doc);
    }

    public static XmlElement makePlayerElement(Player player, XmlDocument doc) {
        
        //create all elements
        XmlElement p = doc.CreateElement("Player");
        XmlAttribute name = doc.CreateAttribute("Name");
        XmlElement ID = doc.CreateElement("ID");
        
        //set element values
        name.Value = player.name;
        ID.InnerText = player.ID;

        //set all child elements of the new node
        p.Attributes.Append(name);
        p.AppendChild(ID);

        //create all levels
        foreach (PlayerLevel lev in player.levels) {
            //create a level and assign it the level number
            XmlElement level = doc.CreateElement("Level");
            XmlAttribute levelNumber = doc.CreateAttribute("Number");
            levelNumber.Value = (lev.level).ToString();
            level.Attributes.Append(levelNumber);
            
            //create the sub attributes and give them their values
            XmlElement bestTime = doc.CreateElement("BestTime");
            XmlElement bonusPoints = doc.CreateElement("BonusPoints");
            XmlElement totalScore = doc.CreateElement("TotalScore");
            bestTime.InnerText = lev.bestTime.ToString();
            bonusPoints.InnerText = lev.bonusPoints.ToString();
            totalScore.InnerText = lev.totalScore.ToString();

            level.AppendChild(bestTime);
            level.AppendChild(bonusPoints);
            level.AppendChild(totalScore);

            p.AppendChild(level);
        }

        return p;
    }

    /// <summary>
    /// adds a given player element to the end of a given XmlDocument and saves it
    /// </summary>
    /// <param name="playerElem"></param>
    /// <param name="doc"></param>
    public static void addPlayerElement(XmlElement playerElem, XmlDocument doc) {
        
        //append this node to the root parent node
        doc.DocumentElement.AppendChild(playerElem);
        doc.Save(document);
    }

    /// <summary>
    /// Searches for a node by the username of the old node and replaces the old with the new
    /// </summary>
    /// <param name="newPlayerElement"></param>
    /// <param name="doc"></param>
    public void alterPlayer(XmlElement newPlayerElement, XmlDocument doc) {
        //get name of current element
        string username = newPlayerElement.GetAttribute("Name");
        foreach (XmlElement playerElement in doc.SelectNodes("//Player")) {
            //find element with matching username
            if (playerElement.GetAttribute("Name") == username) {
                //replace old player instance with new player instance
                (doc.DocumentElement).ReplaceChild(newPlayerElement, playerElement);
            }
        }
        doc.Save(document);
    }
}
