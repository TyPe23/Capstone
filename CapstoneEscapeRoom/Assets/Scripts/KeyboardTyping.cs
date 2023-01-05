using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardTyping : MonoBehaviour {

    string word = null;
    int wordIndex = 0;
    string alpha;
    public Text input = null;

    public void typingFunct(string letter) {
        wordIndex++;
        word = word + letter;
        input.text = word;
    }
}