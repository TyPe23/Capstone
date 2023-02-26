//Original Author: Mary Nations
//Last updated 02/24/2023 by Mary Nations

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
//using System.Linq;

//[InitializeOnLoad]
public class Database : MonoBehaviour {

    public static string document = "playerData.xml";
    public XmlDocument doc = new XmlDocument();

    public Database() {

        

        //check if the document is created or not
        try {
            // open and load the document
            doc.Load(document);
        }
        //make a document if it does not exist
        catch {
            using (XmlWriter writer = XmlWriter.Create(document)) {
                writer.WriteStartElement("PlayerList");
                writer.WriteEndElement();
                writer.Flush();
            }
            // open and load the document
            doc.Load(document);
        }

        //tempgetplayers();
        //getPlayers();

        // --------------------------- for testing; delete later ------------------------------- //
        ////adding players
        //Player p1 = new Player();
        //Player p2 = new Player();
        //Player p3 = new Player();

        //p1.setName("player1");
        //p1.ID = "12345";
        //p2.setName("player2");
        //p3.setName("player3");

        //XmlElement playerElm = makePlayerElement(p1, doc);
        //addPlayerElement(playerElm, doc);
        //playerElm = makePlayerElement(p2, doc);
        //addPlayerElement(playerElm, doc);
        //playerElm = makePlayerElement(p3, doc);
        //addPlayerElement(playerElm, doc);

        ////alterPlayer(playerElm, doc);
    }

    /// <summary>
    /// takes in a player object and turns it into an XMl element
    /// </summary>
    /// <param name="player"></param>
    /// <param name="doc"></param>
    /// <returns>XmlElement</returns>
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
        //check if the player already exists
        if (playerExists(playerElem, doc)) {
            //if so, reroute to alterPlayer instead of re-adding player
            alterPlayer(playerElem, doc);
        }
        else {
            //append this node to the root parent node
            doc.DocumentElement.AppendChild(playerElem);
            doc.Save(document);
        }
    }

    /// <summary>
    /// Searches for a node by the username of the old node and replaces the old with the new
    /// </summary>
    /// <param name="newPlayerElement"></param>
    /// <param name="doc"></param>
    public static void alterPlayer(XmlElement newPlayerElement, XmlDocument doc) {
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

    /// <summary>
    /// Checks if a player in a document already exists. 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="doc"></param>
    /// <returns>bool</returns>
    public static bool playerExists(XmlElement player, XmlDocument doc) {
        string username = player.GetAttribute("Name");
        foreach (XmlElement playerElement in doc.SelectNodes("//Player")) {
            //find element with matching username
            if (playerElement.GetAttribute("Name") == username) {
                //found
                return true;
            }
        }
        //never found
        return false;
    }

    /// <summary>
    /// Pulls all player information from the XML doc and formats it as a 3D matrix
    /// </summary>
    /// <returns>string[,,]</returns>
    public string[,,] getPlayers() {
        //format [level[player[name, score, time]]]
        //ex:   playerList[0] would return all results of level 1
        //      playerList[0,1] returns the second player's pair [name, score, time]
        //      playerList[0,1,1] returns level 1, player 2, score
        //      playerList[0,1,0] returns level 1, player 2, name
        //      playerList[0,1,2] returns level 1, player 2, time

        string[,,] playerList = new string[,,] {
            {
                {"player1", "25", "(05:06:07)"},
                {"player2", "30", "(06:07:08)"}
            },
            {
                { "player1", "42", "(10:20:30)"},
                { "player2", "39", "(12:22:32)"}
            }
        };

        //Debug.Log(playerList[0, 1, 0]);

        return playerList;
    }

    public string[,,] tempgetplayers() {
        //open the document to be read
        //XmlDocument doc = new XmlDocument();
        //doc.LoadXml(document);

        //variables
        string name = "";
        int playerIndex = 0;
        int levelIndex = 0;
        int playerNameIndex = 0;
        int scoreIndex = 1;
        int timeIndex = 2;

        //create a matrix
        List<List<List<string>>> playerList = new List<List<List<string>>>();

        //Populate levels first to match how the matrix is read
        XmlElement docElem = doc.DocumentElement;
        XmlElement firstPlayer = (XmlElement)docElem.FirstChild;
        XmlNodeList levels = firstPlayer.ChildNodes;
        for (int i = 0; i < levels.Count; i++) {
            XmlElement child = (XmlElement)levels[i];
            if (child.GetAttribute("Number") != "") {
                //add a list of string-lists for every level
                playerList.Add(new List<List<string>>());
                levelIndex++;
                Debug.Log("level = " + child.GetAttribute("Number"));
            }
        }


        return getPlayers();
    }
}
