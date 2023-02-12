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

        //adding players
        //Player p1 = new Player();
        //Player p2 = new Player();
        Player p3 = new Player();

        //p1.setName("player1");
        //p2.setName("player2");
        p3.setName("player1");
        p3.setLevel(3);

        //addPlayer(p1, document);
        //addPlayer(p2, document);
        XmlElement playerElm = makePlayerElement(p3, doc);
        //addPlayerElement(playerElm, doc);
        //playerElm = makePlayerElement(p2, doc);
        //addPlayerElement(playerElm, doc);
        //playerElm = makePlayerElement(p3, doc);
        //addPlayerElement(playerElm, doc);

        alterPlayer(playerElm, doc);

        //alterPlayer("player1", "Level", "3");


    }

    public static XmlElement makePlayerElement(Player player, XmlDocument doc) {

        //// open and load the document
        //XmlDocument doc = new XmlDocument();
        //doc.Load(path);

        //create all elements
        XmlElement p = doc.CreateElement("Player");
        XmlElement level = doc.CreateElement("Level");
        XmlAttribute name = doc.CreateAttribute("Name");

        //set element values
        level.InnerText = player.level.ToString();
        name.Value = player.name;

        //set all child elements of the new node
        p.Attributes.Append(name);
        p.AppendChild(level);
        //name.AppendChild(level);

        ////append this node to the root parent node
        //doc.DocumentElement.AppendChild(p);
        //doc.Save(path);

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
    /// Searches for a node by username and changes the given field/element to the new value
    /// </summary>
    /// <param name="newPlayerElement"></param>
    /// <param name="xml"></param>
    public void alterPlayer(XmlElement newPlayerElement, XmlDocument xml){//string username, string field, string newValue) {

        //XmlDocument xml = new XmlDocument();
        //xml.Load(document);
        //get name of current element
        string username = newPlayerElement.GetAttribute("Name");
        Debug.Log(username);
        foreach (XmlElement playerElement in xml.SelectNodes("//Player")) {
            Debug.Log(playerElement.GetAttribute("Name"));
            //find element with matching username
            if (playerElement.GetAttribute("Name") == username) {
                Debug.Log("in the If!");
                //replace old player instance with new player instance
                xml.DocumentElement.ReplaceChild(newPlayerElement, playerElement);
            }
            
            
            //foreach (XmlElement element1 in playerElement) {
            //    //Debug.Log(playerElement.OuterXml);
            //    Debug.Log(element1.InnerText.Split('\n')[0]);

                //Debug.Log(playerElement.SelectSingleNode(".//Name").InnerText.Split('\n')[0]);
                //Debug.Log(username);
                //Grab the user name (the 1st element in the InnerText)
                
                
                //if (element1.InnerText.Split('\n')[0] == username) {
                //    Debug.Log("in if");
                     
                //    XmlNode newNode = xml.CreateElement("test");
                //    newNode.InnerText = newValue;
                //    element1.AppendChild(newNode);
                //    playerElement.ReplaceChild(newNode, element1);
                    
                    
                    
                //    //XmlNode oldNode = element1.SelectSingleNode("//" + field);
                //    //element1.ReplaceChild(newNode, oldNode);

                //    xml.Save(document);
                //}
                //else {
                //    Debug.Log("fml");
                //}
            //}
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
