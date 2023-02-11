using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;

public class Main : MonoBehaviour {
  void Start() {
    Player p = new Player();
    XmlSerializer serializer = new XmlSerializer(typeof(Player));

    // serialize
    using (FileStream tw = new FileStream("test.xml", FileMode.OpenOrCreate)) {
      serializer.Serialize(tw, p);
    }

    // deserialize
    using (FileStream tw = new FileStream("test.xml", FileMode.Open)) {
      p = serializer.Deserialize(tw) as Player;
    }

    print(p.number);
  }
}
