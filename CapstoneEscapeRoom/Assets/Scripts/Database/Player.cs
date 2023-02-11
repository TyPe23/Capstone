using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player {
    [SerializeField]
    public string name;
    public int number;
    public TimeSpan time;
    

    public Player() {
        number = 5;
        //time = (2, 4, 6);
    }
}
