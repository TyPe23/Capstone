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

    public string document = "playerData.xml";

    public Main() {

        ////adding players
        //Player p1 = new Player();
        //Player p2 = new Player();
        //Player p3 = new Player();

        //p1.setName("player1");
        //p2.setName("player2");
        //p3.setName("player5");

        //addPlayer(p1, document);
        //addPlayer(p2, document);
        //addPlayer(p3, document);

        alterPlayer("player1", "Level", "3");


    }

    public static void addPlayer(Player player, string path) {
        
        // open and load the document
        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        //create all elements
        XmlElement p = doc.CreateElement("Player");
        XmlElement level = doc.CreateElement("Level");
        XmlElement name = doc.CreateElement("Name");

        //set element values
        level.InnerText = player.level.ToString();
        name.InnerText = player.name;

        //set all child elements of the new node
        name.AppendChild(level);
        p.AppendChild(name);

        //append this node to the root parent node
        doc.DocumentElement.AppendChild(p);
        doc.Save(path);

    }
    /// <summary>
    /// Searches for a node by username and changes the given field/element to the new value
    /// </summary>
    /// <param name="username"></param>
    /// <param name="field"></param>
    /// <param name="newValue"></param>
    public void alterPlayer(string username, string field, string newValue) {

        //XmlDocument xml = new XmlDocument();
        //xml.Load(document);

        //XmlNode root = xml.DocumentElement;
        //string searchString = "descendant::Player[Name='" + username + "']";

        //XmlNode node = root.SelectSingleNode(searchString);
        //Debug.Log(((System.Xml.XmlElement)node).GetAttribute("Level"));
        ////XmlNode newNode = xml.CreateElement(field);
        ////newNode.InnerText = newValue;

        ////        node.Element(field) = newValue;

        ////xml.ReplaceChild(newNode, node);

        //xml.Save(document);


        XmlDocument xml = new XmlDocument();

        xml.Load(document);

        foreach (XmlElement playerElement in xml.SelectNodes("//Player")) {
            foreach (XmlElement element1 in playerElement) {
                //Debug.Log(playerElement.OuterXml);
                Debug.Log(playerElement.SelectSingleNode(".//Name").InnerText.Split('\n')[0]);
                Debug.Log(username);
                //Grab the user name (the 1st element in the InnerText)
                if (playerElement.SelectSingleNode(".//Name").InnerText.Split('\n')[0].ToString() == username) {
                    Debug.Log("in if");
                     
                    XmlNode newNode = xml.CreateElement("test");
                    newNode.InnerText = newValue;
                    element1.AppendChild(newNode);
                    playerElement.ReplaceChild(newNode, element1);
                    //XmlNode oldNode = element1.SelectSingleNode("//" + field);
                    //element1.ReplaceChild(newNode, oldNode);

                    xml.Save(document);
                }
                else {
                    Debug.Log("fml");
                }
            }
        }
        //xml.Save(document);
    }


    public static T Deserialize<T>(string path) {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StreamReader reader = new StreamReader(path);
        T deserialized = (T)serializer.Deserialize(reader.BaseStream);
        reader.Close();
        return deserialized;
    }
}
