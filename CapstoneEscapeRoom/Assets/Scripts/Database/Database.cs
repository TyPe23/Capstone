//Original Author: Mary Nations
//Last updated 02/24/2023 by Mary Nations

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;

//[InitializeOnLoad]
public class Database : MonoBehaviour {

    public static string document = "playerData.xml";
    public static XmlDocument doc = new XmlDocument();
    public static Aes key = Aes.Create();

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

        byte[] iv = new byte[16];
        //do not change the encryption key below unless the Xml doc is currently un-encrypted!!
        key.Key = Encoding.UTF8.GetBytes("AAECAwQFBgcICQoLDA0ODw==");
        key.IV = iv;
        //Encrypt("Player");
        //Decrypt();
        //Debug.Log(doc.InnerXml);
        //makeDummyData();
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
            Encrypt(playerElem.GetAttribute("Name"));
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
                doc.Save(document);
                Encrypt(newPlayerElement.GetAttribute("Name"));
            }
        }
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
    public string[,,] getPlayers() {
        //format [level[player[name, score, time]]]
        //ex:   playerList[0] would return all results of level 1
        //      playerList[0,1] returns the second player's tuple [name, score, time]
        //      playerList[0,1,1] returns level 1, player 2, score
        //      playerList[0,1,0] returns level 1, player 2, name
        //      playerList[0,1,2] returns level 1, player 2, time

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
                    playerList[levelIndex].Add(new List<string> { name, "", ""} );
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
    public string[,,] ListToArray(List<List<List<string>>> fooList) {

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
        //    Debug.Log("Level " + x.ToString());
        //    for (int y = 0; y < foos.GetLength(1); y++) {
        //        for (int z = 0; z < foos.GetLength(2); z++) {
        //            Debug.Log(foos[x, y, z]);
        //        }
        //    }
        //}

        return foos;
    }

    /// <summary>
    /// Creates dummy data for testing and presentation purposes
    /// </summary>
    public void makeDummyData() {
        //adding players
        Player p1 = new Player();
        Player p2 = new Player();
        Player p3 = new Player();
        Player p4 = new Player();

        p1.setName("Mary");
        p2.setName("Mathew");
        p3.setName("Benjamin");
        p4.setName("Ty");

        Player[] playerList = { p1, p2, p3, p4};
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

    public static void Encrypt(string username) {
        // Find the specified element in the XmlDocument
        // object and create a new XmlElement object.
        foreach (XmlElement playerElement in doc.SelectNodes("//Player")) {
            //find element with matching username
            if (playerElement.GetAttribute("Name") == username) {
                foreach (XmlElement elementToEncrypt in playerElement) {
                    Debug.Log(elementToEncrypt.OuterXml);
                    // Create a new instance of the EncryptedXml class
                    // and use it to encrypt the XmlElement with the
                    // symmetric key.
                    EncryptedXml eXml = new();

                    byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, key, false);
                    // Construct an EncryptedData object and populate
                    // it with the desired encryption information.

                    EncryptedData edElement = new() {
                        Type = EncryptedXml.XmlEncElementUrl
                    };

                    // Create an EncryptionMethod element so that the
                    // receiver knows which algorithm to use for decryption.
                    // Determine what kind of algorithm is being used and
                    // supply the appropriate URL to the EncryptionMethod element.

                    string encryptionMethod = null;

                    encryptionMethod = EncryptedXml.XmlEncAES256Url;

                    edElement.EncryptionMethod = new EncryptionMethod(encryptionMethod);

                    // Add the encrypted element data to the
                    // EncryptedData object.
                    edElement.CipherData.CipherValue = encryptedElement;

                    // Replace the element from the original XmlDocument
                    // object with the EncryptedData element.
                    EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
                }
                break;
            }
        }
        doc.Save(document);
    }

    /// <summary>
    /// Decrypts everything encrypted in the Xml document
    /// </summary>
    public static void Decrypt() {
        //"continue" is a reserved word
        bool cont = true;
        while (cont) {
            // Find the EncryptedData element in the XmlDocument.
            XmlElement encryptedElement = doc.GetElementsByTagName("EncryptedData")[0] as XmlElement;

            // If the EncryptedData element was not found, throw an exception.
            if (encryptedElement == null) {
                cont = false;
                break;
            }

            // Create an EncryptedData object and populate it.
            EncryptedData edElement = new();
            edElement.LoadXml(encryptedElement);

            // Create a new EncryptedXml object.
            EncryptedXml exml = new();

            // Decrypt the element using the symmetric key.
            byte[] rgbOutput = exml.DecryptData(edElement, key);

            // Replace the encryptedData element with the plaintext XML element.
            exml.ReplaceData(encryptedElement, rgbOutput);
        }
        doc.Save(document);
    }
}
