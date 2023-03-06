using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
//using System.Runtime.Serialization;
//using System.Xml.XmlDocument;
//using System.Xml.Serialization;
using System.Xml;
//using System.IO;



//[InitializeOnLoad]
public class Database : MonoBehaviour {

    public static string document = "playerData.xml";
    public static XmlDocument doc = new XmlDocument();

    public Database() {

    }

    public static void startDB() {
        //XmlDocument doc = new XmlDocument();

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
            //to be deleted for production ------------------------------------------------
            makeDummyData();
        }
    }

    /// <summary>
    /// takes in a player object and turns it into an XMl element
    /// </summary>
    /// <param name="player"></param>
    /// <returns>XmlElement</returns>
    public static XmlElement makePlayerElement(Player player) {
        
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
    public static void addPlayerElement(XmlElement playerElem) {
        //check if the player already exists
        if (playerExists(playerElem)) {
            //if so, reroute to alterPlayer instead of re-adding player
            alterPlayer(playerElem);
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
    public static void alterPlayer(XmlElement newPlayerElement) {
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
    /// <returns>bool</returns>
    public static bool playerExists(XmlElement player) {
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
    public static string[,,] testGetPlayers() {
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

        return playerList;
    }

    /// <summary>
    /// Pulls all player information from the XML doc and formats it as a 3D matrix
    /// </summary>
    /// <returns>string[,,]</returns>
    public static string[,,] getPlayers() {
        //format [level[player[name, score, time]]]
        //ex:   playerList[0] would return all results of level 1
        //      playerList[0,1] returns the second player's tuple [name, score, time]
        //      playerList[0,1,1] returns level 1, player 2, score
        //      playerList[0,1,0] returns level 1, player 2, name
        //      playerList[0,1,2] returns level 1, player 2, time

        //open database
        startDB();
        //decrypt all data
        //Decrypt();
        //variables
        string name = "";
        int playerIndex = 0;
        int levelIndex = 0;
        //constants (spot in matrix)
        int scoreIndex = 1;
        int timeIndex = 2;

        //create a list matrix
        List<List<List<string>>> playerList = new List<List<List<string>>>();

        //Populate levels first to match how the matrix is read
        XmlElement docElem = doc.DocumentElement;
        XmlElement selectedPlayer = (XmlElement)docElem.FirstChild;
        XmlNodeList levels = selectedPlayer.ChildNodes;
        for (int i = 0; i < levels.Count; i++) {
            XmlElement child = (XmlElement)levels[i];
            if (child.GetAttribute("Number") != "") {
                //add a list of string-lists for every level
                playerList.Add(new List<List<string>>());
            }
        }

        //Get the players
        foreach (XmlElement playerElement in doc.SelectNodes("//Player")) {
            name = playerElement.GetAttribute("Name");
            //traverse their levels
            levels = playerElement.ChildNodes;
            levelIndex = 0;
            for (int i = 0; i < levels.Count; i++) {
                XmlElement child = (XmlElement)levels[i];
                //get the info for each level
                if (child.GetAttribute("Number") != "") {
                    //add a player to the level list and leave placeholders for player info
                    playerList[levelIndex].Add(new List<string> { name, "", "" });
                    //the score will be the first (and only) node in the list below
                    XmlElement score = (XmlElement)(child.SelectNodes(".//TotalScore")[0]);
                    playerList[levelIndex][playerIndex][scoreIndex] =
                        score.InnerText;
                    //same for the bestTime
                    XmlElement bestTime = (XmlElement)(child.SelectNodes(".//BestTime")[0]);
                    playerList[levelIndex][playerIndex][timeIndex] =
                        bestTime.InnerText;
                    levelIndex++;
                }
            }
            playerIndex++;
        }
        //turn the list into an array
        string[,,] playerArray = ListToArray(playerList);
        return playerArray;
    }

    /// <summary>
    /// turns a 3d matrix list into a 3d array
    /// </summary>
    /// <param name="fooList"></param>
    public static string[,,] ListToArray(List<List<List<string>>> fooList) {

        string[,,] foos = new string[fooList.Count, fooList[0].Count, fooList[0][0].Count];

        for (int x = 0; x < fooList.Count; x++) {
            for (int y = 0; y < fooList[x].Count; y++) {
                for (int z = 0; z < fooList[x][y].Count; z++) {
                    foos[x, y, z] = fooList[x][y][z];
                }
            }
        }

        ////uncomment to print to console
        //for (int x = 0; x < foos.GetLength(0); x++) {
        //    Debug.Log("Level " + (x + 1).ToString());
        //    for (int y = 0; y < foos.GetLength(1); y++) {
        //        Debug.Log(foos[x, y, 0] + ", " + foos[x, y, 1] + ", " + foos[x, y, 2]);
        //    }
        //}

        return foos;
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
            foreach (PlayerLevel level in player.levels) {
                level.totalScore = rand.Next(50, 500);
                level.setTime(rand.Next(10), rand.Next(59), rand.Next(59));
            }
        }

        XmlElement playerElm = makePlayerElement(p1);
        addPlayerElement(playerElm);
        playerElm = makePlayerElement(p2);
        addPlayerElement(playerElm);
        playerElm = makePlayerElement(p3);
        addPlayerElement(playerElm);
        playerElm = makePlayerElement(p4);
        addPlayerElement(playerElm);
    }
}
